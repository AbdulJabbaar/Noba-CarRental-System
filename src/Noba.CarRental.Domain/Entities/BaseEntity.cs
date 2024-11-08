namespace Noba.CarRental.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        protected BaseEntity() { }
    }
}
