using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaLabs.ViewModels.Test
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string TestDetails { get; set; }
        public decimal Price { get; set; }
        public bool AtHome { get; set; }
    }
}
