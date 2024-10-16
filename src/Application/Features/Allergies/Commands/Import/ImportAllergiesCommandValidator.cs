﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Commands.Import;

public class ImportAllergiesCommandValidator : AbstractValidator<ImportAllergiesCommand>
{
        public ImportAllergiesCommandValidator()
        {
           
           RuleFor(v => v.Data)
                .NotNull()
                .NotEmpty();

        }
}

