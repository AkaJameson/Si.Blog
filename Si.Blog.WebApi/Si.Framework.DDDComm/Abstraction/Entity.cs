namespace Si.Framework.DDDComm.Abstraction
{
    public abstract class Entity<TId> where TId : IEquatable<TId>
    {
        public TId Id { get; protected set; }

        public override bool Equals(object? obj)
        {
            if (obj is Entity<TId> entity)
            {
                return EqualityComparer<TId>.Default.Equals(Id, entity.Id);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return left?.Equals(right) ?? right is null;
        }
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }

    }
}
