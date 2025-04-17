using Microsoft.EntityFrameworkCore;
using WebApiLogin.Models;
using WebAPILogin.DataContext;

namespace WebApiLogin.Service.UsuarioService
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<UsuarioModel>>> CreateUsuario(UsuarioModel novoUsuario)
        {
            ServiceResponse<List<UsuarioModel>> serviceResponse = new ServiceResponse<List<UsuarioModel>>();

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

                serviceResponse.Dados = _context.Usuarios.ToList();
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
            ServiceResponse<UsuarioModel> serviceResponse = new ServiceResponse<UsuarioModel>();

            try
            {
                UsuarioModel usuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);

                if (usuario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = usuario;
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
            ServiceResponse<List<UsuarioModel>> serviceResponse = new ServiceResponse<List<UsuarioModel>>();

            try
            {
                serviceResponse.Dados = _context.Usuarios.ToList();
                if (serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Informar Dados";
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
            ServiceResponse<List<UsuarioModel>> serviceResponse = new ServiceResponse<List<UsuarioModel>>();

            try
            {
                UsuarioModel usuario = _context.Usuarios.AsNoTracking().FirstOrDefault(x => x.Id == updateUsuario.Id);
                if (usuario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Usuarios.Update(updateUsuario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Usuarios.ToList();
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
            ServiceResponse<List<UsuarioModel>> serviceResponse = new ServiceResponse<List<UsuarioModel>>();

            try
            {
                UsuarioModel usuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);
                if (usuario == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Usuário não localizado";
                    serviceResponse.Sucesso = false;
                    return serviceResponse;
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Usuarios.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        // 🔧 ALTERAÇÃO FEITA AQUI 🔽
        public async Task<ServiceResponse<object>> TipoUsuario(UsuarioModel loginInfo)
        {
            var serviceResponse = new ServiceResponse<object>();

            try
            {
                // Comparação com a senha JÁ HASHEADA (hash feito no Controller antes de chegar aqui)
                var usuario = _context.Usuarios.FirstOrDefault(u =>
                    u.Email == loginInfo.Email && u.Senha == loginInfo.Senha);

                if (usuario == null)
                {
                    serviceResponse.Sucesso = false;
                    serviceResponse.Mensagem = "Credenciais inválidas";
                    serviceResponse.Dados = null;
                    return serviceResponse;
                }

                // Envia os dados de tipo de usuário (admin ou comum)
                serviceResponse.Dados = new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Tipo // 0 = usuário comum, 1 = admin
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
