namespace CleanArchitecture.Blazor.Application.Features.Allergies.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for advanced filtering of Allergies.
/// </summary>
public class AllergyAdvancedSpecification : Specification<Allergy>
{
    public AllergyAdvancedSpecification(AllergyAdvancedFilter filter)
    {
        DateTime today = DateTime.UtcNow;
        var todayrange = today.GetDateRange(AllergyListView.TODAY.ToString(), filter.LocalTimezoneOffset);
        var last30daysrange = today.GetDateRange(AllergyListView.LAST_30_DAYS.ToString(),filter.LocalTimezoneOffset);

        Query.Where(q => q.AllergyType != null)
             .Where(filter.Keyword,!string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == AllergyListView.My && filter.CurrentUser is not null)
             .Where(x => x.Created >= todayrange.Start && x.Created < todayrange.End.AddDays(1), filter.ListView == AllergyListView.TODAY)
             .Where(x => x.Created >= last30daysrange.Start, filter.ListView == AllergyListView.LAST_30_DAYS);
       
    }
}
