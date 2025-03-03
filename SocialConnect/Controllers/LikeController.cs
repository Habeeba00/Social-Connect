using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialConnect.DTOs.Like;
using SocialConnect.DTOs.Post;
using SocialConnect.Models;
using SocialConnect.UnitOfWorks;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SocialConnect.Controllers
{
    [Route("api/Post/")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public LikeController(UnitOfWork unit, IMapper mapper)
        {   
            this.unit = unit;
            this.mapper = mapper;
        }



        [HttpGet("{PostId}/likes")]
        [SwaggerOperation(Summary = "Retrieve users who liked a post", Description = "")]
        [SwaggerResponse(200, "Returns the list of users who liked the post", typeof(List<DisplayLikeDTO>))]
        [SwaggerResponse(404, "Post not found")]
        public IActionResult Get(string PostId) 
        {
            var post = unit.PostRepo.GetById(PostId);
            if (post == null) 
            {
                return NotFound("Post not found");
            }
            var users = mapper.Map<List<DisplayLikeDTO>>(post.Likes.Select(l => l.UserLikes).ToList());
            return Ok(users);   

        }


        [HttpPost("{PostId}/like")]
        [SwaggerOperation(
         Summary = "Like or Unlike a post",
         Description = "This endpoint allows a user to like or unlike a post. " +
                       "If the user has already liked the post, the like will be removed. " +
                       "If the user hasn't liked the post yet, the like will be added.",
         OperationId = "AddLikeOrRemoveLike"
        )]
        [SwaggerResponse(200, "If the like was successfully added or removed.")]
        [SwaggerResponse(400, "If the request contains invalid data.")]
        [SwaggerResponse(401, "If the user is not authenticated.")]
        [SwaggerResponse(404, "If the post was not found.")]
        public IActionResult AddLike(string PostId)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User not authenticated");
                }
                var post = unit.PostRepo.GetById(PostId);
                if (post == null)
                {
                    return NotFound("Post not found.");
                }

                var existingLike = unit.LikeRepo.GetAll()
                    .FirstOrDefault(l => l.Post_Id == PostId && l.UserId == userId && !l.IsDeleted);

                if (existingLike != null)
                {
                    unit.LikeRepo.Delete(existingLike.LikeId);
                    unit.Save();

                    return Ok(new { Message = "Like removed successfully!" });
                }
                else 
                {
                    var like = new Likes
                    {
                        UserId= userId,
                        Post_Id= PostId
                    };
                    unit.LikeRepo.Add(like);
                    unit.Save();

                    var response = mapper.Map<DisplayLikeDTO>(like);
                    return CreatedAtAction(nameof(AddLike), response);

                }

            }
            else
            { return BadRequest(ModelState); }
        }



        [HttpDelete("{PostId}/like")]
        [SwaggerOperation(Summary = "Remove a like from a post", Description = "This endpoint removes a like from a specific post.")]
        [SwaggerResponse(200, "Like removed successfully", typeof(object))]
        [SwaggerResponse(404, "Post not found")]
        [SwaggerResponse(400, "Like not found")]
        [SwaggerResponse(401, "User not authenticated")]
        public IActionResult Delete(string PostId) 
        {
            var post = unit.PostRepo.GetById(PostId);
            if (post == null)
            {
                return NotFound("Post not found");
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }
            var like = unit.LikeRepo.GetAll()
              .FirstOrDefault(l => l.Post_Id == PostId && l.UserId == userId && !l.IsDeleted);

            if (like == null)
            {
                return BadRequest("Like not found");
            }

            unit.LikeRepo.Delete(like.LikeId);
            unit.Save();
            return Ok(new { Message = "Like removed successfully!" });


        }
    }
}
