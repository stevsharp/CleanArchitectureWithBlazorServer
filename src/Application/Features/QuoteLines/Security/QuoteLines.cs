namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("QuoteLine Permissions")]
    [Description("Set permissions for quoteline operations.")]
    public static class QuoteLines
    {
        [Description("Allows viewing quoteline details.")]
        public const string View = "Permissions.QuoteLines.View";
        [Description("Allows creating quoteline details.")]
        public const string Create = "Permissions.QuoteLines.Create";
        [Description("Allows editing quoteline details.")]
        public const string Edit = "Permissions.QuoteLines.Edit";
        [Description("Allows deleting quoteline details.")]
        public const string Delete = "Permissions.QuoteLines.Delete";
        [Description("Allows printing quoteline details.")]
        public const string Print = "Permissions.QuoteLines.Print";
        [Description("Allows searching quoteline details.")]
        public const string Search = "Permissions.QuoteLines.Search";
        [Description("Allows exporting quoteline details.")]
        public const string Export = "Permissions.QuoteLines.Export";
        [Description("Allows importing quoteline details.")]
        public const string Import = "Permissions.QuoteLines.Import";
    }
}

public class QuoteLinesAccessRights
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
