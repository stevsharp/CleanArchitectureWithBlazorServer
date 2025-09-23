using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities;
public sealed class Company : BaseAuditableEntity
{
    public string Name { get; set; } = default!;
    public string? VatNumber { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? Website { get; set; }
    public string? Industry { get; set; }
    public ICollection<Contact> Contacts { get; set; } = [];
}
