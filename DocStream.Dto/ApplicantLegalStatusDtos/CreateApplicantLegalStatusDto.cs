using System.ComponentModel.DataAnnotations;

namespace DocStream.Dtos.ApplicantLegalStatusDtos
{
    public class CreateApplicantLegalStatusDto
    {
        [Required]
        public string BusinessLegalStatus { get; set; }
        [Required]
        public int ApplicantId { get; set; }
    }
}