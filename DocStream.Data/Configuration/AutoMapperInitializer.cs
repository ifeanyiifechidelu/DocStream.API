using AutoMapper;
using DocStream.Dtos.ApplicantDtos;
using DocStream.Dtos.ApplicantLegalStatusDtos;
using DocStream.Dtos.BankerDtos;
using DocStream.Dtos.ContactPersonDtos;
using DocStream.Dtos.DirectorDtos;
using DocStream.Dtos.ProposedBusinessNameDtos;
using DocStream.Dtos.ShareholderDtos;
using DocStream.Dtos.UserDtos;
using DocStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Data.Configuration
{
    public class AutoMapperInitializer : Profile
    {
        public AutoMapperInitializer()
        {
            CreateMap<Applicant, ApplicantDto>().ReverseMap();
            CreateMap<Applicant, CreateApplicantDto>().ReverseMap();
            CreateMap<ApplicantLegalStatus, ApplicantLegalStatusDto>().ReverseMap();
            CreateMap<ApplicantLegalStatus, CreateApplicantLegalStatusDto>().ReverseMap();
            CreateMap<Banker, BankerDto>().ReverseMap();
            CreateMap<Banker, CreateBankerDto>().ReverseMap();
            CreateMap<ContactPerson, ContactPersonDto>().ReverseMap();
            CreateMap<ContactPerson, CreateContactPersonDto>().ReverseMap();
            CreateMap<Director, DirectorDto>().ReverseMap();
            CreateMap<Director, CreateDirectorDto>().ReverseMap();
            CreateMap<ProposedBusinessName, ProposedBusinessNameDto>().ReverseMap();
            CreateMap<ProposedBusinessName, CreateProposedBusinessNameDto>().ReverseMap();
            CreateMap<Shareholder, ShareholderDto>().ReverseMap();
            CreateMap<Shareholder, CreateShareholderDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
