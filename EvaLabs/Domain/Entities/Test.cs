using System.Collections.Generic;
using EvaLabs.Domain.Models;

namespace EvaLabs.Domain.Entities
{
    public class Test : Auditable
    {
        public Test()
        {
            Branchs = new HashSet<TestBranchs>();
            UserTests = new HashSet<UserTest>();
        }

        public string TestName { get; set; }
        public string TestDetails { get; set; }
        public decimal Price { get; set; }
        public bool AtHome { get; set; }

        public ICollection<TestBranchs> Branchs { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}
