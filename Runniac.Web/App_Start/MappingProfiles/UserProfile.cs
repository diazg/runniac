using AutoMapper;
using Runniac.Data;
using Runniac.Membership.Models;
using Runniac.Utils;
using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runniac.Web.App_Start.MappingProfiles
{
    public class UserProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, UserVM>()
                .ForMember(c => c.Email, o => o.MapFrom(s => s.UserName));
            Mapper.CreateMap<UserVM, User>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email));
        }
    }
}