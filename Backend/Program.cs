using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddSingleton<IPersonaServices, PersonaService2>();

builder.Services.AddKeyedSingleton<IPersonaServices, PersonaService>("personaservices");
builder.Services.AddKeyedSingleton<IPersonaServices, PersonaService2>("personaservices2");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnections"));
});

builder.Services.AddScoped<IPostService, PostService>();

// La URL esta definida en el archivo appsettings.json
builder.Services.AddHttpClient<IPostService, PostService>
    (c => c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
