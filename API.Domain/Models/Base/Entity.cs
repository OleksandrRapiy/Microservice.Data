namespace API.Domain.Models.Base
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; private set; }
    }
}
