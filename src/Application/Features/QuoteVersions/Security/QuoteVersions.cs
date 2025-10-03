﻿namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("QuoteVersion Permissions")]
    [Description("Set permissions for quoteversion operations.")]
    public static class QuoteVersions
    {
        [Description("Allows viewing quoteversion details.")]
        public const string View = "Permissions.QuoteVersions.View";
        [Description("Allows creating quoteversion details.")]
        public const string Create = "Permissions.QuoteVersions.Create";
        [Description("Allows editing quoteversion details.")]
        public const string Edit = "Permissions.QuoteVersions.Edit";
        [Description("Allows deleting quoteversion details.")]
        public const string Delete = "Permissions.QuoteVersions.Delete";
        [Description("Allows printing quoteversion details.")]
        public const string Print = "Permissions.QuoteVersions.Print";
        [Description("Allows searching quoteversion details.")]
        public const string Search = "Permissions.QuoteVersions.Search";
        [Description("Allows exporting quoteversion details.")]
        public const string Export = "Permissions.QuoteVersions.Export";
        [Description("Allows importing quoteversion details.")]
        public const string Import = "Permissions.QuoteVersions.Import";
    }
}

public class QuoteVersionsAccessRights
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
