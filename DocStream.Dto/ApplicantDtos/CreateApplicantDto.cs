using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.ApplicantDtos
{
    public class CreateApplicantDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Applicant Name Is Too Long")]
        public string ApplicantName { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "The Address Name Is Too Long")]
        public string PhyscialAddress { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "The Address Name Is Too Long")]
        public string PostalAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int ApplicantLegalStatusId { get; set; }
        [Required]
        public int ProposedBussinessNameId { get; set; }
    }
}
