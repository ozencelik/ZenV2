using System;

namespace Zen.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; protected set; } = DateTime.Now;
    }
}
