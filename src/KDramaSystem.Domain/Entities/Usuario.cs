namespace KDramaSystem.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string NomeUsuario { get; private set; }
        public string Email { get; private set; }
        public string? FotoUrl { get; private set; }
        public string? Bio { get; private set; }

        private readonly List<Usuario> _seguidores = new();
        public IReadOnlyCollection<Usuario> Seguidores => _seguidores.AsReadOnly();

        private readonly List<Avaliacao> _avaliacoes = new();
        public IReadOnlyCollection<Avaliacao> Avaliacoes => _avaliacoes.AsReadOnly();

        private readonly List<ProgressoTemporada> _progresso = new();
        public IReadOnlyCollection<ProgressoTemporada> Progresso => _progresso.AsReadOnly();

        private readonly List<Comentario> _comentarios = new();
        public IReadOnlyCollection<Comentario> Comentarios => _comentarios.AsReadOnly();

        private readonly List<ListaPrateleira> _listas = new();
        public IReadOnlyCollection<ListaPrateleira> Listas => _listas.AsReadOnly();

        private readonly List<Atividade> _atividades = new();
        public IReadOnlyCollection<Atividade> Atividades => _atividades.AsReadOnly();

        public Usuario(Guid id, string nome, string nomeUsuario, string email, string? fotoUrl = null, string? bio = null)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));
            if (string.IsNullOrWhiteSpace(nomeUsuario)) throw new ArgumentException("Nome de usuário é obrigatório", nameof(nomeUsuario));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.", nameof(email));

            Id = id;
            Nome = nome;
            NomeUsuario = nomeUsuario;
            Email = email;
            FotoUrl = fotoUrl;
            Bio = bio;
        }

        public void Seguir(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            if (usuario.Id == Id) throw new InvalidOperationException("Usuário não pode seguir a si mesmo.");
            if (_seguidores.Any(u => u.Id == usuario.Id)) return;

            _seguidores.Add(usuario);
        }

        public void DeixarDeSeguir(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            var seguidor = _seguidores.FirstOrDefault(u => u.Id == usuario.Id);
            if (seguidor != null)
            {
                _seguidores.Remove(seguidor);
            }
        } 
    }
}