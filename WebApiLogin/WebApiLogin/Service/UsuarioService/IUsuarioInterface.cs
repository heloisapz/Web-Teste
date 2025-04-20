using WebApiLogin.Models;

namespace WebApiLogin.Service.UsuarioService
{
    public interface IUsuarioInterface
    {
        Task<ServiceResponse<List<UsuarioModel>>> GetUsuarios();
        Task<ServiceResponse<UsuarioModel>> GetUsuarioById(int id);
        Task<ServiceResponse<List<UsuarioModel>>> CreateUsuario(UsuarioModel novoUsuario);
        Task<ServiceResponse<List<UsuarioModel>>> UpdateUsuario(UsuarioModel novoUsuario);
        Task<ServiceResponse<List<UsuarioModel>>> DeleteUsuario(int id);
        Task<ServiceResponse<object>> TipoUsuario(LoginDTO loginInfo);
        Task<bool> VerificarEmailExistente(string email);

    }
}
