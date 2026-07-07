using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PawnShop.Application.DTOs;
using PawnShop.Application.Interfaces.IRepositories;
using PawnShop.Application.Interfaces.IUseCases;
using PawnShop.Infrastructure.DBContext;
using PawnShop.Infrastructure.Repositories;
using System.Text;
using PawnShop.Infrastructure.Repositories.QueryRepository;
using PawnShop.Infrastructure.Repositories.CommandRepository;
using PawnShop.Application.UseCases;
using PawnShop.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PawnShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
