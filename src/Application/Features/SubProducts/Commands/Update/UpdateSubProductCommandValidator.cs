﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under one or more agreements.
//     The .NET Foundation licenses this file to you under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2025-01-13
//     Last Modified: 2025-01-13
//     Description: 
//       This file defines the validation rules for the UpdateSubProductCommand within 
//       the CleanArchitecture.Blazor application. It ensures that the command has 
//       all required fields properly populated and validates constraints such as 
//       maximum length and non-null requirements before proceeding with updating 
//       a subproduct.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/subproduct
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// The `UpdateSubProductCommandValidator` is used to validate that an `UpdateSubProductCommand` 
// contains valid and complete data before processing the update. It enforces rules such as 
// ensuring that the `Id` is provided, the `Name` is not empty, and certain properties 
// (e.g., ...) do not exceed their maximum 
// allowed length.

namespace CleanArchitecture.Blazor.Application.Features.SubProducts.Commands.Update;

public class UpdateSubProductCommandValidator : AbstractValidator<UpdateSubProductCommand>
{
        public UpdateSubProductCommandValidator()
        {
           RuleFor(v => v.Id).NotNull();
               RuleFor(v => v.ProdId).NotNull(); 
    RuleFor(v => v.Unit).MaximumLength(255); 
    RuleFor(v => v.Color).MaximumLength(255); 

          
        }
    
}

