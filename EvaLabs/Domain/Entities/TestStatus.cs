using System.Collections.Generic;
using EvaLabs.Domain.Models;

namespace EvaLabs.Domain.Entities
{
    public class TestStatus : Auditable
    {
        public TestStatus()
        {
            UserTests = new HashSet<UserTest>();
        }

        public string StatusName { get; set; }

        public ICollection<UserTest> UserTests { get; set; }
    }
}
