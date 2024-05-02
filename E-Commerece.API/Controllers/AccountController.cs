using Core.Identity;
using Core.Interfaces;
using E_Commerece.API.DTOs.Account;
using E_Commerece.API.ExceptionsConfiguration.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;

namespace E_Commerece.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager
                                 , SignInManager<ApplicationUser> signInManager
                                 , ILogger<AccountController> logger
                                 , ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _tokenService = tokenService;
        }


        [Authorize]
        [HttpGet("currentUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]

        public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);

                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return BadRequest(new ApiResponse(404, "Can't Find User Associated With This Email"));
                }

                var currentUserFound = new UserDto
                {
                    DisplayName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                };

                return Ok(currentUserFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during User Finding Proccess.");
                return StatusCode(500, "An error occurred while Trying To Find Current User");

            }
        }

        
        [HttpGet("emailExistence")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]

        public async Task<ActionResult<bool>> CheckForEmailExistenceAsync([FromQuery] string Email)
        {
            try
            {
                
                var emailExistence = await _userManager.FindByEmailAsync(Email);

                if (emailExistence != null)
                {
                    return BadRequest(new ApiResponse(409, "Email Already Registered"));
                }

                else
                {
                    return Ok("You are free To use this Email! Enjoy The Experience"); // Or you can return any other appropriate value indicating email doesn't exist
                }

            }

            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, new ApiResponse(500, "Internal Server Error"));
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    return BadRequest("You Should Provide Exisiting Email & Password To Log into your account");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var checkUserExistence = await _userManager.FindByEmailAsync(loginDto.Email);
                if(checkUserExistence == null)
                {
                    return Unauthorized(new ApiResponse(401 , "You can't Proceed With Your Reuest Password or Email May be Wrong"));
                }

                var result = await _signInManager.CheckPasswordSignInAsync(checkUserExistence , loginDto.Password , false);
                if (!result.Succeeded)
                {
                    return Unauthorized(new ApiResponse(401, "You can't Proceed With Your Reuest Password or Email May be Wrong"));
                }

                var mappedUserResult = new UserDto
                {
                    Email = loginDto.Email,
                    DisplayName = checkUserExistence.UserName,
                    Token = _tokenService.CreateToken(checkUserExistence)
                };

                return Ok(mappedUserResult);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user Login Proccess.");
                return StatusCode(500, "An error occurred while Trying Logging You In. Please try again later!!!");
            }

        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]

        public async Task<ActionResult<UserDto>> RegisterNewUserAsync([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (registerDto == null)
                {
                    return BadRequest("You Should Provide Email and Password In Order To Complete Registeration Prcoess");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (registerDto.Password != registerDto.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return BadRequest(ModelState);
                }


                // var checkForEmailExistence = await _userManager.FindByEmailAsync(registerDto.Email);
                if(CheckForEmailExistenceAsync!=null)
                {
                    return BadRequest(new ApiResponse(409, "Email is lready registered!!! TRY another one"));
                }

                var newUser = new ApplicationUser
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.Username,
                    UserName = registerDto.Username,
                };



                var result = await _userManager.CreateAsync(newUser, registerDto.Password);
                if(!result.Succeeded) 
                {
                    return BadRequest(new ApiResponse(400));
                }


                var userCcreation = new UserDto
                {
                    Email = registerDto.Email,
                    DisplayName = registerDto.Username,
                    Token = _tokenService.CreateToken(newUser)
                };


                return Ok(userCcreation);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user registration.");
                return StatusCode(500, "An error occurred while processing your request. Please try again later");
            }

        }
    }
}
