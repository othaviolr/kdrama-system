using System.Security.Claims;
using KDramaSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KDramaSystem.Infrastructure.Services
{
    public class UsuarioAutenticadoProvider : IUsuarioAutenticadoProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioAutenticadoProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid ObterUsuarioId()
        {
            var id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var guid))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            return guid;
        }

        public string ObterEmail()
        {
            var email = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            return email;
        }
    }
}