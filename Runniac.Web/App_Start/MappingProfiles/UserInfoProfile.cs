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
    public class UserInfoProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<UserInfo, UserVM>()
                .ForMember(c => c.Email, o => o.MapFrom(s => s.UserName))
                .ForMember(c => c.Id, o => o.MapFrom(s => s.UserId));
            Mapper.CreateMap<UserVM, UserInfo>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id));
        }
    }
}