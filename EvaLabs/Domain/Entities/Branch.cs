using System.Collections.Generic;
using EvaLabs.Domain.Models;

namespace EvaLabs.Domain.Entities
{
    public class Branch : Auditable
    {
        public Branch()
        {
            Tests = new HashSet<TestBranchs>();
            UserTests = new HashSet<UserTest>();
        }

        public int LabId { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }

        public int AreaId { get; set; }

        public Area Area { get; set; }
        public Lab Lab { get; set; }

        public ICollection<TestBranchs> Tests { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}
