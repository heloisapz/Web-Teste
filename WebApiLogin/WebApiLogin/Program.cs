using Microsoft.EntityFrameworkCore;
using WebAPILogin.DataContext;
using WebApiLogin.Service.UsuarioService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar o DbContext com a connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Registrando o seu servi�o! Escolha a forma mais adequada ao seu cen�rio:

// Se voc� quer uma inst�ncia �nica por requisi��o (mais comum para servi�os de l�gica de neg�cios):
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

// Ou, se voc� quer uma inst�ncia �nica para toda a aplica��o (menos comum para servi�os de l�gica de neg�cios com depend�ncias):
// builder.Services.AddSingleton<IUsuarioInterface, UsuarioService>();

// Ou, se voc� quer uma nova inst�ncia a cada vez que for solicitado (menos comum na maioria dos cen�rios web):
// builder.Services.AddTransient<IUsuarioInterface, UsuarioService>();


var chaveJwt = "minha-chave-super-secreta-bem-grande-de-teste-123456789";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(chaveJwt)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll"); // Adicionado no final do program

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();