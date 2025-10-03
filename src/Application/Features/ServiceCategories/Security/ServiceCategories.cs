namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("ServiceCategory Permissions")]
    [Description("Set permissions for servicecategory operations.")]
    public static class ServiceCategories
    {
        [Description("Allows viewing servicecategory details.")]
        public const string View = "Permissions.ServiceCategories.View";
        [Description("Allows creating servicecategory details.")]
        public const string Create = "Permissions.ServiceCategories.Create";
        [Description("Allows editing servicecategory details.")]
        public const string Edit = "Permissions.ServiceCategories.Edit";
        [Description("Allows deleting servicecategory details.")]
        public const string Delete = "Permissions.ServiceCategories.Delete";
        [Description("Allows printing servicecategory details.")]
        public const string Print = "Permissions.ServiceCategories.Print";
        [Description("Allows searching servicecategory details.")]
        public const string Search = "Permissions.ServiceCategories.Search";
        [Description("Allows exporting servicecategory details.")]
        public const string Export = "Permissions.ServiceCategories.Export";
        [Description("Allows importing servicecategory details.")]
        public const string Import = "Permissions.ServiceCategories.Import";
    }
}

public class ServiceCategoriesAccessRights
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
