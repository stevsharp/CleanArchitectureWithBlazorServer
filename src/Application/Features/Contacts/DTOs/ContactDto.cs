

namespace CleanArchitecture.Blazor.Application.Features.Contacts.DTOs;

[Description("Contacts")]
public class ContactDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]

    public string? Name {get;set;} 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Phone number")]
    public string? PhoneNumber {get;set;} 
    [Description("Country")]
    public string? Country {get;set;}

    [Description("Address")]
    public string? Address { get; set; }

    [Description("City")]
    public string? City { get; set; }

    [Description("Tax Identification Number")]
    public string? TaxIdentificationNumber { get;set; }

    [Description("Public Financial Service")]
    public string? PublicFinancialService { get; set; }

    [Description("Customer Type")]
    public int? CustomerType { get; set; } = 0;
}

