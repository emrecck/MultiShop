namespace MultiShop.Cargo.Domain.Entites
{
    public interface IBaseEntity<T> : IAuditableEntity, ISoftDeletable
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
    }
}
