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

// Registrando o seu serviço! Escolha a forma mais adequada ao seu cenário:

// Se você quer uma instância única por requisição (mais comum para serviços de lógica de negócios):
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

// Ou, se você quer uma instância única para toda a aplicação (menos comum para serviços de lógica de negócios com dependências):
// builder.Services.AddSingleton<IUsuarioInterface, UsuarioService>();

// Ou, se você quer uma nova instância a cada vez que for solicitado (menos comum na maioria dos cenários web):
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