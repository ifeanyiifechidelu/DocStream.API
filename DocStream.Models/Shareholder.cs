using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Models
{
    public class Shareholder
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Nationality { get; set; }
        public string CountryOfUsualResidency { get; set; }
        public string PhysicalAddress { get; set; }
        public string ConvictionStatus { get; set; }
        [ForeignKey(nameof(Applicant))]
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
    }
}
