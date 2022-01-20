using System;

namespace EvaLabs.Domain.Models.Interfaces
{
    public interface IAuditable<TForeignKey> : IActiveable, ISoftDeletable
    {
        TForeignKey CreatorId { get; set; }
        DateTime CreationDate { get; set; }
        TForeignKey ModifierId { get; set; }
        DateTime LastModifiedDate { get; set; }
    }
}