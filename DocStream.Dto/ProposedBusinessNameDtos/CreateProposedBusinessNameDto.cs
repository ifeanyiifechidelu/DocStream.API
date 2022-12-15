using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.ProposedBusinessNameDtos
{
    public class CreateProposedBusinessNameDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Applicant Name Is Too Long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "The Location Name Is Too Long")]
        public string Location { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "The Address Name Is Too Long")]
        public string PostalAddress { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int ApplicantId { get; set; }
    }
}
