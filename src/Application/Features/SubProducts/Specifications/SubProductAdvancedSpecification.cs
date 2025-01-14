﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2025-01-13
//     Last Modified: 2025-01-13
//     Description: 
//       Defines a specification for applying advanced filtering options to the 
//       SubProduct entity, supporting different views and keyword-based searches.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanArchitecture.Blazor.Application.Features.SubProducts.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for advanced filtering of SubProducts.
/// </summary>
public class SubProductAdvancedSpecification : Specification<SubProduct>
{
    public SubProductAdvancedSpecification(SubProductAdvancedFilter filter)
    {
        DateTime today = DateTime.UtcNow;
        var todayrange = today.GetDateRange(SubProductListView.TODAY.ToString(), filter.LocalTimezoneOffset);
        var last30daysrange = today.GetDateRange(SubProductListView.LAST_30_DAYS.ToString(),filter.LocalTimezoneOffset);

        Query
             .Where(filter.Keyword,!string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == SubProductListView.My && filter.CurrentUser is not null)
             .Where(x => x.Created >= todayrange.Start && x.Created < todayrange.End.AddDays(1), filter.ListView == SubProductListView.TODAY)
             .Where(x => x.Created >= last30daysrange.Start, filter.ListView == SubProductListView.LAST_30_DAYS);
       
    }
}
