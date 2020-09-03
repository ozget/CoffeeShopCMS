using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Web.MVC.Framework.Configuration
{
    public class AutoMapperConfiguration : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register all profile classes in the calling assembly
            //builder.RegisterAssemblyTypes(typeof(AutoMapperConfiguration).Assembly).As<Profile>();
            builder.RegisterAssemblyTypes(Assembly.Load("CoffeeShopCMS.Service")).As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
                cfg.ValidateInlineMaps = false; // prevent validation errors for unmapped properties
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}