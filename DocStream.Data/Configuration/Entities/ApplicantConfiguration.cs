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
    public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasData(
                new Applicant
                {
                    Id = 1,
                    ApplicantName = "Bonglis Group",
                    PhyscialAddress = "13 Marina, Lagos Island, Lagos",
                    PostalAddress = "13 Marina, Lagos Island, Lagos",
                    MobileNumber = "+2347049375663",
                    Email = "bonglisgroup@bonglishq.com",
                    ApplicantLegalStatusId = 1,
                    ProposedBussinessNameId = 1,
                },
                new Applicant
                {
                    Id = 2,
                    ApplicantName = "Nandi Enterprise",
                    PhyscialAddress = "59 Marina, Lagos Island, Lagos",
                    PostalAddress = "59 Marina, Lagos Island, Lagos",
                    MobileNumber = "+2347064375663",
                    Email = "nandienterprise@nandihq.com",
                    ApplicantLegalStatusId = 2,
                    ProposedBussinessNameId = 2,
                }
                );
        }
    }
}
