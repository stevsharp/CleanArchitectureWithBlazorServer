using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;

public class Contact : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public string? PublicFinancialService { get; set; }
    public List<Offer> Offers { get; set; } = [];
    public virtual ICollection<Invoice> Invoices { get; set; } = [];

}
