// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Allergies.Commands.Create;

public class CreateAllergyCommandValidator : AbstractValidator<CreateAllergyCommand>
{
        public CreateAllergyCommandValidator()
        {
                RuleFor(v => v.AllergyType).MaximumLength(255); 
    RuleFor(v => v.Comments).MaximumLength(255); 
    RuleFor(v => v.PatientId).NotNull(); 

        }
       
}

