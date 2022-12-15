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
    public class ContactPersonConfiguration : IEntityTypeConfiguration<ContactPerson>
    {
        public void Configure(EntityTypeBuilder<ContactPerson> builder)
        {
            builder.HasData(
                new ContactPerson
                {
                    Id = 1,
                    ContactPersonName = "Ayo Oluwa",
                    PhyscialAddress = "1 Marina, Lagos Island, Lagos",
                    PostalAddress = "13 Marina, Lagos Island, Lagos",
                    MobileNumber = "+2347049375663",
                    Email = "ayooluwa@gmail.com",
                    ApplicantId = 1,
                },
                new ContactPerson
                {
                    Id = 2,
                    ContactPersonName = "Emeka Okoye",
                    PhyscialAddress = "7 Marina, Lagos Island, Lagos",
                    PostalAddress = "7 Marina, Lagos Island, Lagos",
                    MobileNumber = "+2347047375663",
                    Email = "emekaokoye@gmail.com",
                    ApplicantId = 2,
                }
                );
        }
    }
}
