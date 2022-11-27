using AutoMapper;
using FluentAssertions.Common;
using LendThingsAPI.Configuration;
using LendThingsAPI.DataAccess;
using LendThingsAPI.DataInitialization;
using LendThingsAPI.Proto;
using LendThingsCommonClasses.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddSqlServer<LendThingsContext>(builder.Configuration.GetConnectionString("Default"))
    .AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LendThingsContext>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Any",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                      });
});

//gRPCConfiguration
builder.Services.AddGrpc(opt => {
    opt.EnableDetailedErrors = true;
});
builder.Services.AddGrpcReflection();

//Agregando configuracion para JWT
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

//Authorization and Authentication
builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//Initialize User Data
if (app.Environment.IsDevelopment())
{
    await InitializationDataForUsers.Initialize(app.Services);
    await InitializationDataForCategories.Initialize(app.Services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Any");

app.UseAuthorization();

app.UseAuthentication();

app.MapGrpcReflectionService();
app.MapGrpcService<GrpcLoanService>();

app.MapControllers();

app.Run();
