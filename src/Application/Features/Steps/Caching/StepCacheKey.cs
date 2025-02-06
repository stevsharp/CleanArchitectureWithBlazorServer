
namespace CleanArchitecture.Blazor.Application.Features.Steps.Caching;
/// <summary>
/// Static class for managing cache keys and expiration for Step-related data.
/// </summary>
public static class StepCacheKey
{
    public const string GetAllCacheKey = "all-Steps";
    public static string GetPaginationCacheKey(string parameters) {
        return $"StepCacheKey:StepsWithPaginationQuery,{parameters}";
    }
    public static string GetExportCacheKey(string parameters) {
        return $"StepCacheKey:ExportCacheKey,{parameters}";
    }
    public static string GetByNameCacheKey(string parameters) {
        return $"StepCacheKey:GetByNameCacheKey,{parameters}";
    }
    public static string GetByIdCacheKey(string parameters) {
        return $"StepCacheKey:GetByIdCacheKey,{parameters}";
    }

    public static string GetCommentByStepIdCacheKey(string parameters){
        return $"StepCommentCacheKey:GetCommentByIdCacheKey,{parameters}";
    }

    public static IEnumerable<string>? Tags => new string[] { "step" };
    public static void Refresh()
    {
        FusionCacheFactory.RemoveByTags(Tags);
    }
}

