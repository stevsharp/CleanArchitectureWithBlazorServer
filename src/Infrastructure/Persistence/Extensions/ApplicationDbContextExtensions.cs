
namespace CleanArchitecture.Blazor.Infrastructure;

public static class ApplicationDbContextExtensions
{
    public static void AddOrUpdate<TEntity>(this IApplicationDbContext context, DbSet<TEntity> dbSet, TEntity entity)
        where TEntity : class
    {
        var dbContext = context as ApplicationDbContext;

        if (dbContext == null)
            throw new InvalidOperationException("The context must be a DbContext.");

        if (dbContext.Entry(entity).IsKeySet)
            dbSet.Update(entity);
        else
            dbSet.Add(entity);
    }
}