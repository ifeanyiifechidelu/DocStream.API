using DocStream.FileEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string PhyscialAddress { get; set; }
        public string PostalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Shareholder> shareholders { get; set; }
        public ICollection<Director> Directors { get; set; }
        public ICollection<ContactPerson> contactPeople { get; set; }
        public ICollection<FileDetails> FileDetails { get; set; }
        [ForeignKey(nameof(ApplicantLegalStatus))]
        public int ApplicantLegalStatusId { get; set; }
        public ApplicantLegalStatus ApplicantLegalStatus { get; set; }
        [ForeignKey(nameof(ProposedBusinessName))]

        public int ProposedBussinessNameId { get; set; }
        public ProposedBusinessName ProposedBussinessNames { get; set; }
    }
}