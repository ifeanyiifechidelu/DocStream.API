using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Token;
using DocStream.Dtos.UserDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DocStream.Core.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private ApplicationUser _user;

        private const string _loginProvider = "DocStreamApi";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
            this._logger = logger;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<IEnumerable<IdentityError>> Register(UserDto userDto)
        {
            _user = _mapper.Map<ApplicationUser>(userDto);
            _user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }

        public async Task<TokenRequest> Login(LoginUserDto loginDto)
        {
            _logger.LogInformation($"Looking for user with email {loginDto.Email}");
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if (_user == null || isValidUser == false)
            {
                _logger.LogWarning($"User with email {loginDto.Email} was not found");
                return null;
            }

            var token = await GenerateToken();
            _logger.LogInformation($"Token generated for user with email {loginDto.Email} | Token: {token}");

            return new TokenRequest
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }

        private async Task<string> GenerateToken()
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<TokenRequest> VerifyRefreshToken(TokenRequest request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);

            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new TokenRequest
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
    }
}

//        public async Task<string> CreateToken(ApplicationUser user)
//        {
//            var signingCredentials = GetSigningCredentials();
//            var claims = await GetClaims();
//            var token = GenerateTokenOptions(signingCredentials, claims);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
//        {
//            var jwtSettings = _configuration.GetSection("JwtSettings");
//            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
//                jwtSettings.GetSection("DurationInMinutes").Value));

//            var token = new JwtSecurityToken(
//                issuer: jwtSettings.GetSection("Issuer").Value,
//                claims: claims,
//                expires: expiration,
//                signingCredentials: signingCredentials
//                );

//            return token;
//        }

//        private async Task<List<Claim>> GetClaims()
//        {
//            var claims = new List<Claim>
//             {
//                 new Claim(ClaimTypes.Name, _user.UserName),
//                 new Claim(ClaimTypes.Email, _user.Email)
//             };

//            var roles = await _userManager.GetRolesAsync(_user);

//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            return claims;
//        }

//        private SigningCredentials GetSigningCredentials()
//        {
            
//            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JwtSettings: Key"));

//            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
//        }

//        public async Task<bool> ValidateUser(LoginUserDto userDTO)
//        {
//            _user = await _userManager.FindByNameAsync(userDTO.Email);
//            var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
//            return (_user != null && validPassword);
//        }


//        public async Task<string> CreateRefreshToken()
//        {
//            await _userManager.RemoveAuthenticationTokenAsync(_user, "HotelListingApi", "RefreshToken");
//            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, "HotelListingApi", "RefreshToken");
//            var result = await _userManager.SetAuthenticationTokenAsync(_user, "HotelListingApi", "RefreshToken", newRefreshToken);
//            return newRefreshToken;
//        }

//        public async Task<TokenRequest> VerifyRefreshToken(TokenRequest request)
//        {
//            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
//            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
//            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == ClaimTypes.Name)?.Value;
//            _user = await _userManager.FindByNameAsync(username);
//            try
//            {
//                var isValid = await _userManager.VerifyUserTokenAsync(_user, "HotelListingApi", "RefreshToken", request.RefreshToken);
//                if (isValid)
//                {
//                    return new TokenRequest { Token = await CreateToken(_user), RefreshToken = await CreateRefreshToken() };
//                }
//                await _userManager.UpdateSecurityStampAsync(_user);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//            return null;
//        }
//    }
//
