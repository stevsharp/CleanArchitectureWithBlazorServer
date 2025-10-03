namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("PricingModel Permissions")]
    [Description("Set permissions for pricingmodel operations.")]
    public static class PricingModels
    {
        [Description("Allows viewing pricingmodel details.")]
        public const string View = "Permissions.PricingModels.View";
        [Description("Allows creating pricingmodel details.")]
        public const string Create = "Permissions.PricingModels.Create";
        [Description("Allows editing pricingmodel details.")]
        public const string Edit = "Permissions.PricingModels.Edit";
        [Description("Allows deleting pricingmodel details.")]
        public const string Delete = "Permissions.PricingModels.Delete";
        [Description("Allows printing pricingmodel details.")]
        public const string Print = "Permissions.PricingModels.Print";
        [Description("Allows searching pricingmodel details.")]
        public const string Search = "Permissions.PricingModels.Search";
        [Description("Allows exporting pricingmodel details.")]
        public const string Export = "Permissions.PricingModels.Export";
        [Description("Allows importing pricingmodel details.")]
        public const string Import = "Permissions.PricingModels.Import";
    }
}

public class PricingModelsAccessRights
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
