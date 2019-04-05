using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using WhatIsNext.Controllers;
using WhatIsNext.Model.Contexts;
using WhatIsNext.Mappers;
using WhatIsNext.Model.Entities;
using WhatIsNext.Dtos;
using WhatIsNext.Services;

namespace WhatIsNext
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "What Is Next", Version = "v1" });
            });

            services.AddDbContext<WinContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");

                options.UseNpgsql(connectionString);
            });

            services.AddTransient<IClassMapping<Graph, GraphDto>, GraphToGraphDtoMapping>();
            services.AddTransient<IClassMapping<GraphDto, Graph>, GraphDtoToGraphMapping>();
            services.AddTransient<IEntityUpdater<Graph>, GraphUpdater>();
            services.AddTransient<IClassMapping<Concept, ConceptDto>, ConceptToConceptDtoMapping>();
            services.AddTransient<IClassMapping<ConceptDto, Concept>, ConceptDtoToConceptMapping>();
            services.AddTransient<IEntityUpdater<Concept>, ConceptUpdater>();

            services.AddTransient<IGraphService, GraphService>();
            
            services.AddTransient<GraphController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = "swagger";
            });
            
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<WinContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
