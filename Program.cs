using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using CaixaRegistradora.Services;
using CaixaRegistradora.Classes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<List<Nota>>();
builder.Services.AddScoped<Service>();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStaticFiles();
    app.UseSwagger(s => {
        s.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(o => {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Caixa Registradora");
        o.RoutePrefix = string.Empty;
    });
}

var ligadoDesde = DateTime.Now.ToLocalTime().ToString("dd'/'MM'/'yyyy' 'HH:mm:ss");

app.UseHttpsRedirection();

app.MapGet("/health-check", () => $"Tudo Certo!\nApi Ligada desde: {ligadoDesde}.");
app.MapControllers();
app.Run();