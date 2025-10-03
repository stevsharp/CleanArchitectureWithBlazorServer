namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("EquipmentItem Permissions")]
    [Description("Set permissions for equipmentitem operations.")]
    public static class EquipmentItems
    {
        [Description("Allows viewing equipmentitem details.")]
        public const string View = "Permissions.EquipmentItems.View";
        [Description("Allows creating equipmentitem details.")]
        public const string Create = "Permissions.EquipmentItems.Create";
        [Description("Allows editing equipmentitem details.")]
        public const string Edit = "Permissions.EquipmentItems.Edit";
        [Description("Allows deleting equipmentitem details.")]
        public const string Delete = "Permissions.EquipmentItems.Delete";
        [Description("Allows printing equipmentitem details.")]
        public const string Print = "Permissions.EquipmentItems.Print";
        [Description("Allows searching equipmentitem details.")]
        public const string Search = "Permissions.EquipmentItems.Search";
        [Description("Allows exporting equipmentitem details.")]
        public const string Export = "Permissions.EquipmentItems.Export";
        [Description("Allows importing equipmentitem details.")]
        public const string Import = "Permissions.EquipmentItems.Import";
    }
}

public class EquipmentItemsAccessRights
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
