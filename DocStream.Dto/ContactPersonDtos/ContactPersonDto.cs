using DocStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.ContactPersonDtos
{
    public class ContactPersonDto : CreateContactPersonDto
    {
        public int Id { get; set; }
        public Applicant Applicant { get; set; }
    }
}
