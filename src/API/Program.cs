using Application;
using Carter;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

#region IoC Container
builder.Services.AddCarter()
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddAppDbContext()
                .AddServices()
                .AddMediator()
                .AddValidators();

#endregion IoC Container

var app = builder.Build();

#region HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapCarter();
#endregion HTTP Request Pipeline

app.Run();