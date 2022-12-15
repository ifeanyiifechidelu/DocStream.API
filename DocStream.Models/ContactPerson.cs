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
    public class ContactPerson
    {
        [Key]
        public int Id { get; set; }
        public string ContactPersonName { get; set; }
        public string PhyscialAddress { get; set; }
        public string PostalAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        [ForeignKey(nameof(Applicant))]
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }

    }
}
