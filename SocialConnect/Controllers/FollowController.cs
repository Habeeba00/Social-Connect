using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialConnect.DTOs.Follower;
using SocialConnect.DTOs.User;
using SocialConnect.Models;
using SocialConnect.UnitOfWorks;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SocialConnect.Controllers
{
    [Route("api/User/")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        UnitOfWork unit;
        IMapper mapper;
        public FollowController(UnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }
        [HttpPost("{UserId}/follow")]
        [SwaggerOperation(Summary = "Add a follower to a user", Description = "Allows a user to follow another user.")]
        [SwaggerResponse(200, "Follow added successfully.")]
        [SwaggerResponse(400, "Bad request, You are already following this user.")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "User not found")]
        public IActionResult Follow(string UserId)
        {
            if (ModelState.IsValid) 
            {
                var followerId =User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(followerId))
                {
                    return Unauthorized("User not authenticated");
                }

                if (followerId==UserId) 
                {
                    return BadRequest("You cannot follow yourself.");
                }

                var followedUser = unit.UserRepo.GetById(UserId);
                var followerUser = unit.UserRepo.GetById(followerId);

                if (followedUser == null)
                {
                    return NotFound("User to be followed not found.");
                }
                if (followerUser == null)
                {
                    return NotFound("Follower user not found.");
                }

                var existingFollow = unit.FollowerRepo
                    .GetAll()
                    .FirstOrDefault(f => f.FollowerId == followerId && f.FollowedUserId == UserId);

                if (existingFollow != null)
                {
                    return BadRequest("You are already following this user.");
                }

                var follow = new UserFollower
                {
                    FollowId = Guid.NewGuid().ToString(),
                    FollowerId =followerId,
                    FollowedUserId=UserId
                    
                };
                unit.FollowerRepo.Add(follow);
                unit.Save();

                var response = mapper.Map<DisplayProfileDTO>(follow);

                return CreatedAtAction(nameof(Follow),response,"Successfully followed the user.");
            }
            else
            {
                return BadRequest(ModelState);
            }

        }



        [HttpDelete("{UserId}/follow")]
        [SwaggerOperation(Summary = "Unfollow a user", Description = "Allows a user to unfollow another user.")]
        [SwaggerResponse(200, "Unfollowed successfully.")]
        [SwaggerResponse(400, "Bad request, You cannot unfollow yourself.")]
        [SwaggerResponse(401, "Unauthorized")]
        [SwaggerResponse(404, "User not found")]
        [SwaggerResponse(404, "Follow relationship not found")]
        public IActionResult Unfollow(string UserId)
        {
            if (ModelState.IsValid)
            {
                var followerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(followerId))
                {
                    return Unauthorized("User not authenticated");
                }
                if (followerId == UserId)
                {
                    return BadRequest("You cannot unfollow yourself");
                }
                var followedUser = unit.UserRepo.GetById(UserId);
                var followerUser = unit.UserRepo.GetById(followerId);

                if (followedUser == null)
                {
                    return NotFound("User to be unfollowed not found.");
                }
                if (followerUser == null)
                {
                    return NotFound("Follower user not found.");
                }

                var ExistingFollow = unit.FollowerRepo.GetAll()
                    .FirstOrDefault(f => f.FollowerId == followerId && f.FollowedUserId == UserId);
                if (ExistingFollow == null)
                {
                    return NotFound("You are not following this user.");
                }
                unit.FollowerRepo.Unfollow(followerId,UserId);
                unit.Save();
                return Ok("Successfully unfollowed the user.");

            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
