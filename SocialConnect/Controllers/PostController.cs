using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialConnect.DTOs.Post;
using SocialConnect.DTOs.User;
using SocialConnect.Models;
using SocialConnect.UnitOfWorks;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SocialConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public PostController(UnitOfWork unit,IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all posts ", Description = "")]
        [SwaggerResponse(200, "Return all posts", typeof(List<DisplayPostDTO>))]
        public IActionResult GetAll()
        {
            List<Post> posts=unit.PostRepo.GetAll();
            List<DisplayPostDTO> displayPostDTOs=mapper.Map<List<DisplayPostDTO>>(posts);
            return Ok(displayPostDTOs);
        }




        [HttpPost]
        [SwaggerOperation(Summary = "Craete posts")]
        [SwaggerResponse(201, "If posts created successfully")]
        [SwaggerResponse(400, "If invalid posts data")]
        public IActionResult Post(AddPostDTO addPostDTO) 
        {

            if (ModelState.IsValid) 
            {
                //
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User ID not found in token.");
                }

                //
                var user = unit.UserRepo.GetById(userId);

                if (user==null)
                {
                    return NotFound("User not found");
                }
                Post post = mapper.Map<Post>(addPostDTO);
                post.UserId= userId;
                post.UserPost= user;
                post.CreatedAt = DateTime.Now;


                unit.PostRepo.Add(post);
                unit.Save();

                var response = mapper.Map<DisplayPostDTO>(post);

                return CreatedAtAction(nameof(Post), response);
            }
            else
            { return BadRequest(ModelState); }
        }





        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a post by ID", Description = "Fetches a post by its unique ID.")]
        [SwaggerResponse(200, "Post found", typeof(DisplayPostDTO))]
        [SwaggerResponse(404, "Post not found")]
        public IActionResult GetById(string id)
        {
            Post post=unit.PostRepo.GetById(id);
            if (post == null) 
            {
                return NotFound("Post not found");
            }
            DisplayPostDTO displayPostDTO=mapper.Map<DisplayPostDTO>(post);
            return Ok(displayPostDTO);
        }





        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Edit an existing post", Description = "Edits the details of a post by its unique ID. Only the user who created the post can edit it.")]
        [SwaggerResponse(200, "Post updated successfully")]
        [SwaggerResponse(404, "Post not found")]
        [SwaggerResponse(403, "User is not authorized to edit this post")]
        public IActionResult Edit(string id,UpdatePostDTO updatePostDTO) 
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User ID not found in token.");
                }

                var user = unit.UserRepo.GetById(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                Post post = unit.PostRepo.GetById(id);
                if (post == null)
                {
                    return NotFound("Post not found");
                }
                if (post.UserId != userId)
                {
                    return Forbid("You are not authorized to edit this post.");
                }

                post.PostId = id;
                post.Title = updatePostDTO.Title ?? post.Title;
                post.Content = updatePostDTO.Content ?? post.Content;
                post.UpdatedAt = DateTime.Now;

                //Post post = mapper.Map<Post>(updatePostDTO);
                unit.PostRepo.Update(post);
                unit.Save();

                var displayPostDTO = mapper.Map<DisplayPostDTO>(post);
                return Ok(new { Message = "Post updated successfully", Data = displayPostDTO });
            }
            else 
            {
                return BadRequest(ModelState);

            }


        }




        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a post by ID", Description = "Deletes a post by its unique ID. Only the user who created the post can delete it.")]
        [SwaggerResponse(200, "Post deleted successfully")]
        [SwaggerResponse(404, "Post not found")]
        [SwaggerResponse(403, "User is not authorized to delete this post")]
        public IActionResult Delete(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            Post post = unit.PostRepo.GetById(id);
            if (post == null)
            {
                return NotFound("Post not found");
            }
            unit.PostRepo.Delete(id);
            unit.Save();
            return Ok(new {Message="Post deleted successfully"});

        }

    }
}
