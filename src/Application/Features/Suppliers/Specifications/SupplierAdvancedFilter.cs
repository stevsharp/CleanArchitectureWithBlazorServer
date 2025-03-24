

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Specifications;

#nullable disable warnings
/// <summary>
/// Specifies the different views available for the Supplier list.
/// </summary>
public enum SupplierListView
{
    [Description("All")]
    All,
    [Description("My")]
    My,
    [Description("Created Toady")]
    TODAY,
    [Description("Created within the last 30 days")]
    LAST_30_DAYS
}
/// <summary>
/// A class for applying advanced filtering options to Supplier lists.
/// </summary>
public class SupplierAdvancedFilter: PaginationFilter
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Email { get; set; }
    public string? IBAN { get; set; }
    public string? TaxIdentificationNumber { get; set; }

    public TimeSpan LocalTimezoneOffset { get; set; }
    public SupplierListView ListView { get; set; } = SupplierListView.All;
    public UserProfile? CurrentUser { get; set; }
}