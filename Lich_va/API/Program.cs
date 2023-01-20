using API.Entities;
using API.Repositories;
using GoogleAuth.Services;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using BankDataLibrary.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string json = File.ReadAllText(@"appsettings.json");
JObject o = JObject.Parse(@json);
AppSettings.Instance = JsonConvert.DeserializeObject<AppSettings>(o["AppSettings"].ToString());
AppSettings.Instance.FunnyLittleString = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(AppSettings.Instance.FunnyLittleString));
LichvaContext.ConnectionString = AppSettings.Instance.LichvaConnectionString;

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication()
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JwtSecret"])),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IBankRepository, DBBankRepository>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseAuthentication();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
