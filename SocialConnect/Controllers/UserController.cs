using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialConnect.DTOs.Profile;
using SocialConnect.DTOs.User;
using SocialConnect.Models;
using SocialConnect.UnitOfWorks;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        SignInManager<User> SignInManager;
        UserManager<User> UserManager;
        public UserController(UnitOfWork unit,IMapper mapper, SignInManager<User> SignInManager, UserManager<User> UserManager)
        {
            this.unit = unit;
            this.mapper = mapper;
            this.SignInManager = SignInManager;
            this.UserManager = UserManager;
        }
        //Register
        [HttpPost("Register")]
        [SwaggerOperation(Summary = "Registers a new user.", Description = "Registers a new user with the provided details.")]
        [SwaggerResponse(201, "User registered successfully", typeof(RegisterResponseDTO))]
        [SwaggerResponse(400, "Invalid request or validation errors")]
        public IActionResult Register(RegisterDTO registerDTO)
        {
            var user = new User
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                FullName=registerDTO.FullName
            };

            var result = UserManager.CreateAsync(user, registerDTO.Password).Result;

            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //Login
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Logs in a user and returns a JWT token.", Description = "Authenticates the user with the provided username and password and returns a JWT token if successful.")]
        [SwaggerResponse(200, "JWT token returned successfully", typeof(string))]
        [SwaggerResponse(401, "Invalid username or password")]
        public IActionResult Login(LoginDTO loginDTO) 
        {
            var r = SignInManager.PasswordSignInAsync(loginDTO.Username, loginDTO.Password, false, false).Result;
            if (r.Succeeded) 
            {
                var user=UserManager.FindByNameAsync(loginDTO.Username).Result;
                #region Token
                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim(ClaimTypes.Name,user.UserName));
                userdata.Add(new Claim(ClaimTypes.NameIdentifier, user.Id)); // Use UserId as NameIdentifier

                var roles = UserManager.GetRolesAsync(user).Result;
                foreach (var role in roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, role));
                }
                var key = "welcome to my secret key Habiba Mohamed";
                var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var signingCredential = new SigningCredentials(secretkey,SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken
                    (
                    claims: userdata,
                    expires:DateTime.Now.AddDays(5),
                    signingCredentials: signingCredential
                    );
                string tokenstring=new JwtSecurityTokenHandler().WriteToken(token);
                #endregion
                var response = mapper.Map<LoginResponse>(user);
                response.Token=tokenstring;
                response.Roles= roles.ToList();
                return Ok (response);
            } 
            else
            {
                return Unauthorized("Invalid login attempt");
            }
        }


        //Change password
        [HttpPost("ChangePassword")]
        [SwaggerOperation(Summary = "Changes the password of a user.", Description = "Changes the password of the user identified by the given ID, using the provided old and new passwords.")]
        [SwaggerResponse(200, "Password changed successfully")]
        [SwaggerResponse(400, "Invalid request or validation errors")]
        [SwaggerResponse(401, "Unauthorized if the user is not authenticated")]
        public IActionResult ChangePassword(ChangePasswordDTO pass)
        {
            if (ModelState.IsValid)
            {
                var password=UserManager.FindByNameAsync(pass.Username).Result;
                var r = UserManager.ChangePasswordAsync(password,pass.OldPassword,pass.NewPassword).Result;
                if (r.Succeeded) { return Ok("Password Changed successfully"); }
                else { return BadRequest(r.Errors); }
            }
            else
            { return BadRequest(ModelState); }
        }



        //Logout
        [HttpGet("Logout")]
        [SwaggerOperation(Summary = "Logs out the user.", Description = "Signs out the user and invalidates their authentication.")]
        [SwaggerResponse(200, "User logged out successfully")]
        public IActionResult Logout()
        {
            SignInManager.SignOutAsync();
            return Ok("Logged out succssfully");
        }



    }
}
