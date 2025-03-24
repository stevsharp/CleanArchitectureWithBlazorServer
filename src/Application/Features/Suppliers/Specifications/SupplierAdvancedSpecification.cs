

namespace CleanArchitecture.Blazor.Application.Features.Suppliers.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for advanced filtering of Suppliers.
/// </summary>
public class SupplierAdvancedSpecification : Specification<Supplier>
{
    public SupplierAdvancedSpecification(SupplierAdvancedFilter filter)
    {
        DateTime today = DateTime.UtcNow;
        var todayrange = today.GetDateRange(SupplierListView.TODAY.ToString(), filter.LocalTimezoneOffset);
        var last30daysrange = today.GetDateRange(SupplierListView.LAST_30_DAYS.ToString(),filter.LocalTimezoneOffset);

        Query.Where(q => q.Name != null)
             .Where(x => x.Name.Contains(filter.Name), !string.IsNullOrEmpty(filter.Name))
             .Where(filter.Keyword,!string.IsNullOrEmpty(filter.Keyword))
             .Where(x => x.Country.Contains(filter.Country), !string.IsNullOrEmpty(filter.Country))
             .Where(x => x.TaxIdentificationNumber.Contains(filter.TaxIdentificationNumber), !string.IsNullOrEmpty(filter.TaxIdentificationNumber))
             .Where(x => x.IBAN.Contains(filter.IBAN), !string.IsNullOrEmpty(filter.IBAN))
             .Where(x => x.Email.Contains(filter.Email), !string.IsNullOrEmpty(filter.Email))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == SupplierListView.My && filter.CurrentUser is not null)
             .Where(x => x.Created >= todayrange.Start && x.Created < todayrange.End.AddDays(1), filter.ListView == SupplierListView.TODAY)
             .Where(x => x.Created >= last30daysrange.Start, filter.ListView == SupplierListView.LAST_30_DAYS);
       
    }
}
