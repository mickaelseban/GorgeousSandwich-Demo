namespace Domain.SeedWork
{
    public abstract class Entity
    {
        public virtual long Id { get; protected set; }
        protected virtual object Actual => this;

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() != other.Actual.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode() => (Actual.GetType().ToString() + Id).GetHashCode();
    }
}