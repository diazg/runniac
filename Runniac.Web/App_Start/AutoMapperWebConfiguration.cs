using AutoMapper;
using Runniac.Data;
using Runniac.Web.App_Start.MappingProfiles;
using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runniac.Web.App_Start
{
    public class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new CommentProfile());
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new UserInfoProfile());
                cfg.AddProfile(new PhotoProfile());
                cfg.AddProfile(new VoteProfile());
                cfg.AddProfile(new TownProfile());
            });
        }
    }
}