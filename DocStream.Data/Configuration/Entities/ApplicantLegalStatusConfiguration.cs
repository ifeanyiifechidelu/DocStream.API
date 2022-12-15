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
    public class ApplicantLegalStatusConfiguration : IEntityTypeConfiguration<ApplicantLegalStatus>
    {
        public void Configure(EntityTypeBuilder<ApplicantLegalStatus> builder)
        {
            builder.HasData(
               new ApplicantLegalStatus
               {
                   Id = 1,
                   BusinessLegalStatus = "Sole Proprietor",
               },
               new ApplicantLegalStatus
               {
                   Id = 2,
                   BusinessLegalStatus = "Partnership",
               },
               new ApplicantLegalStatus
               {
                   Id = 3,
                   BusinessLegalStatus = "Public Limited Liabilty Company",
               },
               new ApplicantLegalStatus
               {
                   Id = 4,
                   BusinessLegalStatus = "Private Limited Liabliity Company",
               },
               new ApplicantLegalStatus
               {
                   Id = 5,
                   BusinessLegalStatus = "Cooperative Society",
               },
               new ApplicantLegalStatus
               {
                   Id = 6,
                   BusinessLegalStatus = "Other",
               }
               );
        }
    }
}
