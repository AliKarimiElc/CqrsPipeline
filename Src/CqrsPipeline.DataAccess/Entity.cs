namespace CqrsPipeline.DataAccess
{
    public abstract class Entity<T> : IAuditable where T : IEquatable<T>
    {
        private int? _requestedHashCode;
        public T Id { get; set; }

        private bool IsTransient()
        {
            return EqualityComparer<T>.Default.Equals(Id, default);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Entity<T> item))
                return false;

            if (ReferenceEquals(this, item))
                return true;

            if (GetType() != item.GetType())
                return false;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return EqualityComparer<T>.Default.Equals(item.Id, Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                _requestedHashCode ??= this.Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }

        public static bool operator == (Entity<T> left, Entity<T>? right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator != (Entity<T> left, Entity<T> right)
        {
            return !(left == right);
        }
    }
}