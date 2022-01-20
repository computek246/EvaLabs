using System;

namespace EvaLabs.Domain.Models.Interfaces
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}