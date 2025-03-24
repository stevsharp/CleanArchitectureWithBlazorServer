namespace CleanArchitecture.Blazor.Application.Features.Suppliers.DTOs;

[Description("Suppliers")]
public record SupplierDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string? Name {get;set;} 

    [Description("Address")]
    public string? Address {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Vat")]
    public string? VAT {get;set;} 
    [Description("Country")]
    public string? Country {get;set;}

    [Description("CompanyType")]
    public string? CompanyType { get; set; } // New property for CompanyType

    [Description("IBAN")]
    public string? IBAN { get; set; }

    [Description("SWIFT")]
    public string? SWIFT { get; set; }

    [Description("TaxIdentificationNumber")]
    public string? TaxIdentificationNumber { get; set; }

    [Description("PublicFinancialService")]
    public string? PublicFinancialService { get; set; }

    [Description("ContactPerson")]
    public string? ContactPerson { get; set; }

    [Description("Notes")]
    public string? Notes { get; set; }

    [Description("IsActive")]
    public bool IsActive { get; set; }

    [Description("Created at")]
    public DateTime CreatedAt {get;set;} 


}

