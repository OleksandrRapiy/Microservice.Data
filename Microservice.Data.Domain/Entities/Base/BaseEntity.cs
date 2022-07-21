namespace Microservice.Data.Domain.Entites.Base
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; private set; }
    }
}
