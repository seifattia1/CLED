using AutoMapper;
using CLED.Areas.Identity.Data;
using CLED.Models;
using CLED.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CLEDUser, UserViewModel>()
                .ForMember(dst => dst.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<UserViewModel, CLEDUser>();
        }
    }
}
