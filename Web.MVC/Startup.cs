using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoffeeShopCMS.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using CoffeeShopCMS.Data.Repositories;
using CoffeeShopCMS.Domain.Repositories;
using CoffeeShopCMS.Service.Services;
using Web.MVC.Framework.Configuration;
using CoffeeShopCMS.Service.Provider;
using CoffeeShopCMS.Infrastructure;

namespace Web.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<CoffeeShopCMS.Data.ApplicationContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ApplicationContextConnection"), b => b.MigrationsAssembly("Web.MVC")));
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("ApplicationContextConnection")));

            var builder = new ContainerBuilder(); //Autofac

            builder.Populate(services);
            //builder.RegisterType<MyType>().As<IMyType>();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("CoffeeShopCMS.Service"))
                  .Where(t => t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                  .Where(t => t.Name.EndsWith("Repository"))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                  .Where(t => t.Name.EndsWith("UnitOfWork"))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("ApplicationDbContext"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<MernisService>().As<IVerificationOperationsService>();
            builder.RegisterType<ApplicationContext>().As<ApplicationContext>().SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            services.AddScoped<ICustomerService, CustomerService>();

            builder.RegisterModule(new AutoMapperConfiguration());

            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Customer}/{action=Create}/{id?}");
            });
        }
    }
}