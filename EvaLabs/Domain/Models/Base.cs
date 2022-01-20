using EvaLabs.Domain.Models.Interfaces;

namespace EvaLabs.Domain.Models
{
    public abstract class Base : Auditable<int?>, IBase<int>
    {
        public virtual string Name { get; set; }
    }
}