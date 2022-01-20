using System;
using EvaLabs.Domain.Models.Interfaces;

namespace EvaLabs.Domain.Models
{
    public class Auditable : Auditable<int>
    {
    }


    public class Auditable<TForeignKey>
        : BaseEntity, IAuditable<TForeignKey>
    {
        public bool IsActive { get; set; }

        public TForeignKey CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public TForeignKey ModifierId { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}