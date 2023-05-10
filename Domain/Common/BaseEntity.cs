using System;

namespace Domain.Common
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}