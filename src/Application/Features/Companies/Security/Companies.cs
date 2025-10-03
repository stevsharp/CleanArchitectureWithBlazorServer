namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("Company Permissions")]
    [Description("Set permissions for company operations.")]
    public static class Companies
    {
        [Description("Allows viewing company details.")]
        public const string View = "Permissions.Companies.View";
        [Description("Allows creating company details.")]
        public const string Create = "Permissions.Companies.Create";
        [Description("Allows editing company details.")]
        public const string Edit = "Permissions.Companies.Edit";
        [Description("Allows deleting company details.")]
        public const string Delete = "Permissions.Companies.Delete";
        [Description("Allows printing company details.")]
        public const string Print = "Permissions.Companies.Print";
        [Description("Allows searching company details.")]
        public const string Search = "Permissions.Companies.Search";
        [Description("Allows exporting company details.")]
        public const string Export = "Permissions.Companies.Export";
        [Description("Allows importing company details.")]
        public const string Import = "Permissions.Companies.Import";
    }
}

public class CompaniesAccessRights
{
    public bool View { get; set; }
    public bool Create { get; set; }
    public bool Edit { get; set; }
    public bool Delete { get; set; }
    public bool Print { get; set; }
    public bool Search { get; set; }
    public bool Export { get; set; }
    public bool Import { get; set; }
}
