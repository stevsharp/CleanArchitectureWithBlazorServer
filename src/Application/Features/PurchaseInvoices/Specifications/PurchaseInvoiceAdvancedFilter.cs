
namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Specifications;

#nullable disable warnings
/// <summary>
/// Specifies the different views available for the PurchaseInvoice list.
/// </summary>
public enum PurchaseInvoiceListView
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
/// A class for applying advanced filtering options to PurchaseInvoice lists.
/// </summary>
public class PurchaseInvoiceAdvancedFilter: PaginationFilter
{
    public TimeSpan LocalTimezoneOffset { get; set; }
    public PurchaseInvoiceListView ListView { get; set; } = PurchaseInvoiceListView.All;
    public UserProfile? CurrentUser { get; set; }

    public string InvoiceNumber { get; set; }   
}