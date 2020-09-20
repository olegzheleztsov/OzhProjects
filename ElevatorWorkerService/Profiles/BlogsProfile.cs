// Create By: Oleg Gelezcov                        (olegg )
// Project: ElevatorWorkerService     File: BlogsProfile.cs    Created at 2020/09/02/8:39 AM
// All rights reserved, for personal using only
// 

using AutoMapper;
using ElevatorLib.Models.Blogs;
using ElevatorWorkerService.Models.Blogs;

namespace ElevatorWorkerService.Profiles
{
    public class BlogsProfile : Profile
    {
        public BlogsProfile()
        {
            CreateMap<Blog, BlogUpdateModel>();
            CreateMap<BlogUpdateModel, Blog>();
            CreateMap<Blog, BlogDto>();
        }
    }
}