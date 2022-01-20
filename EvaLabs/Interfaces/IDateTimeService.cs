using System;

namespace EvaLabs.Interfaces
{
    public interface IDateTimeService
    {
        public DateTime Now { get; }
        public DateTime UtcNow { get; }
        public DateTime GetDifference(DateTime fromDateTime, DateTime toDateTime);
    }
}