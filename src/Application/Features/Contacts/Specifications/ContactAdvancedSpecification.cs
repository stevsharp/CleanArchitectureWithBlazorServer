
namespace CleanArchitecture.Blazor.Application.Features.Contacts.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for advanced filtering of Contacts.
/// </summary>
public class ContactAdvancedSpecification : Specification<Contact>
{
    public ContactAdvancedSpecification(ContactAdvancedFilter filter)
    {
        DateTime today = DateTime.UtcNow;
        var todayrange = today.GetDateRange(ContactListView.TODAY.ToString(), filter.LocalTimezoneOffset);
        var last30daysrange = today.GetDateRange(ContactListView.LAST_30_DAYS.ToString(),filter.LocalTimezoneOffset);

        Query.Where(x => x.TaxIdentificationNumber != null)
                  .Where(x => x.TaxIdentificationNumber.Contains(filter.Keyword) || x.Description.Contains(filter.Keyword) ||
                              x.PublicFinancialService.Contains(filter.Keyword), !string.IsNullOrEmpty(filter.Keyword))
                  .Where(x => x.TaxIdentificationNumber.Contains(filter.TaxIdentificationNumber), !string.IsNullOrEmpty(filter.TaxIdentificationNumber))
                  .Where(x => x.PublicFinancialService.Contains(filter.PublicFinancialService), !string.IsNullOrEmpty(filter.PublicFinancialService))
                   //.Where(filter.Keyword,!string.IsNullOrEmpty(filter.Keyword))
                   .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == ContactListView.My && filter.CurrentUser is not null)
             .Where(x => x.Created >= todayrange.Start && x.Created < todayrange.End.AddDays(1), filter.ListView == ContactListView.TODAY)
             .Where(x => x.Created >= last30daysrange.Start, filter.ListView == ContactListView.LAST_30_DAYS);



    }
}
