﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This file is part of the CleanArchitecture.Blazor project.
//     Licensed to the .NET Foundation under one or more agreements.
//     The .NET Foundation licenses this file to you under the MIT license.
//     See the LICENSE file in the project root for more information.
//
//     Author: neozhu
//     Created Date: 2025-02-04
//     Last Modified: 2025-02-04
//     Description: 
//       This file defines the validation rules for the ImportStepsCommand 
//       within the CleanArchitecture.Blazor application. It ensures that the 
//       command's required properties are correctly set before proceeding with 
//       the step import process.
//     
//     Documentation:
//       https://docs.cleanarchitectureblazor.com/features/step
// </auto-generated>
//------------------------------------------------------------------------------

// Usage:
// This validator is used to ensure that an ImportStepsCommand has valid input 
// before attempting to import step data. It checks that the Data property is not 
// null and is not empty, ensuring that the command has valid content for import.

namespace CleanArchitecture.Blazor.Application.Features.Steps.Commands.Import;

public class ImportStepsCommandValidator : AbstractValidator<ImportStepsCommand>
{
        public ImportStepsCommandValidator()
        {
           
           RuleFor(v => v.Data)
                .NotNull()
                .NotEmpty();

        }
}

