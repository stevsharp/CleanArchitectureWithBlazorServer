

using CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.PurchaseItems.DTOs;

[Description("PurchaseItems")]
public record PurchaseItemDto
{
    [Description("Id")]
    public int Id { get; set; }
        [Description("Invoice id")]
    public int InvoiceId {get;set;} 
    [Description("Item code")]
    public string? ItemCode {get;set;} 
    [Description("Item description")]
    public string? ItemDescription {get;set;} 
    [Description("Quantity")]
    public int Quantity {get;set;} 
    [Description("Unit")]
    public string? Unit {get;set;} 
    [Description("Color")]
    public string? Color {get;set;} 
    [Description("Unit price")]
    public decimal UnitPrice {get;set;} 
    [Description("Vat percentage")]
    public decimal VATPercentage {get;set;} 
    [Description("Vat amount")]
    public decimal VATAmount {get;set;} 
    [Description("Total amount")]
    public decimal TotalAmount {get;set;} 
    //[Description("Invoice")]
    //public PurchaseInvoiceDto Invoice {get;set;} 


}

