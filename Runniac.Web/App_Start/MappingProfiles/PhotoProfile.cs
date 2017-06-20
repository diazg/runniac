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
    public class PhotoProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Photo, PhotoVM>()
                .ForMember(c => c.PhotoDate, o => o.MapFrom(s => String.Format("{0:dd/MM/yyyy}", s.PhotoDate)))
                .ForMember(c => c.Event, o => o.MapFrom(s => new EventVM { Id = s.Id }));
            Mapper.CreateMap<PhotoVM, Photo>()
                .ForMember(d => d.PhotoDate, o => o.MapFrom(s => ParseUtils.ParseDate(s.PhotoDate)));
        }
    }
}