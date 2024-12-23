﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-02
//     Last Modified: 2024-12-02
//     Description: 
//       Defines permission constants for crmproductcategory-related operations, such as 
//       viewing, creating, editing, deleting, and more. These permissions are 
//       used to manage access control within the application.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;

namespace CleanArchitecture.Blazor.Infrastructure.PermissionSet;

public static partial class Permissions
{
    [DisplayName("CrmProductCategory Permissions")]
    [Description("Set permissions for crmproductcategory operations.")]
    public static class CrmProductCategories
    {
        [Description("Allows viewing crmproductcategory details.")]
        public const string View = "Permissions.CrmProductCategories.View";
        [Description("Allows creating crmproductcategory details.")]
        public const string Create = "Permissions.CrmProductCategories.Create";
        [Description("Allows editing crmproductcategory details.")]
        public const string Edit = "Permissions.CrmProductCategories.Edit";
        [Description("Allows deleting crmproductcategory details.")]
        public const string Delete = "Permissions.CrmProductCategories.Delete";
        [Description("Allows printing crmproductcategory details.")]
        public const string Print = "Permissions.CrmProductCategories.Print";
        [Description("Allows searching crmproductcategory details.")]
        public const string Search = "Permissions.CrmProductCategories.Search";
        [Description("Allows exporting crmproductcategory details.")]
        public const string Export = "Permissions.CrmProductCategories.Export";
        [Description("Allows importing crmproductcategory details.")]
        public const string Import = "Permissions.CrmProductCategories.Import";
    }
}

