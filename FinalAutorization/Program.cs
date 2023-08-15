using FinalAutorization;
using FinalAutorization.Context;
using FinalAutorization.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UsersDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));
builder.Services.AddIdentity<User, IdentityRole>(opt => { 
    opt.Password.RequiredUniqueChars    = 0;
    opt.Password.RequiredLength         = 10;
    opt.Password.RequireLowercase       = false;
    opt.Password.RequireUppercase       = true;
    opt.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<UsersDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme   = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme      = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme               = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.SaveToken                   = true;
    opt.RequireHttpsMetadata        = false;
    opt.TokenValidationParameters   = new TokenValidationParameters()
    {
        ValidateIssuer      = true,
        ValidateAudience    = true,
        ValidAudience       = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer         = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



