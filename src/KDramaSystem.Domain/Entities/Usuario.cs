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

        private readonly List<Usuario> _seguindo = new();
        public IReadOnlyCollection<Usuario> Seguindo => _seguindo.AsReadOnly();

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

        private Usuario() { } 

        public Usuario(Guid id, string nome, string nomeUsuario, string email, string? fotoUrl = null, string? bio = null)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(nomeUsuario)) throw new ArgumentException("Nome de usuário é obrigatório.");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.");

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
            if (_seguindo.Any(u => u.Id == usuario.Id)) return;

            _seguindo.Add(usuario);
            usuario._seguidores.Add(this);
        }

        public void DeixarDeSeguir(Usuario usuario)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));

            var seguindo = _seguindo.FirstOrDefault(u => u.Id == usuario.Id);
            if (seguindo is not null)
                _seguindo.Remove(seguindo);

            var seguidor = usuario._seguidores.FirstOrDefault(u => u.Id == Id);
            if (seguidor is not null)
                usuario._seguidores.Remove(seguidor);
        }

        public void AdicionarAvaliacao(Avaliacao avaliacao)
        {
            if (avaliacao == null) throw new ArgumentNullException(nameof(avaliacao));
            _avaliacoes.Add(avaliacao);
        }

        public void AdicionarProgresso(ProgressoTemporada progresso)
        {
            if (progresso == null) throw new ArgumentNullException(nameof(progresso));
            _progresso.Add(progresso);
        }

        public void AdicionarComentario(Comentario comentario)
        {
            if (comentario == null) throw new ArgumentNullException(nameof(comentario));
            _comentarios.Add(comentario);
        }

        public void AdicionarLista(ListaPrateleira lista)
        {
            if (lista == null) throw new ArgumentNullException(nameof(lista));
            _listas.Add(lista);
        }

        public void RegistrarAtividade(Atividade atividade)
        {
            if (atividade == null) throw new ArgumentNullException(nameof(atividade));
            _atividades.Add(atividade);
        }

        public void AtualizarBio(string? novaBio)
        {
            Bio = novaBio?.Trim();
        }

        public void AtualizarFoto(string? novaFotoUrl)
        {
            FotoUrl = novaFotoUrl?.Trim();
        }

        public void EditarPerfil(string nome, string nomeUsuario, string? fotoUrl, string? bio)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(nomeUsuario))
                throw new ArgumentException("Nome de usuário é obrigatório.");

            Nome = nome;
            NomeUsuario = nomeUsuario;
            FotoUrl = fotoUrl;
            Bio = bio;
        }
    }
}