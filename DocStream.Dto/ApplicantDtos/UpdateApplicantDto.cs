using DocStream.Dtos.ContactPersonDtos;
using DocStream.Dtos.DirectorDtos;
using DocStream.Dtos.ShareholderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.ApplicantDtos
{
    public class UpdateApplicantDto : CreateApplicantDto
    {
        public ICollection<CreateShareholderDto> shareholders { get; set; }
        public ICollection<CreateDirectorDto> Directors { get; set; }
        public ICollection<CreateContactPersonDto> contactPeople { get; set; }
    }
}
