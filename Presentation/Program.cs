using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Framework.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation.Service;
using System.Reflection;
using Data.Context;
using System.Text.Json.Serialization;
using Presentation.Models.Aut;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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

#region "JWT Token For Authentication Login"    
SiteKeys.Configure(builder.Configuration.GetSection("AppSettings"));
var key = Encoding.ASCII.GetBytes(SiteKeys.Token);

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(token =>
 {
     token.RequireHttpsMetadata = false;
     token.SaveToken = true;
     token.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(key),
         ValidateIssuer = true,
         ValidIssuer = SiteKeys.WebSiteDomain,
         ValidateAudience = true,
         ValidAudience = SiteKeys.WebSiteDomain,
         RequireExpirationTime = true,
         ValidateLifetime = true,
         ClockSkew = TimeSpan.Zero
     };
 });

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});

var app = builder.Build();

app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
