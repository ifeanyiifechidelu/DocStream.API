using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.UserDtos
{
    public class TokenDto
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
    }
}
