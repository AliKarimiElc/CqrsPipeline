using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CqrsPipeline.DataAccess.EntityFramework
{
    /// <summary>
    /// Some extension methods for db context
    /// </summary>
    public static class DbContextExtension
    {
        /// <summary>
        /// Add some properties to each entities that required , such as audit properties
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddAuditingProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(entity => typeof(IAuditable).IsAssignableFrom(entity.ClrType)))
            {
                modelBuilder.Entity(entityType.ClrType).Property<DateTime>("ModifiedTime");
                modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CreatedTime");
                modelBuilder.Entity(entityType.ClrType).Property<string>("CreatedBy");
                modelBuilder.Entity(entityType.ClrType).Property<string>("ModifiedBy");
            }
        }

        /// <summary>
        /// Generate and set values for audit properties
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        public static void ApplyAuditing(this EntityEntry entity, string userId)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property("CreatedTime").CurrentValue = DateTime.UtcNow;
                entity.Property("CreatedBy").CurrentValue = userId;
            }
            if (entity.State == EntityState.Modified)
            {
                entity.Property("ModifiedTime").CurrentValue = DateTime.UtcNow;
                entity.Property("ModifiedBy").CurrentValue = userId;
            }
        }

        /// <summary>
        /// Get the entities that is changed
        /// </summary>
        /// <param name="changeTracker"></param>
        /// <param name="getUnChangedAggregates"></param>
        /// <typeparam name="TId"></typeparam>
        /// <returns></returns>
        public static List<Entity<TId>> GetChangedEntities<TId>(this ChangeTracker changeTracker,
            bool getUnChangedAggregates = false)
            where TId : IEquatable<TId>
        {
            if (!getUnChangedAggregates)
                return changeTracker.Entries<Entity<TId>>()
                    .Where(x => x.State == EntityState.Modified ||
                                x.State == EntityState.Added ||
                                x.State == EntityState.Deleted).Select(c => c.Entity).ToList();
            return changeTracker.Entries<Entity<TId>>().Select(c => c.Entity).ToList();
        }
    }
}