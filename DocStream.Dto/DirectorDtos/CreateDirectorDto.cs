using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.DirectorDtos
{
    public class CreateDirectorDto
    {
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "The Name Is Too Long")]
        public String Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "The Name Is Too Long")]
        public String Nationality { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "The Address Name Is Too Long")]
        public string CountryOfUsualResidency { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "The Address Name Is Too Long")]
        public string PhysicalAddress { get; set; }
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "The Address Name Is Too Long")]
        public string ConvictionStatus { get; set; }
        [Required]
        public int ApplicantId { get; set; }
    }
}
