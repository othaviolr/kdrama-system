namespace KDramaSystem.Domain.Entities;

public class Temporada
{
    public Guid Id { get; private set; }
    public Guid DoramaId { get; private set; }
    public int Numero { get; private set; }
    public string? Nome { get; private set; }
    public int AnoLancamento { get; private set; }
    public bool EmExibicao { get; private set; }
    public string? Sinopse { get; private set; }

    public virtual ICollection<Episodio> Episodios { get; private set; } = new List<Episodio>();

    public int NumeroEpisodios => Episodios.Count;

    protected Temporada() { }

    public Temporada(Guid id, Guid doramaId, int numero, int anoLancamento, bool emExibicao, string? nome = null, string? sinopse = null)
    {
        if (numero <= 0)
            throw new ArgumentNullException(nameof(numero), "Número da temporada deve ser maior que zero.");

        var anoAtual = DateTime.UtcNow.Year;
        if (anoLancamento < 1950 || anoLancamento > anoAtual + 1)
            throw new InvalidOperationException("Ano de lançamento inválido.");

        Id = id;
        DoramaId = doramaId;
        Numero = numero;
        AnoLancamento = anoLancamento;
        EmExibicao = emExibicao;
        Nome = nome?.Trim();
        Sinopse = sinopse?.Trim();
    }

    public void AdicionarEpisodio(Episodio episodio)
    {
        if (episodio is null)
            throw new ArgumentNullException(nameof(episodio), "Episódio não pode ser nulo.");

        if (Episodios.Any(e => e.Numero == episodio.Numero))
            throw new InvalidOperationException($"Já existe um episódio número {episodio.Numero} nesta temporada.");

        Episodios.Add(episodio);
    }

    public void MarcarComoEncerrada()
    {
        EmExibicao = false;
    }

    public void AtualizarSinopse(string? sinopse)
    {
        Sinopse = sinopse;
    }

    public void AtualizarNome(string? nome)
    {
        Nome = nome;
    }

    public void AtualizarAnoLancamento(int ano)
    {
        if (ano <= 1900 || ano > DateTime.UtcNow.Year + 1)
            throw new ArgumentException("Ano de lançamento inválido.", nameof(ano));
        AnoLancamento = ano;
    }

    public void ReabrirTemporada()
    {
        EmExibicao = true;
    }
}