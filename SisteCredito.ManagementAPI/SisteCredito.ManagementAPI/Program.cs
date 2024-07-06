using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SisteCredito.ManagementAPI.Application.Dto;
using SisteCredito.ManagementAPI.Application.Interfaces;
using SisteCredito.ManagementAPI.Application.Services;
using SisteCredito.ManagementAPI.Infraestructure.Data;
using SisteCredito.ManagementAPI.Infraestructure.Repositories;
using System.Text;
using static SisteCredito.ManagementAPI.Domain.Interfaces.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<FileUploadOperationFilter>();
});


var connectionString = builder.Configuration.GetConnectionString("SisteCreditoContext");
builder.Services.AddDbContext<SisteCreditoContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IExtraHourService, ExtraHourService>();
builder.Services.AddScoped<IEncryptDecryptService, EncryptDecryptService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddAutoMapper(typeof(Program));





builder.Services.Configure<AuthenticationDTO>(builder.Configuration.GetSection("Authentication"));
builder.Services.AddSingleton<IOptionsMonitor<AuthenticationDTO>, OptionsMonitor<AuthenticationDTO>>();

builder.Services.Configure<AuthenticationDTO>(builder.Configuration.GetSection("TokenConfiguration"));
builder.Services.AddSingleton<IOptionsMonitor<TokenConfigurationDto>, OptionsMonitor<TokenConfigurationDto>>();


var jwtKey = builder.Configuration.GetSection("Authentication:TokenConfiguration:key");

builder.Services.AddAuthentication(t =>
{
    t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey.Value)),
         ValidateAudience = false,
         ValidateIssuerSigningKey = true,
         ValidateIssuer = false,
         ValidateLifetime = true,
         RequireExpirationTime = true,
         ClockSkew = TimeSpan.Zero
     };
     options.SaveToken = true;
     options.RequireHttpsMetadata = true;
 });


builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

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


app.MapControllers();

app.Run();
