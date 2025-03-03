 using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialConnect.DTOs.Comment;
using SocialConnect.DTOs.User;
using SocialConnect.Models;
using SocialConnect.UnitOfWorks;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SocialConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        UserManager<User> UserManager;
        UnitOfWork unit;
        IMapper mapper;
        public ProfileController(UnitOfWork unit, IMapper mapper,UserManager<User> UserManager)
        {
            this.unit = unit;
            this.mapper = mapper;
            this.UserManager = UserManager;
        }

        [HttpGet("allUsers")]
        [SwaggerOperation(Summary ="Get all users", Description ="")]
        [SwaggerResponse(200, "Return all users", typeof(List<DisplayProfileDTO>))]
        public IActionResult GetAll() 
        {
            List<User>users=unit.UserRepo.GetAll();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }
            List<DisplayProfileDTO> displayProfileDTO = mapper.Map<List<DisplayProfileDTO>>(users);

            return Ok(displayProfileDTO);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Can search on user by user id ", Description = "")]
        [SwaggerResponse(200, "Return user data", typeof(DisplayProfileDTO))]
        [SwaggerResponse(404, "If no user founded")]
        public IActionResult GetById(string id)
        {
            User user = unit.UserRepo.GetById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            DisplayProfileDTO displayProfileDTO= mapper.Map<DisplayProfileDTO>(user);
            return Ok(displayProfileDTO);

        }




        [HttpPost("Create")]
        [SwaggerOperation(
        Summary = "Create User Profile",
        Description = "Creates a new user profile, uploads a profile picture, and returns the profile details."
        )]
        [SwaggerResponse(201, "Profile created successfully.", typeof(DisplayProfileDTO))]
        [SwaggerResponse(400, "Invalid user data.")]
        [SwaggerResponse(401, "User not authenticated.")]
        [SwaggerResponse(409, "User profile already exists.")]
        public IActionResult Post([FromBody]AddProfileDTO addProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }

            var user = unit.UserRepo.GetAll().FirstOrDefault(p => p.Id == userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            mapper.Map(addProfileDTO, user);


            string path = $"{Directory.GetCurrentDirectory()}\\Uploads\\{addProfileDTO.Picture.FileName}";
            FileStream fileStream = new FileStream(path, FileMode.Create);
            user.Picture.CopyTo(fileStream);
            user.Photopath = path;

            unit.UserRepo.Update(user);
            unit.Save();

            var displayProfileDTO = mapper.Map<DisplayProfileDTO>(user);

            displayProfileDTO.Username = user.UserName;
            displayProfileDTO.FullName = user.FullName;
            displayProfileDTO.Email = user.Email;

            return Ok(new { Message = "Profile created successfully" ,Data= displayProfileDTO});
        }

        



        [HttpPut("Update")]
        [SwaggerOperation(
        Summary = "Update User Profile",
        Description = "Updates an existing user profile. If the data provided is partial, it will only update the given fields."
        )]
        [SwaggerResponse(200, "Profile updated successfully.", typeof(DisplayProfileDTO))]
        [SwaggerResponse(400, "Invalid user data.")]
        [SwaggerResponse(401, "User not authenticated.")]
        [SwaggerResponse(404, "User profile not found.")]
        public IActionResult Update([FromBody] UpdateProfileDTO updateProfileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }

            var user = unit.UserRepo.GetAll().FirstOrDefault(p => p.Id == userId);
            if (user == null)
            {
                return NotFound("User profile not found.");
            }

            if (updateProfileDTO.Username != null && updateProfileDTO.Username != user.UserName)
            {
                return BadRequest("Username updates are restricted. Please use the username change functionality.");
            }

            updateProfileDTO.FullName ??= user.FullName;
            updateProfileDTO.Email ??= user.Email;
            mapper.Map(updateProfileDTO, user);

            if (updateProfileDTO.Picture != null)
            {
                // Handle picture upload and update logic
                string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateProfileDTO.Picture.FileName);
                string path = Path.Combine(uploadDirectory, fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    updateProfileDTO.Picture.CopyTo(fileStream);
                }

                user.Photopath = path; // Update photo path
            }

            unit.UserRepo.Update(user);
            unit.Save();

            var displayProfileDTO = mapper.Map<DisplayProfileDTO>(user);  

            return Ok(new { Message = "Profile updated successfully", Data = displayProfileDTO });
        }



    }
}
