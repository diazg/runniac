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
    public class CommentProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Comment, CommentVM>()
                .ForMember(c => c.CommentDate, o => o.MapFrom(s => String.Format("{0:dd/MM/yyyy HH:mm}", s.CommentDate)));                
            Mapper.CreateMap<CommentVM, Comment>()
                .ForMember(d => d.CommentDate, o => o.MapFrom(s => ParseUtils.ParseDate(s.CommentDate)));
        }
    }
}