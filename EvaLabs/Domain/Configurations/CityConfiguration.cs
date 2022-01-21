using System.Collections.Generic;
using EvaLabs.Domain.Configurations.Base;
using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaLabs.Domain.Configurations
{
    public class CityConfiguration : ConfigurationBase<City>
    {
        public override void ConfigureEntity(EntityTypeBuilder<City> builder)
        {
            builder.HasData(new List<City>
            {
                new() {Id = 1, CityName = "Alexandria", IsActive = true},
                new() {Id = 2, CityName = "Aswan", IsActive = true},
                new() {Id = 3, CityName = "Asyut", IsActive = true},
                new() {Id = 4, CityName = "Beheira", IsActive = true},
                new() {Id = 5, CityName = "Beni Suef", IsActive = true},
                new() {Id = 6, CityName = "Cairo", IsActive = true},
                new() {Id = 7, CityName = "Dakahlia", IsActive = true},
                new() {Id = 8, CityName = "Damietta", IsActive = true},
                new() {Id = 9, CityName = "Faiyum", IsActive = true},
                new() {Id = 10, CityName = "Gharbia", IsActive = true},
                new() {Id = 11, CityName = "Giza", IsActive = true},
                new() {Id = 12, CityName = "Ismailia", IsActive = true},
                new() {Id = 13, CityName = "Kafr El Sheikh", IsActive = true},
                new() {Id = 14, CityName = "Luxor", IsActive = true},
                new() {Id = 15, CityName = "Matruh", IsActive = true},
                new() {Id = 16, CityName = "Minya", IsActive = true},
                new() {Id = 17, CityName = "Monufia", IsActive = true},
                new() {Id = 18, CityName = "New Valley", IsActive = true},
                new() {Id = 19, CityName = "North Sinai", IsActive = true},
                new() {Id = 20, CityName = "Port Said", IsActive = true},
                new() {Id = 21, CityName = "Qalyubia", IsActive = true},
                new() {Id = 22, CityName = "Qena", IsActive = true},
                new() {Id = 23, CityName = "Red Sea", IsActive = true},
                new() {Id = 24, CityName = "Sharqia", IsActive = true},
                new() {Id = 25, CityName = "Sohag", IsActive = true},
                new() {Id = 26, CityName = "South Sinai", IsActive = true},
                new() {Id = 27, CityName = "Suez", IsActive = true},
            });
        }
    }
}