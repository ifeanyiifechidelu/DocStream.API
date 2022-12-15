using DocStream.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Data.Configuration.Entities
{
    partial class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.HasData(
                new Director
                {
                    Id = 1,
                    Name = "Ayo Oluwa",
                    Nationality = "Nigerian",
                    CountryOfUsualResidency = "Nigeria",
                    PhysicalAddress = "13 Marina, Lagos Island, Lagos",
                    ConvictionStatus = "None",
                    ApplicantId = 1
                },
                new Director
                {
                    Id=2,
                    Name = "Emeka Okoye",
                    Nationality = "Nigerian",
                    CountryOfUsualResidency = "Nigeria",
                    PhysicalAddress = "54 Marina, Lagos Island, Lagos",
                    ConvictionStatus = "None",
                    ApplicantId = 2
                }

                );
        }
    }
}
