﻿
using System.ComponentModel;

namespace CleanArchitecture.Blazor.Infrastructure.PermissionSet;

public static partial class Permissions
{
    [DisplayName("InvoiceLine Permissions")]
    [Description("Set permissions for invoiceline operations.")]
    public static class InvoiceLines
    {
        [Description("Allows viewing invoiceline details.")]
        public const string View = "Permissions.InvoiceLines.View";
        [Description("Allows creating invoiceline details.")]
        public const string Create = "Permissions.InvoiceLines.Create";
        [Description("Allows editing invoiceline details.")]
        public const string Edit = "Permissions.InvoiceLines.Edit";
        [Description("Allows deleting invoiceline details.")]
        public const string Delete = "Permissions.InvoiceLines.Delete";
        [Description("Allows printing invoiceline details.")]
        public const string Print = "Permissions.InvoiceLines.Print";
        [Description("Allows searching invoiceline details.")]
        public const string Search = "Permissions.InvoiceLines.Search";
        [Description("Allows exporting invoiceline details.")]
        public const string Export = "Permissions.InvoiceLines.Export";
        [Description("Allows importing invoiceline details.")]
        public const string Import = "Permissions.InvoiceLines.Import";
    }
}

