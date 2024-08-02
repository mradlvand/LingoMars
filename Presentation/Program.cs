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

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});


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

#region "JWT Token For Authentication Login"    

app.UseCookiePolicy();
app.UseSession();
app.Use(async (context, next) =>
{
    var JWToken = context.Session.GetString("JWToken");
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
    }
    await next();
});
app.UseAuthentication();
app.UseAuthorization();


#endregion

app.MapControllers();

app.Run();
