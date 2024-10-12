namespace CleanArchitecture.Blazor.Application.Features.Allergies.Specifications;

#nullable disable warnings
/// <summary>
/// Specifies the different views available for the Allergy list.
/// </summary>
public enum AllergyListView
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
/// A class for applying advanced filtering options to Allergy lists.
/// </summary>
public class AllergyAdvancedFilter: PaginationFilter
{
    public TimeSpan LocalTimezoneOffset { get; set; }
    public AllergyListView ListView { get; set; } = AllergyListView.All;
    public UserProfile? CurrentUser { get; set; }
}