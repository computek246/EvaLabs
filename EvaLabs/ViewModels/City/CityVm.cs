using System.Linq;
using EvaLabs.ViewModels.Common.Helper;

namespace EvaLabs.ViewModels.City
{
    public class CityVm : ErrorValidator
    {

        public int Id { get; set; }
        public int RegionId { get; set; }
        public int CountryId { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string CountryName { get; set; }

        protected override bool Validate()
        {
            return new CityVmValidator().CustomizedValidate(this).ModelValidatorList().Any();
        }
    }
}