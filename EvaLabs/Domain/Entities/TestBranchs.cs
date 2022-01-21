using EvaLabs.Domain.Models;

namespace EvaLabs.Domain.Entities
{
    public class TestBranchs : Auditable
    {
        public int TestId { get; set; }
        public int BranchId { get; set; }

        public Test Test { get; set; }
        public Branch Branch { get; set; }
    }
}
