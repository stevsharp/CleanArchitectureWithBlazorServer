﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under one or more agreements.
//     The .NET Foundation licenses this file to you under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2024-12-06
//     Last Modified: 2024-12-06
//     Description: 
//       This file defines the validation rules for the UpdateSupplierCommand within 
//       the CleanArchitecture.Blazor application. It ensures that the command has 
//       all required fields properly populated and validates constraints such as 
//       maximum length and non-null requirements before proceeding with updating 
//       a supplier.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/supplier
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// The `UpdateSupplierCommandValidator` is used to validate that an `UpdateSupplierCommand` 
// contains valid and complete data before processing the update. It enforces rules such as 
// ensuring that the `Id` is provided, the `Name` is not empty, and certain properties 
// (e.g., ...) do not exceed their maximum 
// allowed length.

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Commands.Update;

public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
{
        public UpdateSupplierCommandValidator()
        {
           RuleFor(v => v.Id).NotNull();
               RuleFor(v => v.Name).MaximumLength(50).NotEmpty(); 
    RuleFor(v => v.Address).MaximumLength(255); 
    RuleFor(v => v.Phone).MaximumLength(255); 
    RuleFor(v => v.Email).MaximumLength(255); 
    RuleFor(v => v.VAT).MaximumLength(255); 
    RuleFor(v => v.Country).MaximumLength(255); 
    RuleFor(v => v.CreatedAt).NotNull(); 

          
        }
    
}
