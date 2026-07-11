using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using PawnShop.Infrastructure.Repositories.QueryRepository;
using PawnShop.Infrastructure.Repositories.CommandRepository;
using PawnShop.Application.UseCases;
using PawnShop.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Allow configuration from environment variables (overrides appsettings)
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Prefer connection string from environment variables when available
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                       ?? Environment.GetEnvironmentVariable("DefaultConnection")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PawnShopDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddControllers();
// Temporary permissive CORS policy: allow any origin/method/header.
// Replace with a restricted origin (deployed URL) when ready for production.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PawnShop API", Version = "v1" });

    // Add JWT Bearer definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token only (no 'Bearer ' prefix)"
    });

    // Require Bearer token for all operations (optional)
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] { }
        }
    });
});

// Allow overriding JwtSettings values from environment variables. Environment vars to use:
// JwtSettings__Key, JwtSettings__Issuer, JwtSettings__Audience, JwtSettings__ExpiryMinutes
var envJwtKey = Environment.GetEnvironmentVariable("JwtSettings__Key") ?? Environment.GetEnvironmentVariable("JWT_KEY");
if (!string.IsNullOrEmpty(envJwtKey)) builder.Configuration["JwtSettings:Key"] = envJwtKey;

var envJwtIssuer = Environment.GetEnvironmentVariable("JwtSettings__Issuer") ?? Environment.GetEnvironmentVariable("JWT_ISSUER");
if (!string.IsNullOrEmpty(envJwtIssuer)) builder.Configuration["JwtSettings:Issuer"] = envJwtIssuer;

var envJwtAudience = Environment.GetEnvironmentVariable("JwtSettings__Audience") ?? Environment.GetEnvironmentVariable("JWT_AUDIENCE");
if (!string.IsNullOrEmpty(envJwtAudience)) builder.Configuration["JwtSettings:Audience"] = envJwtAudience;

var envJwtExpiry = Environment.GetEnvironmentVariable("JwtSettings__ExpiryMinutes") ?? Environment.GetEnvironmentVariable("JWT_EXPIRYMINUTES");
if (!string.IsNullOrEmpty(envJwtExpiry)) builder.Configuration["JwtSettings:ExpiryMinutes"] = envJwtExpiry;

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSettings =
    builder.Configuration
           .GetSection("JwtSettings")
           .Get<JwtSettings>();

builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtSettings.Key))
        };
});

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserQueryRepository, UserQueryRepository>();
builder.Services.AddScoped<IUserCommandRepository, UserCommandRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILoanCommandRepository, LoanCommandRepository>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanQueryRepository, LoanQueryRepository>();
builder.Services.AddScoped<IDashboardQueryRepository, DashboardQueryRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ICapitalQueryRepository, CapitalQueryRepository>();
builder.Services.AddScoped<ICapitalCommandRepository, CapitalCommandRepository>();
builder.Services.AddScoped<ICapitalContributorQueryRepository, CapitalContributorQueryRepository>();
builder.Services.AddScoped<ICapitalContributorCommandRepository, CapitalContributorCommandRepository>();
builder.Services.AddScoped<ICapitalService, CapitalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable the temporary permissive CORS policy. Keep this before authentication/authorization.
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
