// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;


public class Supplier : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? VAT { get; set; } // New property for VAT
    public string? Country { get; set; } // New property for Country
    public string? CompanyType {get; set; } // New property for CompanyType
    public string? IBAN { get;set; }
    public string? SWIFT { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public string? PublicFinancialService { get; set; }
    public string? ContactPerson { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }

    public List<SupplyItem>? SupplyItems { get; set; }

    public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = [];

}