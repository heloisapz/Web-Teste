using Microsoft.AspNetCore.Mvc;
using WebApiLogin.Models;
using WebApiLogin.Service.UsuarioService;

namespace WebApiLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;

        // 🔐 Chave usada para assinar o JWT (fique à vontade para trocar por uma mais forte)
        private readonly string chaveJwt = "minha-chave-super-secreta-bem-grande-de-teste-123456789";

        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<UsuarioModel>>>> GetUsuarios()
        {
            return Ok(await _usuarioInterface.GetUsuarios());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<UsuarioModel>>> GetUsuarioById(int id)
        {
            var response = await _usuarioInterface.GetUsuarioById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<UsuarioModel>>>> CreateUsuario(UsuarioModel novoUsuario)
        {
            // 🔒 Hasheia a senha antes de salvar
            novoUsuario.Senha = HashHelper.GerarHash(novoUsuario.Senha);
            var response = await _usuarioInterface.CreateUsuario(novoUsuario);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<UsuarioModel>>>> UpdateUsuario(UsuarioModel updateUsuario)
        {
            updateUsuario.Senha = HashHelper.GerarHash(updateUsuario.Senha);
            var response = await _usuarioInterface.UpdateUsuario(updateUsuario);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<UsuarioModel>>>> DeleteUsuario(int id)
        {
            var response = await _usuarioInterface.DeleteUsuario(id);
            return Ok(response);
        }

        [HttpPost("tipo")]
        public async Task<ActionResult<ServiceResponse<object>>> TipoUsuario([FromBody] UsuarioModel loginInfo)
        {
            // Hasheia a senha para comparação
            loginInfo.Senha = HashHelper.GerarHash(loginInfo.Senha);
            var response = await _usuarioInterface.TipoUsuario(loginInfo);
            return Ok(response);
        }
    }
}
