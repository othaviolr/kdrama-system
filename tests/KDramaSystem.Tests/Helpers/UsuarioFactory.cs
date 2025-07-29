using KDramaSystem.Domain.Entities;
using System;

namespace KDramaSystem.Tests.Helpers
{
    public static class UsuarioFactory
    {
        public static Usuario CriarUsuario(
            Guid? id = null,
            string nome = "Usuario Teste",
            string nomeUsuario = "usuarioTeste",
            string email = "teste@email.com",
            string? fotoUrl = null,
            string? bio = null)
        {
            return new Usuario(
                id ?? Guid.NewGuid(),
                nome,
                nomeUsuario,
                email,
                fotoUrl,
                bio);
        }
    }
}