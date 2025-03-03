using AutoMapper;
using SocialConnect.DTOs.Comment;
using SocialConnect.DTOs.Follower;
using SocialConnect.DTOs.Like;
using SocialConnect.DTOs.Post;
using SocialConnect.DTOs.Profile;
using SocialConnect.DTOs.User;
using SocialConnect.Models;

namespace SocialConnect.MappingConfigs
{
    public class MapppingConfig:Profile 
    {
        public MapppingConfig()
        {
            CreateMap<User, AddProfileDTO>().ReverseMap();
            CreateMap<User, DisplayProfileDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Followers, opt => opt.MapFrom(src => src.Followers))
                .ForMember(dest => dest.Following, opt => opt.MapFrom(src => src.Following));
            CreateMap<User, UpdateProfileDTO>().ReverseMap();

            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<User, LoginResponse>().ReverseMap();

            CreateMap<User,RegisterDTO>().ReverseMap();
            CreateMap<User, RegisterResponseDTO>().ReverseMap();
            CreateMap<User, ChangePasswordDTO>().ReverseMap();

            CreateMap<Post, AddPostDTO>().ReverseMap();
            CreateMap<Post, DisplayPostDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserPost.UserName));
            CreateMap<Post, UpdatePostDTO>().ReverseMap();


            CreateMap<Likes, DisplayLikeDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserLikes.UserName));
            CreateMap<User, DisplayLikeDTO>().ReverseMap();


            CreateMap<Comment, AddCommentDTO>().ReverseMap();
            CreateMap<Comment, DisplayCommentDTO>()
                 .ForMember(dest => dest.Username, op => op.MapFrom(src => src.UserComment.UserName));
            CreateMap<User, DisplayCommentDTO>().ReverseMap();


            CreateMap<UserFollower, DisplayFollowerDTO>()
                .ForMember(dest => dest.FollowerName, op => op.MapFrom(src => src.Follower.UserName));
            CreateMap<UserFollower, DisplayFollowingDTO>()
                .ForMember(dest => dest.FollowedUserName, opt => opt.MapFrom(src => src.FollowedUser.UserName));

            CreateMap<UserFollower, DisplayProfileDTO>().ReverseMap();

        }
    }
}
