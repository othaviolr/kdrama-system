namespace KDramaSystem.Domain.Entities
{
    public class Episodio
    {
        public Guid Id { get; private set; }
        public Guid TemporadaId { get; private set; }
        public int Numero { get; private set; }
        public string Titulo { get; private set; }
        public string? Sinopse { get; private set; }
        public int DuracaoMinutos { get; private set; }

        public Episodio(Guid id, Guid temporadaId, int numero, string titulo, int duracaoMinutos, string? sinopse = null)
        {
            if (numero <= 0) throw new ArgumentException("Número do episódio deve ser maior que zero.");
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.");
            if (duracaoMinutos <= 0) throw new ArgumentException("Duração deve ser maior que zero minutos.");

            Id = id;
            TemporadaId = temporadaId;
            Numero = numero;
            Titulo = titulo;
            DuracaoMinutos = duracaoMinutos;
            Sinopse = sinopse;
        }
    }
}