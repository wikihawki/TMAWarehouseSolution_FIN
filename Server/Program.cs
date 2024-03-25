using Azure.Core;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

// Starting
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Sorry, your connection is invalid"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidateLifetime = true,
    ValidIssuer = jwtSection!.Issuer,
    ValidAudience = jwtSection.Audience,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
});


builder.Services.AddScoped<IUserAccount, UserAccountRepository>();
builder.Services.AddScoped<IItemsManagement, ManageItemsRepository>();
builder.Services.AddScoped<IItemRequestsManagement, ManageRequestsRepository>();


builder.Services.AddScoped<IGenericRepositoryInterface<Status>, StatusRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Item>, ItemRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<ItemGroup>, ItemGroupRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<ItemRequest>, ItemRequestRepository>();
builder.Services.AddScoped<IGenericRepositoryInterface<Unit>, UnitRepository>();






builder.Services.AddCors(options =>
options.AddPolicy("AllowBlazorWasm", builder => builder.WithOrigins("http://localhost:7174", "https://localhost:7270").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorWasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
