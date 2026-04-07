using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApiCadastro.Data;
using WebApiCadastro.Mapping;
using WebApiCadastro.Services.Senha;
using WebApiCadastro.Services.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApiCadastro.Services.Auditorias;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =  new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ISenhaService, SenhaService>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference("/scalar", op =>
    {
        op.Title = "Minha API - Scalar";
        op.Theme = ScalarTheme.DeepSpace;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
