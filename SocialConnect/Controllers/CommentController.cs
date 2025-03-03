using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialConnect.DTOs.Comment;
using SocialConnect.DTOs.Like;
using SocialConnect.Models;
using SocialConnect.UnitOfWorks;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SocialConnect.Controllers
{
    [Route("api/Posts/")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public CommentController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet("{PostId}/comments")]
        [SwaggerOperation(Summary = "Retrieve users who commented a post", Description = "")]
        [SwaggerResponse(200, "Returns the list of users who commented the post", typeof(List<DisplayCommentDTO>))]
        [SwaggerResponse(404, "Post not found")]
        public IActionResult GetPostComments(string PostId) 
        {
            var post = unit.PostRepo.GetById(PostId);
            if (post == null)
            {
                return NotFound("Post not found");
            }
      
            var comments = mapper.Map<List<DisplayCommentDTO>>(post.Comments);

            return Ok(comments);

        }





        [HttpPost("{PostId}/comments")]
        [SwaggerOperation(
         Summary = " Add comments on a post",
         Description = "This endpoint allows a user to Comment on a post. " ,
         OperationId = "AddComment"
        )]
        [SwaggerResponse(200, "If the comment was successfully added.")]
        [SwaggerResponse(400, "If the request contains invalid data.")]
        [SwaggerResponse(401, "If the user is not authenticated.")]
        [SwaggerResponse(404, "If the post was not found.")]
        public IActionResult PostComment(string PostId,[FromBody] AddCommentDTO addCommentDTO) 
        {
            if (ModelState.IsValid) 
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized("User not authenticated");
                }
                var post=unit.PostRepo.GetById(PostId);
                if (post == null) 
                {
                    return NotFound("Post not found");
                }
                var comment = new Comment 
                {
                    UserId = userId,
                    Post_Id=PostId,
                    CommentContent = addCommentDTO.CommentContent,
                    CreatedAt = DateTime.Now
                };
                unit.CommentRepo.Add(comment);
                unit.Save();

                var response = mapper.Map<DisplayCommentDTO>(comment);
                return CreatedAtAction(nameof(PostComment),response);
            }
            else
            {
                return BadRequest(ModelState); 
            }
        }




        [HttpDelete("{PostId}")]
        [SwaggerOperation(Summary = "Remove a comment from a post", Description = "This endpoint removes a comment from a specific post. The comment can be removed by its owner or an admin.")]
        [SwaggerResponse(200, "Comment removed successfully", typeof(object))]
        [SwaggerResponse(404, "Post not found")]
        [SwaggerResponse(400, "Comment not found")]
        [SwaggerResponse(401, "User not authenticated")]
        [SwaggerResponse(403, "User is not authorized to delete this comment")]
        public IActionResult Delete(string PostId) 
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var post = unit.PostRepo.GetById(PostId);
            if (post == null) 
            {
                return NotFound("Post not found");
            }
 
            var comment = unit.CommentRepo.GetAll()
                .FirstOrDefault(c=>c.Post_Id==PostId && c.UserId==userId && !c.IsDeleted);
            if(comment ==null) 
            {
                return NotFound("Comment not found");
            }

            var isAdmin = User.IsInRole("Admin");
            if (comment.UserId != userId && !isAdmin)
            {
                return Forbid("User is not authorized to delete this comment.");
            }
            unit.CommentRepo.Delete(comment.CommentId);
            unit.Save();
            return Ok(new { Message = "Comment removed successfully!" });
        }

    }
}
