using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VeyraApi.Interfaces;
using VeyraApi.Data;
using VeyraApi.Repositories;
using VeyraApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Database
var supabaseUrl = builder.Configuration["NEXT_PUBLIC_SUPABASE_URL"] ?? "";
var dbPassword = builder.Configuration["SUPABASE_DB_PASSWORD"] ?? "";
var refName = supabaseUrl.Replace("https://", "").Replace(".supabase.co", "");
var connectionString = $"Host=db.{refName}.supabase.co;Port=5432;Database=postgres;Username=postgres;Password={dbPassword};SSL Mode=Require;Trust Server Certificate=true";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Auth
var jwtSecret = builder.Configuration["JWT_SECRET"] ?? "";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "veyra-api",
            ValidAudience = "veyra-client",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

// DI
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<IFinishingPackageRepository, FinishingPackageRepository>();
builder.Services.AddScoped<ISmartDeviceRepository, SmartDeviceRepository>();
builder.Services.AddScoped<ISmartPackageRepository, SmartPackageRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();
builder.Services.AddScoped<IHazemService, GroqService>();
builder.Services.AddHttpClient<IHazemService, GroqService>();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Health check
app.MapGet("/", () => Results.Ok(new { status = "healthy", version = "1.0.0" }));

app.Run();
