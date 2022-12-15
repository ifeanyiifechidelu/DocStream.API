using DocStream.Core.Token;
using DocStream.Dtos.UserDtos;
using DocStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Core.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDto userDTO);
        Task<string> CreateToken(ApplicationUser user);
        Task<string> CreateRefreshToken();
        Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    }
}
