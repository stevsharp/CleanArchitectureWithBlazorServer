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
//       This file defines the validation rules for the ImportOffersCommand 
//       within the CleanArchitecture.Blazor application. It ensures that the 
//       command's required properties are correctly set before proceeding with 
//       the offer import process.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/offer
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// This validator is used to ensure that an ImportOffersCommand has valid input 
// before attempting to import offer data. It checks that the Data property is not 
// null and is not empty, ensuring that the command has valid content for import.

namespace CleanArchitecture.Blazor.Application.Features.Offers.Commands.Import;

public class ImportOffersCommandValidator : AbstractValidator<ImportOffersCommand>
{
        public ImportOffersCommandValidator()
        {
           
           RuleFor(v => v.Data)
                .NotNull()
                .NotEmpty();

        }
}

