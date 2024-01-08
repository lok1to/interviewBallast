using InterviewBallast.Api.DI;
using InterviewBallast.Api.Middleware;
using InterviewBallast.Common.Helper;
using InterviewBallast.Core.Automapper;
using InterviewBallast.Core.Dto.Player;
using InterviewBallast.Core.Validators;
using InterviewBallast.Domain.Entities;
using InterviewBallast.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfig.LoadServices(builder.Services);
builder.Services.AddControllers();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Audience = "testAudience";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "InterviewBallast API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var connectionStringApp = builder.Configuration.GetConnectionString("InterviewBallastConnectionString");
builder.Services.AddDbContext<InterviewBallastContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(connectionStringApp));

var connectionStringAuth = builder.Configuration.GetConnectionString("InterviewBallastAuthConnectionString");
builder.Services.AddDbContext<InterviewBallastAuthContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(connectionStringAuth));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var app = builder.Build();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ValidationMiddleware<PlayerRequest>>(new PlayerValidator());

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
