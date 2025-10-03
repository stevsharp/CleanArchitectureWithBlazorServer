namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("CostItem Permissions")]
    [Description("Set permissions for costitem operations.")]
    public static class CostItems
    {
        [Description("Allows viewing costitem details.")]
        public const string View = "Permissions.CostItems.View";
        [Description("Allows creating costitem details.")]
        public const string Create = "Permissions.CostItems.Create";
        [Description("Allows editing costitem details.")]
        public const string Edit = "Permissions.CostItems.Edit";
        [Description("Allows deleting costitem details.")]
        public const string Delete = "Permissions.CostItems.Delete";
        [Description("Allows printing costitem details.")]
        public const string Print = "Permissions.CostItems.Print";
        [Description("Allows searching costitem details.")]
        public const string Search = "Permissions.CostItems.Search";
        [Description("Allows exporting costitem details.")]
        public const string Export = "Permissions.CostItems.Export";
        [Description("Allows importing costitem details.")]
        public const string Import = "Permissions.CostItems.Import";
    }
}

public class CostItemsAccessRights
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
