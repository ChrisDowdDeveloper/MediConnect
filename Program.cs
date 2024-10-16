using DotNetEnv;
using MediConnectBackend.Data;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using MediConnectBackend.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// **Load environment variables only in development**
if (builder.Environment.IsDevelopment())
{
    // Load environment variables from .env file
    Env.Load();
}

// Fetch environment variables
string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? throw new InvalidOperationException("DB_HOST is not configured");
string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? throw new InvalidOperationException("DB_NAME is not configured");
string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? throw new InvalidOperationException("DB_USER is not configured");
string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? throw new InvalidOperationException("DB_PASSWORD is not configured");
string dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306"; // Default MySQL port
string jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? throw new InvalidOperationException("JWT_SECRET_KEY is missing");
string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER is missing");
string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new InvalidOperationException("JWT_AUDIENCE is missing");

// MySQL connection string using environment variables
string connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};Port={dbPort};Charset=utf8;";

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Register Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

// JWT authentication setup using the secret key
var key = Encoding.ASCII.GetBytes(jwtSecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false; // Consider setting this to true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPastAppointmentRepository, PastAppointmentRepository>();

// Build the app
var app = builder.Build();

// Apply pending migrations
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

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    // Use exception handling middleware
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable static files if needed
// app.UseStaticFiles();

// Add routing middleware
app.UseRouting();

// Enable Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Enable Swagger for API documentation
app.UseSwagger();
app.UseSwaggerUI();

// Map controllers
app.MapControllers();

// Run the application asynchronously
await app.RunAsync();
