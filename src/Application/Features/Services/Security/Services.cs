namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("Service Permissions")]
    [Description("Set permissions for service operations.")]
    public static class Services
    {
        [Description("Allows viewing service details.")]
        public const string View = "Permissions.Services.View";
        [Description("Allows creating service details.")]
        public const string Create = "Permissions.Services.Create";
        [Description("Allows editing service details.")]
        public const string Edit = "Permissions.Services.Edit";
        [Description("Allows deleting service details.")]
        public const string Delete = "Permissions.Services.Delete";
        [Description("Allows printing service details.")]
        public const string Print = "Permissions.Services.Print";
        [Description("Allows searching service details.")]
        public const string Search = "Permissions.Services.Search";
        [Description("Allows exporting service details.")]
        public const string Export = "Permissions.Services.Export";
        [Description("Allows importing service details.")]
        public const string Import = "Permissions.Services.Import";
    }
}

public class ServicesAccessRights
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
