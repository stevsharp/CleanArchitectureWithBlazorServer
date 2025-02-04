
namespace CleanArchitecture.Blazor.Application.Features.Steps.Specifications;
#nullable disable warnings
/// <summary>
/// Specification class for advanced filtering of Steps.
/// </summary>
public class StepAdvancedSpecification : Specification<Step>
{
    public StepAdvancedSpecification(StepAdvancedFilter filter)
    {
        DateTime today = DateTime.UtcNow;
        var todayrange = today.GetDateRange(StepListView.TODAY.ToString(), filter.LocalTimezoneOffset);
        var last30daysrange = today.GetDateRange(StepListView.LAST_30_DAYS.ToString(),filter.LocalTimezoneOffset);

        Query.Where(q => q.Name != null)
             .Where(filter.Keyword,!string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == StepListView.My && filter.CurrentUser is not null)
             .Where(x => x.Created >= todayrange.Start && x.Created < todayrange.End.AddDays(1), filter.ListView == StepListView.TODAY)
             .Where(x => x.Created >= last30daysrange.Start, filter.ListView == StepListView.LAST_30_DAYS);
       
    }
}
