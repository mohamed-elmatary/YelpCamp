using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YelpCamp.Dtos;
using YelpCamp.Models;

namespace YelpCamp.App_Start
{
    public  class MappingConfig :Profile
    {
        public  MappingConfig()
        {
            
            CreateMap<Campground, CampgroundDTO>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                   .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                   .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                   .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                   .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                   .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                   .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ApplicationUserId))
                   .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Lat))
                   .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Long))
                   .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))

                   .ReverseMap();

           //CreateMap<Comment, CommentDTO>()
           //       .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           //       .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
           //       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
           //       .ForMember(dest => dest.CampgroundId, opt => opt.MapFrom(src => src.CampgroundId))
           //       .ForMember(dest => dest.CampgroundName, opt => opt.MapFrom(src => src.Campground.Name))
           //       .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
           //       .ReverseMap();
            }



        
    }
}