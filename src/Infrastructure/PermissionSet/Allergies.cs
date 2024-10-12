// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;

namespace CleanArchitecture.Blazor.Infrastructure.PermissionSet;

public static partial class Permissions
{
    [DisplayName("Allergy Permissions")]
    [Description("Set permissions for allergy operations.")]
    public static class Allergies
    {
        [Description("Allows viewing allergy details.")]
        public const string View = "Permissions.Allergies.View";
        [Description("Allows creating allergy details.")]
        public const string Create = "Permissions.Allergies.Create";
        [Description("Allows editing allergy details.")]
        public const string Edit = "Permissions.Allergies.Edit";
        [Description("Allows deleting allergy details.")]
        public const string Delete = "Permissions.Allergies.Delete";
        [Description("Allows printing allergy details.")]
        public const string Print = "Permissions.Allergies.Print";
        [Description("Allows searching allergy details.")]
        public const string Search = "Permissions.Allergies.Search";
        [Description("Allows exporting allergy details.")]
        public const string Export = "Permissions.Allergies.Export";
        [Description("Allows importing allergy details.")]
        public const string Import = "Permissions.Allergies.Import";
    }
}

