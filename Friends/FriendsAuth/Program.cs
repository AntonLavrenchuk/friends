using FriendsAuth.Models;
using FriendsData.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtBearer:Issuer"],
            ValidAudience = builder.Configuration["JwtBearer:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( builder.Configuration["JwtBearer:Key"]))
        };

    });
builder.Services.AddAutoMapper(builder =>
{
    builder.CreateMap<RegisterModel, User>()
    .ForMember(user => user.Events, opt => opt.AllowNull())
    .ForMember(user => user.RefreshToken, opt => opt.Ignore())
    .ForMember(user => user.RefreshTokenID, opt => opt.Ignore())
    .ForMember(user => user.Role, opt => opt.Ignore())
    .ForMember(user => user.RoleId, opt => opt.Ignore())
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
