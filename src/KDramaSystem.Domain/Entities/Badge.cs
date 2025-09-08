using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Domain.Entities
{
    public class Badge
    {
        public Guid Id { get; private set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; private set; } = null!;
        public RaridadeBadge Raridade { get; private set; }
        public int Pontos { get; private set; }
        public CategoriaBadge Categoria { get; private set; }
        public string Condicao { get; private set; } = null!;
        public string IconeUrl { get; private set; } = null!;

        public Badge(Guid id, string nome, string descricao, RaridadeBadge raridade, int pontos, string condicao, string iconeUrl, CategoriaBadge categoria)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Raridade = raridade;
            Pontos = pontos;
            Condicao = condicao;
            IconeUrl = iconeUrl;
            Categoria = categoria;
        }
    }
}