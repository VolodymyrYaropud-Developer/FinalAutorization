using FinalAutorization.Context;
using FinalAutorization.Models;
using FinalAutorization.Servivces;
using FinalAutorization.Servivces.JWTData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

bool IsDockerUsed = true;

string dbHost;
string dbName;
string dbPassword;
string connectionString;

if (IsDockerUsed)
{
    dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    dbName = Environment.GetEnvironmentVariable("DB_NAME");
    dbPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
    connectionString = string.Format(builder.Configuration.GetConnectionString("DockerConnectionString"), dbHost, dbName, dbPassword);
}
else
{
    connectionString = builder.Configuration.GetConnectionString("ConnStr");
}


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UsersDBContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredUniqueChars = 0;
    opt.Password.RequiredLength = 10;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<UsersDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    //Not nessesary
    //opt.SaveToken                   = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],// Use IOptions to parse JSON file
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddScoped<IControllerService, ControllerServise>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
void ConfigureServices(IServiceCollection services)
{

    services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
}



