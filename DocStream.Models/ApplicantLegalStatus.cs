using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Models
{
    public class ApplicantLegalStatus
    {
        [Key]
        public int Id { get; set; }

        public string BusinessLegalStatus { get; set; }
        [ForeignKey(nameof(Applicant))]
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
    }
}
