using EvaLabs.Domain.Models.Interfaces;

namespace EvaLabs.Domain.Models
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
    }
}