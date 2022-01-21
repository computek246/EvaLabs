using System;
using EvaLabs.Domain.Models;

namespace EvaLabs.Domain.Entities
{
    public class UserTest : Auditable
    {
        public int UserId { get; set; }
        public int LabId { get; set; }
        public int? BranchId { get; set; }
        public int TestId { get; set; }
        public int TestStatusId { get; set; }
        public int TestLocation { get; set; }
        public DateTime TestDate { get; set; }
        public DateTime ResultDate { get; set; }

        public int? CityId { get; set; }
        public int? AreaId { get; set; }
        public string HomeAddress { get; set; }

        public User User { get; set; }
        public Lab Lab { get; set; }
        public Branch Branch { get; set; }
        public Test Test { get; set; }
        public City City { get; set; }
        public Area Area { get; set; }
        public TestStatus TestStatus { get; set; }
        public TestResult TestResult { get; set; }
    }
}
