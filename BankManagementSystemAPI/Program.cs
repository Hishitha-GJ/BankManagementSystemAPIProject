using Microsoft.EntityFrameworkCore;
using BankManagementSystemAPI.Data;
using BankManagementSystemAPI.Repositories;
using BankManagementSystemAPI.Services;
using BankManagementSystemAPI.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// --- Add services to the container ---
builder.Services.AddControllers()
    .AddNewtonsoftJson(options => {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- DbContext ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Repositories ---
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<BankRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<UserRepository>();

// --- Services ---
builder.Services.AddScoped<IAccount, AccountService>();
builder.Services.AddScoped<IBank, BankService>();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<ITransaction, TransactionService>();
builder.Services.AddScoped<IUser, UserService>();

// --- JWT Service ---
builder.Services.AddScoped<IJWTService, JWTService>();

// --- Configure Authentication ---
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});

var app = builder.Build();

// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ?? Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

