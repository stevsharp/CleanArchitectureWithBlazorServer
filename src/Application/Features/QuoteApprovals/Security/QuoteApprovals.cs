
namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("QuoteApproval Permissions")]
    [Description("Set permissions for quoteapproval operations.")]
    public static class QuoteApprovals
    {
        [Description("Allows viewing quoteapproval details.")]
        public const string View = "Permissions.QuoteApprovals.View";
        [Description("Allows creating quoteapproval details.")]
        public const string Create = "Permissions.QuoteApprovals.Create";
        [Description("Allows editing quoteapproval details.")]
        public const string Edit = "Permissions.QuoteApprovals.Edit";
        [Description("Allows deleting quoteapproval details.")]
        public const string Delete = "Permissions.QuoteApprovals.Delete";
        [Description("Allows printing quoteapproval details.")]
        public const string Print = "Permissions.QuoteApprovals.Print";
        [Description("Allows searching quoteapproval details.")]
        public const string Search = "Permissions.QuoteApprovals.Search";
        [Description("Allows exporting quoteapproval details.")]
        public const string Export = "Permissions.QuoteApprovals.Export";
        [Description("Allows importing quoteapproval details.")]
        public const string Import = "Permissions.QuoteApprovals.Import";
    }
}

public class QuoteApprovalsAccessRights
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
