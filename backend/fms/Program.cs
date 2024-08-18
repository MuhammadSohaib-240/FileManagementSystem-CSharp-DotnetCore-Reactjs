using fms.Database;
using fms.Repositories;
using fms.Security;
using fms.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<JwtService>();

// Add authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-32-character-or-longer-secret-key-her")),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

// Custom middleware to validate JWT token and skip specific paths
app.Use(async (context, next) =>
{
    try
    {
        var path = context.Request.Path.ToString().ToLower();
        if (!path.Contains("/api/auth/register") && !path.Contains("/api/auth/login"))
        {
            var jwtService = context.RequestServices.GetRequiredService<JwtService>();
            var token = context.Request.Cookies["jwt"];
            if (!string.IsNullOrEmpty(token) && token != "Invalid Credentials")
            {
                Console.WriteLine($"Token retrieved from cookie: {token}");
                var validatedToken = jwtService.Verify(token);
                context.Items["UserId"] = validatedToken.Issuer;
            }
        }
        await next.Invoke();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred in middleware: " + ex.Message);
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized");
    }
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
