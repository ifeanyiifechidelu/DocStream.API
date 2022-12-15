using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Dtos.UserDtos
{
    public class ResetPasswordDto
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
