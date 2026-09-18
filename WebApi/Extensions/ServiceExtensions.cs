using Asp.Versioning;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Presentation.ActionFilters;
using Presentation.Controllers;
using Repositories.Contracts;
using Repositories.EfCore;
using Services;
using Services.Contracts;

namespace WebApi.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSqlContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
        );
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services
            .AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequiredLength = 6;

                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyManager>();
        services.AddScoped<ICurrentTenantService, CurrentTenantService>();
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerService, LoggerManager>();
    }

    public static void ConfigureActionFilters(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilterAttribute>();
        services.AddSingleton<LogFilterAttribute>();
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "CorsPolicy",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination")
            );
        });
    }

    public static void ConfigureDataShaper(this IServiceCollection services)
    {
        services.AddScoped(typeof(IDataShaper<>), typeof(DataShaper<>));
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc(opt =>
            {
                opt.Conventions.Controller<CompanyController>().HasApiVersion(new ApiVersion(1, 0));

                opt.Conventions.Controller<CompanyV2Controller>().HasApiVersion(new ApiVersion(2, 0));
            })
            .AddApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "CMS",
                    Version = "v1",
                    Description = "CMS ASP.NET Core API",
                    TermsOfService = new Uri("https://www.ozanyazici.com.tr/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ozan Yazıcı",
                        Email = "ozanyazici9@gmail.com",
                        Url = new Uri("https://www.ozanyazici.com.tr/"),
                    },
                }
            );
            s.SwaggerDoc("v2", new OpenApiInfo { Title = "CMS V2", Version = "v2" });

            s.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token, no need to add the \"Bearer\" prefix.",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                }
            );

            s.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = [],
            });
        });
    }
}
