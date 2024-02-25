using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddTransient<DapperDbContext>();
builder.Services.AddTransient<ICepRepository, CepRepository>();
builder.Services.AddTransient<ICepService, CepService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "Desafio Back-end - Negocie Online",
        Description = "Consulta de CEP's",
        Contact = new OpenApiContact
        {
            Name = "Repositório",
            Url = new Uri("https://github.com/triciopo/desafio-backend")
        },
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();