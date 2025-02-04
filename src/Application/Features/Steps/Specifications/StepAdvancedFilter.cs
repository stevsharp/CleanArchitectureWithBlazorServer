

namespace CleanArchitecture.Blazor.Application.Features.Steps.Specifications;

#nullable disable warnings
/// <summary>
/// Specifies the different views available for the Step list.
/// </summary>
public enum StepListView
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
/// A class for applying advanced filtering options to Step lists.
/// </summary>
public class StepAdvancedFilter: PaginationFilter
{
    public TimeSpan LocalTimezoneOffset { get; set; }
    public StepListView ListView { get; set; } = StepListView.All;
    public UserProfile? CurrentUser { get; set; }
}