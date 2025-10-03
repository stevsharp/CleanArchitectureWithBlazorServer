namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("Vendor Permissions")]
    [Description("Set permissions for vendor operations.")]
    public static class Vendors
    {
        [Description("Allows viewing vendor details.")]
        public const string View = "Permissions.Vendors.View";
        [Description("Allows creating vendor details.")]
        public const string Create = "Permissions.Vendors.Create";
        [Description("Allows editing vendor details.")]
        public const string Edit = "Permissions.Vendors.Edit";
        [Description("Allows deleting vendor details.")]
        public const string Delete = "Permissions.Vendors.Delete";
        [Description("Allows printing vendor details.")]
        public const string Print = "Permissions.Vendors.Print";
        [Description("Allows searching vendor details.")]
        public const string Search = "Permissions.Vendors.Search";
        [Description("Allows exporting vendor details.")]
        public const string Export = "Permissions.Vendors.Export";
        [Description("Allows importing vendor details.")]
        public const string Import = "Permissions.Vendors.Import";
    }
}

public class VendorsAccessRights
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
