namespace KDramaSystem.Domain.Entities
{
    public class UsuarioAutenticacao
    {
        public Guid UsuarioId { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }

        public UsuarioAutenticacao(Guid usuarioId, string email, string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.", nameof(email));
            if (string.IsNullOrWhiteSpace(senhaHash)) throw new ArgumentException("Senha é obrigatório.", nameof(senhaHash));

            UsuarioId = usuarioId;
            Email = email;
            SenhaHash = senhaHash;
        }
    }
}