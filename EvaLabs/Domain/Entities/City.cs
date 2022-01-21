using System.Collections.Generic;
using EvaLabs.Domain.Models;

namespace EvaLabs.Domain.Entities
{
    public class City : Auditable
    {
        public City()
        {
            Areas = new HashSet<Area>();
            UserTests = new HashSet<UserTest>();
        }

        public string CityName { get; set; }

        public ICollection<Area> Areas { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}
