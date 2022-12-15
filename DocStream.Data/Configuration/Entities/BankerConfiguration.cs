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
    public class BankerConfiguration : IEntityTypeConfiguration<Banker>
    {
        public void Configure(EntityTypeBuilder<Banker> builder)
        {
            builder.HasData(
                new Banker
                {
                    Id = 1,
                    BankName = "Guarantee Trust Bank",
                    Email = "gtb@gtbhq.com",
                    PhoneNumber = "+2347893027493",
                    Address = "13 Marina, Lagos Island, Lagos"
                },
                new Banker
                {
                    Id = 2,
                    BankName = "First Bank",
                    Email = "firstbank@firsthq.com",
                    PhoneNumber = "+2347856027493",
                    Address = "78 Marina, Lagos Island, Lagos"
                }
                );
        }
    }
}
