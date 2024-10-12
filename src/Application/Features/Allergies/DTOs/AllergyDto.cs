// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Allergies.DTOs;

[Description("Allergies")]
public class AllergyDto
{
    [Description("Id")]
    public int Id { get; set; }
        [Description("Allergy type")]
    public string? AllergyType {get;set;} 
    [Description("Comments")]
    public string? Comments {get;set;} 
    [Description("Patient id")]
    public int PatientId {get;set;} 


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Allergy, AllergyDto>().ReverseMap();
        }
    }
}

