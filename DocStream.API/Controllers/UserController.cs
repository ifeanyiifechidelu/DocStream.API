using AutoMapper;
using DocStream.Core.Interfaces;
using DocStream.Core.Services;
using DocStream.Dtos.UserDtos;
using DocStream.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DocStream.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAuthManager _authManager;

        public UserController(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ILogger<UserController> logger,
                IMapper mapper,
                IEmailSender emailSender,
                IAuthManager authManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _emailSender = emailSender;
            _authManager = authManager;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Post: AddUser
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Registration attempt for {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApplicationUser>(userDto);
                user.UserName = userDto.Email;
                user.EmailConfirmed = false;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    return BadRequest($"User Registration Attempt Failed");
                }
               
                    //var token = _authManager.CreateToken(user);

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var callbackurl = Url.Action("ConfirmEmail", "User", new { userId = user.Id, token = code }, protocol: HttpContext.Request.Scheme);


                await _emailSender.SendEmailAsync(userDto.Email, "Confirm Email - Identity Manager", "Please confirm your email by clicking here: <a href=\"" + callbackurl + "\">link</a>");

                return Ok("Check your email to confirm your email");
                
                 

                
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something Went wrong in the {nameof(Register)}", statusCode: 500);
            }


        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();

            code = Encoding.UTF8.GetString(Convert.FromBase64String(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            var status = result.Succeeded
                ? "Thank you for confirming your email"
                : "your email is not confirmed, please try again later";

            return Ok(status);
        }


        // <summary>
        // 
        // </summary>
        // <param name = "id" ></ param >
        // < returns ></ returns >

        // Post: LoginUser

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginUserDto userDto)
        {
            _logger.LogInformation($"Login attempt for {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByEmailAsync(userDto.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, userDto.Password))
                    return Unauthorized();

                if (!user.EmailConfirmed)
                {
                    return BadRequest($"Email needs to be confirmed");
                }

                return new TokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token = await _authManager.CreateToken(user)
                };


            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something Went Wrong in the {nameof(Login)}");
                return Problem($"Something Went wrong in the {nameof(Login)}", statusCode: 500);
            }


        }
        // Logou
        [HttpPost("userLogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // GET: AllUsers
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {

            try
            {
                var users = _userManager.Users.ToList();
                return Ok(users);

            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(GetAllUsers)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }
        // get single user
        //[Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<TokenDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return new TokenDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _authManager.CreateToken(user)
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Put: UpdateUser
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Microsoft.AspNetCore.Mvc.HttpPut("[controller]/UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Update attempt for {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ApplicationUser>(userDto);
                user.UserName = userDto.Email;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest($"User Update Attempt Failed");
                }

                return Accepted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(UpdateUser)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Microsoft.AspNetCore.Mvc.HttpPut("[controller]/UpdateUserPassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto userDto)
        {
            _logger.LogInformation($"Update Password attempt for {userDto}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                //var user = _mapper.Map<ApiUser>(userDto);
                var user = await _userManager.FindByEmailAsync(userDto.Email);
                if (user.Email != userDto.Email)
                {
                    _logger.LogInformation($"Check the credentials and try again");
                    return BadRequest();
                }

                var result = await _userManager.ChangePasswordAsync(user, userDto.CurrentPassword, userDto.NewPassword);

                if (!result.Succeeded)
                {
                    return BadRequest($"Password Update Attempt Failed");
                }

                return Accepted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(UpdatePassword)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackurl = Url.Action("ResetPassword", "User", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(model.Email, "Reset Password - Identity Manager", "Please reset your password by clicking here: <a href=\"" + callbackurl + "\">link</a>");
            return Ok("Check your email to reset your password");
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {

            var errorResult = new StringBuilder();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return Ok("Password Reset Successfully");
                }

                foreach (var item in result.Errors)
                {
                    errorResult.AppendLine(item.Description);
                }
            }

            return BadRequest(errorResult.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        // Delete: DeleteUser
        //[Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Microsoft.AspNetCore.Mvc.HttpDelete("[controller]/DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = _mapper.Map<ApplicationUser>(id);
                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest($"User Update Attempt Failed");
                }

                return Accepted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(UpdateUser)}");
                return StatusCode(500, "Internal Server Error. Please try Again Later.");
            }
        }
    }

    //private void SendEmail(string body, string email)
    //{

    //}
}
