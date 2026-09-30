namespace Entities.Constants;

public class RoleNames
{
    public const string SystemAdmin = "SystemAdmin";
    public const string CompanyAdmin = "CompanyAdmin";
    public const string ProjectManager = "ProjectManager";
    public const string SiteManager = "SiteManager";
    public const string Engineer = "Engineer";
    public const string Accountant = "Accountant";
    public const string Procurement = "Procurement";
    public const string Warehouse = "Warehouse";
    public const string Viewer = "Viewer";

     // Seed ve döngüler için (const olamaz, dizi olduğu için static readonly)
    public static readonly string[] All =
    {
        SystemAdmin, CompanyAdmin, ProjectManager,
        SiteManager, Accountant, Viewer
    };


}
