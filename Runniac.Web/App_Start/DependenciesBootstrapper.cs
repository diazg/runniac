using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Runniac.Business;
using Runniac.Business.Impl;
using Runniac.Data.Repositories;
using Runniac.Data;
using Runniac.ExternalDataExtraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Runniac.Data.Repositories.Impl;
using Runniac.Web.WebAppServices;
using Runniac.Web.WebAppServices.Impl;
using Runniac.Membership;
using System.Configuration;
using Runniac.Email;

namespace Runniac.Web.App_Start
{
    public static class DependenciesBootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UsersContext>().As<IUsersContext>();
            builder.RegisterType<EventRepository>().As<IEventRepository>();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();
            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>();
            builder.RegisterType<VoteRepository>().As<IVoteRepository>();
            builder.RegisterType<TownRepository>().As<ITownRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<EventService>().As<IEventService>();
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<PhotoService>().As<IPhotoService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<TownService>().As<ITownService>();
            builder.RegisterType<EventsMultiSourceExtractor>().As<IMultiExtractor>();
            builder.RegisterType<WebSecurityService>().As<IWebSecurityService>();            
            builder.RegisterType<EmailSender>().As<IEmailSender>().WithParameters(new[]{
                new NamedParameter("apiKey", ConfigurationManager.AppSettings["MailgunKey"].ToString()), 
                new NamedParameter("domain", ConfigurationManager.AppSettings["MailgunDomain"].ToString())
            });

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}