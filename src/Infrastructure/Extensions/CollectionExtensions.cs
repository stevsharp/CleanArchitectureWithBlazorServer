
namespace CleanArchitecture.Blazor.Infrastructure.Extensions;

public static class CollectionExtensions
{
    public static void SyncWith<TTarget, TSource>(
        this IList<TTarget> target,
        IEnumerable<TSource> source,
        Func<TTarget, int> targetIdSelector,
        Func<TSource, int> sourceIdSelector,
        Action<TTarget, TSource> updateAction,
        Func<TSource, TTarget> createNew)
    {
        var sourceList = source.ToList();
        var sourceIds = sourceList.Select(sourceIdSelector).ToHashSet();

        // Remove items not in source
        for (int i = target.Count - 1; i >= 0; i--)
        {
            if (!sourceIds.Contains(targetIdSelector(target[i])))
            {
                target.RemoveAt(i);
            }
        }

        foreach (var sourceItem in sourceList)
        {
            var existing = target.FirstOrDefault(x => targetIdSelector(x) == sourceIdSelector(sourceItem));
            if (existing is not null)
            {
                updateAction(existing, sourceItem); // Update
            }
            else
            {
                target.Add(createNew(sourceItem)); // Add new
            }
        }
    }
}
