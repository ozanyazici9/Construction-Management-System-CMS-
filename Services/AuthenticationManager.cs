using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Entities.Constants;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class AuthenticationManager : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;
    private readonly IConfiguration _configuration;

    private User? _user;

    public AuthenticationManager(
        UserManager<User> userManager,
        IMapper mapper,
        ILoggerService logger,
        IRepositoryManager repositoryManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _mapper = mapper;
        _logger = logger;
        _repositoryManager = repositoryManager;
        _configuration = configuration;
    }

    public async Task<TokenDto> CreateToken(bool populateExp)
    {
        var signinCredentials = GetSiginCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

        var refreshToken = GenerateRefreshToken();
        _user.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new TokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public async Task<IdentityResult> RegisterCompanyAsync(
        CompanyForRegistrationDto companyForRegistrationDto
    )
    {
        var company = _mapper.Map<Company>(companyForRegistrationDto.Company);
        var user = _mapper.Map<User>(companyForRegistrationDto.Owner);

        await using var transaction = await _repositoryManager.BeginTransactionAsync();

        _repositoryManager.Company.CreateOneCompany(company);
        await _repositoryManager.SaveAsync(); // company.Id burada oluşuyor.

        user.CompanyId = company.Id;

        var result = await _userManager.CreateAsync(user, companyForRegistrationDto.Owner.Password);
        if (!result.Succeeded)
            return result; // commit edilmeden çıkınca transaction geri alındı.

        var roleResult = await _userManager.AddToRoleAsync(user, RoleNames.CompanyAdmin);
        if (!result.Succeeded)
            return roleResult; // yine rollback

        await transaction.CommitAsync();
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
    {
        var user = _mapper.Map<User>(userForRegistrationDto);

        var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRolesAsync(user, userForRegistrationDto.Roles);
        }

        return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
    {
        _user = await _userManager.FindByNameAsync(userForAuthDto.UserName);
        var result =
            _user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password);
        if (!result)
        {
            _logger.LogWarning(
                $"{nameof(ValidateUser)} : Authentication failed. Wrong username or password"
            );
        }
        return result;
    }

    private SigningCredentials GetSiginCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim("CompanyId", _user.CompanyId.ToString()),
        };

        var roles = await _userManager.GetRolesAsync(_user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(
        SigningCredentials signinCredentials,
        List<Claim> claims
    )
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signinCredentials
        );

        return tokenOptions;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["secretKey"];

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;

        // ClaimsPrincipal: Süresi dolmuş access token'ın içinden çıkarılan kullanıcı kimliğidir.
        // İçinde token'ın payload kısmındaki claim'ler (kullanıcı adı, roller, iss, aud, exp) bulunur.
        // Burada amaç yetkilendirme değil, token'ın sahibini tespit etmektir:
        // principal.Identity.Name ile kullanıcıyı veritabanından bulup refresh token'ını doğrularız.
        var principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out securityToken
        );

        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (
            jwtSecurityToken is null
            || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase
            )
        )
        {
            throw new SecurityTokenException("Invalid Token");
        }

        return principal;
    }
}
