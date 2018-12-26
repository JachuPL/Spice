using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spice.Application.Common;
using Spice.Application.Plants;
using Spice.Application.Plants.Interfaces;
using Spice.AutoMapper;
using Spice.Persistence;
using Spice.ViewModels.Plants.Validators;

namespace Spice.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(AutoMapperFactory.CreateMapper());

            RegisterApplicationServices(services);

            services.AddDbContext<SpiceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePlantViewModelValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IDatabaseService, SpiceContext>();

            services.AddTransient<IQueryPlants, QueryPlants>();
            services.AddTransient<ICommandPlants, CommandPlants>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}