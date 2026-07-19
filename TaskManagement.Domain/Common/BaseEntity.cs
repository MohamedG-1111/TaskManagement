namespace TaskManagement.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
