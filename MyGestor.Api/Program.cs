using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyGestor.Domain.Interfaces;
using MyGestor.Application.Services;
using MyGestor.Infrastructure.Persistence;
using MyGestor.Infrastructure.Repositories;
using MyGestor.Application.Interfaces;
using MyGestor.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Definindo o nome da política de CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// DbContext com SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência dos Repositories e Services
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
    options.AddPolicy("AdminOrUser", policy =>
        policy.RequireRole("Admin", "Usuario"));
});

// Configurar CORS (Render precisa aceitar qualquer origem)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin() // Permite qualquer origem (necessário no Render)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger é bom para testar localmente

var app = builder.Build();

// Forçar o uso da variável de ambiente PORT para o Render
var port = Environment.GetEnvironmentVariable("PORT");
if (port != null)
{
    app.Urls.Add($"http://*:{port}");
}

// Middlewares
if (app.Environment.IsDevelopment())
{
    // Somente exibir Swagger se estiver rodando local
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usa a política de CORS
app.UseCors(MyAllowSpecificOrigins);

// (Comentado) CORS manual para localhost:4200
// Se você quiser deixar CORS específico só pra local, pode fazer diferente
/*
app.Use((context, next) =>
{
    context.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:4200";
    context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
    return next();
});
*/

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
