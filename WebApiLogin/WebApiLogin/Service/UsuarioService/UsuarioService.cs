using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiLogin.Models;
using WebAPILogin.DataContext;

namespace WebApiLogin.Service.UsuarioService
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtKey = "minha-chave-super-secreta-bem-grande-de-teste-123456789";

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<UsuarioModel>>> CreateUsuario(UsuarioModel novoUsuario)
        {
            var serviceResponse = new ServiceResponse<List<UsuarioModel>>();

            try
            {
                if (novoUsuario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar Dados";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Add(novoUsuario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<UsuarioModel>> GetUsuarioById(int id)
        {
            var serviceResponse = new ServiceResponse<UsuarioModel>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

                if (usuario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado";
                    serviceResponse.Sucesso = false;
                }
                else
                {
                    serviceResponse.Dados = usuario;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UsuarioModel>>> GetUsuarios()
        {
            var serviceResponse = new ServiceResponse<List<UsuarioModel>>();

            try
            {
                serviceResponse.Dados = await _context.Usuarios.ToListAsync();
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum usuário encontrado";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UsuarioModel>>> UpdateUsuario(UsuarioModel updateUsuario)
        {
            var serviceResponse = new ServiceResponse<List<UsuarioModel>>();

            try
            {
                var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == updateUsuario.Id);
                if (usuario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Usuarios.Update(updateUsuario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UsuarioModel>>> DeleteUsuario(int id)
        {
            var serviceResponse = new ServiceResponse<List<UsuarioModel>>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
                if (usuario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = await _context.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<object>> TipoUsuario(LoginDTO loginInfo)
        {
            var serviceResponse = new ServiceResponse<object>();

            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginInfo.Email);

                if (usuario == null || !VerificarSenha(loginInfo.Senha, usuario.Senha))
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Credenciais inválidas";
                    return serviceResponse;
                }

                // Gerar o token JWT
                var token = GerarToken(usuario);

                // Resposta com o tipo de usuário e o token
                serviceResponse.Sucesso = true;
                serviceResponse.Mensagem = "Login realizado com sucesso";
                serviceResponse.Dados = new
                {
                    token,
                    tipo = usuario.Tipo,  // Tipo de usuário (admin ou comum)
                    nome = usuario.Nome,
                    id = usuario.Id
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Sucesso = false;
                serviceResponse.Mensagem = ex.Message;
            }

            return serviceResponse;
        }

        private string GerarToken(UsuarioModel usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static bool VerificarSenha(string senhaDigitada, string senhaHashSalva)
        {
            var hashDaDigitada = HashHelper.GerarHash(senhaDigitada);
            return hashDaDigitada == senhaHashSalva;
        }
        public async Task<bool> VerificarEmailExistente(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

    }
}