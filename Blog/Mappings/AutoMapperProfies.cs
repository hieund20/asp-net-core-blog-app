using AutoMapper;
using Blog.API.Models.Domain;
using Blog.API.Models.DTO;

namespace Blog.API.Mappings
{
    public class AutoMapperProfies : Profile
    {
        public AutoMapperProfies()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();

            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<AddPostRequestDto, Post>().ReverseMap();
            CreateMap<UpdatePostRequestDto, Post>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<AddCommentRequestDto, Comment>().ReverseMap();

            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<AddTagRequestDto, Tag>().ReverseMap();

            CreateMap<PostTag, PostTagDto>().ReverseMap();
            CreateMap<AddPostTagRequestDto, PostTag>().ReverseMap();
        }
    }
}
