﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-06
//     Last Modified: 2024-12-06
//     Description: 
//       Defines a specification for filtering a Offer entity by its ID.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CleanArchitecture.Blazor.Application.Features.Offers.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for filtering Offers by their ID.
/// </summary>
public class OfferByIdSpecification : Specification<Offer>
{
    public OfferByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}