﻿namespace MyGestor.Application.DTOs.Auth;

public class RegisterDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

    public string Role { get; set; } = "Usuario";
}
