using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.UserDtos
{
    public class UserDto : LoginUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        public string PhoneNumber { get; set; }

        
    }
}
