
namespace CleanArchitecture.Blazor.Application.Features.PurchaseInvoices.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for advanced filtering of PurchaseInvoices.
/// </summary>
public class PurchaseInvoiceAdvancedSpecification : Specification<PurchaseInvoice>
{
    public PurchaseInvoiceAdvancedSpecification(PurchaseInvoiceAdvancedFilter filter)
    {
        DateTime today = DateTime.UtcNow;
        var todayrange = today.GetDateRange(PurchaseInvoiceListView.TODAY.ToString(), filter.LocalTimezoneOffset);
        var last30daysrange = today.GetDateRange(PurchaseInvoiceListView.LAST_30_DAYS.ToString(),filter.LocalTimezoneOffset);

        Query.Where(q => q.InvoiceNumber != null)
             .Where(x => x.InvoiceNumber.Contains(filter.InvoiceNumber), !string.IsNullOrEmpty(filter.InvoiceNumber))
             .Where(filter.Keyword,!string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == PurchaseInvoiceListView.My && filter.CurrentUser is not null)
             .Where(x => x.Created >= todayrange.Start && x.Created < todayrange.End.AddDays(1), filter.ListView == PurchaseInvoiceListView.TODAY)
             .Where(x => x.Created >= last30daysrange.Start, filter.ListView == PurchaseInvoiceListView.LAST_30_DAYS);
       
    }
}
