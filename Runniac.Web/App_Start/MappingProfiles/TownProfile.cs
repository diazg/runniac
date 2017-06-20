using AutoMapper;
using Runniac.Data;
using Runniac.Utils;
using Runniac.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runniac.Web.App_Start.MappingProfiles
{
    public class TownProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Town, TownVM>();
        }
    }
}