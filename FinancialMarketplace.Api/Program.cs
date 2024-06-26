using System.Text;

using FinancialMarketplace.Api;
using FinancialMarketplace.Api.Middlewares;
using FinancialMarketplace.Application;
using FinancialMarketplace.Application.Exceptions;
using FinancialMarketplace.Infrastructure;

using DotNetEnv;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure();

var jwtSecretKey = Encoding.UTF8.GetBytes(Env.GetString("API_AUTH_KEY") ?? throw new MissingEnvironmentVariableException("API_AUTH_KEY"));

if (jwtSecretKey.Length < 32)
    throw new FormatException("The API_AUTH_KEY must have at least 32 characters");

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    )
);

var app = builder.Build();

app.Migrate();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.MapHealthChecks("/_health");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<AuthMiddleware>();

app.UseCors();
app.Run();
