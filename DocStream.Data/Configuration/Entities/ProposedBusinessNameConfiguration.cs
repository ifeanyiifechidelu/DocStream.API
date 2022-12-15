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
    public class ProposedBusinessNameConfiguration : IEntityTypeConfiguration<ProposedBusinessName>
    {

        public void Configure(EntityTypeBuilder<ProposedBusinessName> builder)
        {
            builder.HasData(
                new ProposedBusinessName
                {
                    Id = 1,
                    Name = "High Plastics",
                    Location = "Lagos",
                    PhoneNumber = "+2347893027983",
                    Email = "highplastics@highplastic.com",
                    PostalAddress = "13 Marina, Lagos Island, Lagos",
                    ApplicantId = 1
                },
                new ProposedBusinessName
                {
                    Id = 2,
                    Name = "Tastey Bakery",
                    Location = "Lagos",
                    PhoneNumber = "+2348093027983",
                    Email = "tasteybakery@tasteybakery.com",
                    PostalAddress = "100 Marina, Lagos Island, Lagos",
                    ApplicantId = 2
                }
                );
        }
    }
}
