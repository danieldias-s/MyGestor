// DTOs/Usuario/CreateUserDto.cs
namespace MyGestor.Application.DTOs.Auth
{
    public class CreateUserDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
