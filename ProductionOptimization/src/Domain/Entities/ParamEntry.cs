using Domain.Common;

namespace Domain.Entities
{
    public class ParamEntry:AuditableEntity
    {
        public virtual string Name { get; set; }
        public virtual double Value { get; set;  }
        public virtual string Symbol { get; set; }
    }
}
