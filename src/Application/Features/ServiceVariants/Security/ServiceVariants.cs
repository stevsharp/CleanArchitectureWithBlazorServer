namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("ServiceVariant Permissions")]
    [Description("Set permissions for servicevariant operations.")]
    public static class ServiceVariants
    {
        [Description("Allows viewing servicevariant details.")]
        public const string View = "Permissions.ServiceVariants.View";
        [Description("Allows creating servicevariant details.")]
        public const string Create = "Permissions.ServiceVariants.Create";
        [Description("Allows editing servicevariant details.")]
        public const string Edit = "Permissions.ServiceVariants.Edit";
        [Description("Allows deleting servicevariant details.")]
        public const string Delete = "Permissions.ServiceVariants.Delete";
        [Description("Allows printing servicevariant details.")]
        public const string Print = "Permissions.ServiceVariants.Print";
        [Description("Allows searching servicevariant details.")]
        public const string Search = "Permissions.ServiceVariants.Search";
        [Description("Allows exporting servicevariant details.")]
        public const string Export = "Permissions.ServiceVariants.Export";
        [Description("Allows importing servicevariant details.")]
        public const string Import = "Permissions.ServiceVariants.Import";
    }
}

public class ServiceVariantsAccessRights
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
