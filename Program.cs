using DotNetEnv;
using MediConnectBackend.Data;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Mappers;
using MediConnectBackend.Models;
using MediConnectBackend.Repository;
using MediConnectBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();  // Load environment variables from .env file in development
}

// Get database and JWT settings from environment variables
string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("DB_HOST is not configured");
string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? throw new InvalidOperationException("DB_NAME is not configured");
string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("DB_USER is not configured");
string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD is not configured");
string dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306"; // Default MySQL port
string jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException("JWT_SECRET_KEY is missing");
string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER is missing");
string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
if (string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException("JWT_AUDIENCE is missing or empty");
}

string connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};Port={dbPort};Charset=utf8;";

// Add DbContext for Entity Framework
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

var key = Encoding.ASCII.GetBytes(jwtSecretKey);

builder.Services.AddAuthentication(option => 
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOption =>
{
    var key = builder.Configuration.GetValue<string>("JwtConfig:Key");
    var keyBytes = Encoding.ASCII.GetBytes(key);
    jwtOption.SaveToken = true;
    jwtOption.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

// Add CORS to allow localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

// Add controllers
builder.Services.AddControllers();

// Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add scoped services for repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPastAppointmentRepository, PastAppointmentRepository>();
builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();
builder.Services.AddScoped<IAvailabilityRepository, AvailabilityRepository>();
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();



var app = builder.Build();

// Ensure the database is up-to-date
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    dbContext.Database.Migrate();
}

// Initialize roles (Admin, Doctor, Patient)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roleNames = { "Admin", "Doctor", "Patient" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}

// Production error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Use CORS for localhost
app.UseCors("AllowLocalhost3000");

// Add routing
app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Enable Swagger for API testing
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();
