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
    public class EventProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Event, EventVM>()
                .ForMember(d => d.EventDate, o => o.MapFrom(s => String.Format("{0:dd/MM/yyyy HH:mm}", s.EventDate)))
                .ForMember(d => d.DateWithFormat, o => o.MapFrom(s => s.EventDate));
            Mapper.CreateMap<EventVM, Event>()
                .ForMember(d => d.EventDate, o => o.MapFrom(s => ParseUtils.ParseDate(s.EventDate)));
        }
    }
}