using FriendsApi.Models;
using FriendsData;
using FriendsData.Entities;
using FriendsData.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FriendsApi
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
            services.AddAutoMapper( cfg =>
            {
                cfg.CreateMap<EventCreateModel, Event>()
                .ForMember(evt => evt.Members, 
                opt => opt.MapFrom(model => new List<User>()))
                .ForMember(evt => evt.Id,
                opt => opt.Ignore())
                .ForMember(evt => evt.Organizator,
                opt=>opt.Ignore());

                cfg.CreateMap<Event, EventReadModel>()
                .ForMember(model => model.MembersId,
                opt => opt.MapFrom( evt => evt.Members.Select(memb => memb.Id)))
                .ForMember(model => model.OrganizatorId,
                opt => opt.MapFrom(evt => evt.OrganizatorId));

                cfg.CreateMap<EventUpdateModel, Event>()
                .ForMember(evt => evt.Members,
                opt => opt.Ignore())
                .ForMember(evt => evt.Id,
                opt => opt.Ignore())
                .ForMember(evt => evt.Organizator,
                opt=>opt.Ignore())
                .ForMember(evt => evt.OrganizatorId,
                opt => opt.Ignore());

            });

            var connectionStr = Configuration["DbConnectionString"];
            
            services.AddDbContext<EventsContext>(opt => opt.UseSqlServer(connectionStr));

            services.AddScoped<UnitOfWork>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FriendsApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FriendsApi v1"));
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
