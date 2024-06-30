using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Framework.Middlewares;
using Presentation.Service;
using System.Reflection;
using Data.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var connectionString = builder.Configuration.GetConnectionString("LearnContext");
builder.Services.AddDbContext<DBLearnContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<ILevelLogic, LevelLogic>();
builder.Services.AddScoped<ILessonLogic, LessonLogic>();
builder.Services.AddScoped<IVocabLogic, VocabLogic>();
builder.Services.AddScoped<IGrammerLogic, GrammerLogic>();
builder.Services.AddScoped<ISpeakingLogic, SpeakingLogic>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
