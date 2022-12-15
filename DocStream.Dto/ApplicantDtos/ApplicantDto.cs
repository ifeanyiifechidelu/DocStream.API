
using DocStream.Dtos.BankerDtos;
using DocStream.Dtos.ContactPersonDtos;
using DocStream.Dtos.DirectorDtos;
using DocStream.Dtos.ProposedBusinessNameDtos;
using DocStream.Dtos.ShareholderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.ApplicantDtos
{
    public class ApplicantDto : CreateApplicantDto
    {
        public int Id { get; set; }
        public ICollection<ShareholderDto> shareholders { get; set; }
        public ICollection<DirectorDto> Directors { get; set; }
        public ICollection<ContactPersonDto> contactPeople { get; set; }

        public ProposedBusinessNameDto ProposedBussinessNames { get; set; }
    }
}
