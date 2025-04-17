namespace WebApiLogin.Models
{
    public class ServiceResponse<T>
    {
        public T? Dados { get; set; }
        public bool Sucesso { get; set; } = true;
        public string Mensagem { get; set; } = string.Empty;
    }
}
