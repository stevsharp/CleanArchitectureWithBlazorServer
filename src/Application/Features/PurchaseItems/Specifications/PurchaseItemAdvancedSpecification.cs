

namespace CleanArchitecture.Blazor.Application.Features.PurchaseItems.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for advanced filtering of PurchaseItems.
/// </summary>
public class PurchaseItemAdvancedSpecification : Specification<PurchaseItem>
{
    public PurchaseItemAdvancedSpecification(PurchaseItemAdvancedFilter filter)
    {
        //DateTime today = DateTime.UtcNow;
        //var todayrange = today.GetDateRange(PurchaseItemListView.TODAY.ToString(), filter.LocalTimezoneOffset);
        //var last30daysrange = today.GetDateRange(PurchaseItemListView.LAST_30_DAYS.ToString(),filter.LocalTimezoneOffset);

        //Query.Where(q => q.Name != null)
        //     .Where(filter.Keyword,!string.IsNullOrEmpty(filter.Keyword))
        //     .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == PurchaseItemListView.My && filter.CurrentUser is not null)
        //     .Where(x => x.Created >= todayrange.Start && x.Created < todayrange.End.AddDays(1), filter.ListView == PurchaseItemListView.TODAY)
        //     .Where(x => x.Created >= last30daysrange.Start, filter.ListView == PurchaseItemListView.LAST_30_DAYS);
       
    }
}
