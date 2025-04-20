
using System.ComponentModel;

namespace CleanArchitecture.Blazor.Infrastructure.PermissionSet;

public static partial class Permissions
{
    [DisplayName("PurchaseItem Permissions")]
    [Description("Set permissions for purchaseitem operations.")]
    public static class PurchaseItems
    {
        [Description("Allows viewing purchaseitem details.")]
        public const string View = "Permissions.PurchaseItems.View";
        [Description("Allows creating purchaseitem details.")]
        public const string Create = "Permissions.PurchaseItems.Create";
        [Description("Allows editing purchaseitem details.")]
        public const string Edit = "Permissions.PurchaseItems.Edit";
        [Description("Allows deleting purchaseitem details.")]
        public const string Delete = "Permissions.PurchaseItems.Delete";
        [Description("Allows printing purchaseitem details.")]
        public const string Print = "Permissions.PurchaseItems.Print";
        [Description("Allows searching purchaseitem details.")]
        public const string Search = "Permissions.PurchaseItems.Search";
        [Description("Allows exporting purchaseitem details.")]
        public const string Export = "Permissions.PurchaseItems.Export";
        [Description("Allows importing purchaseitem details.")]
        public const string Import = "Permissions.PurchaseItems.Import";
    }
}

