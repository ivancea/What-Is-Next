using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using WhatIsNext.Controllers;
using WhatIsNext.Dtos;
using WhatIsNext.Mappers;
using WhatIsNext.Model.Contexts;
using WhatIsNext.Model.Entities;
using WhatIsNext.Services;

#pragma warning disable SA1124 // Do not use regions
#pragma warning disable SA1516 // Elements should be separated by blank line

// Create the Web builder
var builder = WebApplication.CreateBuilder(args);

#region Configure services

builder.Services.AddControllers()
    .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

// In production, the React files will be served from this directory
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/build";
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "What Is Next", Version = "v1" });
});

builder.Services.AddDbContext<WinContext>(options =>
{
    var connectionString = builder.Configuration.GetValue<string>("POSTGRES_CONNECTION_STRING");

    options.UseNpgsql(connectionString);
});

builder.Services.AddTransient<IClassMapping<Graph, GraphDto>, GraphToGraphDtoMapping>();
builder.Services.AddTransient<IClassMapping<GraphDto, Graph>, GraphDtoToGraphMapping>();
builder.Services.AddTransient<IEntityUpdater<Graph>, GraphUpdater>();
builder.Services.AddTransient<IClassMapping<Concept, ConceptDto>, ConceptToConceptDtoMapping>();
builder.Services.AddTransient<IClassMapping<ConceptDto, Concept>, ConceptDtoToConceptMapping>();
builder.Services.AddTransient<IEntityUpdater<Concept>, ConceptUpdater>();

builder.Services.AddTransient<IGraphService, GraphService>();

builder.Services.AddTransient<GraphController>();

#endregion

#region Configure the Web application

var app = builder.Build();

if (!builder.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
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

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (builder.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});

using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var context = serviceScope.ServiceProvider.GetService<WinContext>();

context.Database.Migrate();

#endregion

app.Run();