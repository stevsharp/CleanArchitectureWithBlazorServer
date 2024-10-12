// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class Allergy : BaseAuditableEntity
{
    public string? AllergyType { get; set; }
    public string? Comments { get; set; }

    public int PatientId { get; set; }
}