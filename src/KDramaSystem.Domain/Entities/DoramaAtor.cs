namespace KDramaSystem.Domain.Entities;

public class DoramaAtor
{
    public Guid DoramaId { get; private set; }
    public Guid AtorId { get; private set; }

    public Dorama Dorama { get; private set; }
    public Ator Ator { get; private set; }

    public DoramaAtor(Guid doramaId, Guid atorId)
    {
        if (doramaId == Guid.Empty)
            throw new ArgumentException("DoramaId inválido.");

        if (atorId == Guid.Empty)
            throw new ArgumentException("AtorId inválido.");

        DoramaId = doramaId;
        AtorId = atorId;
    }

    private DoramaAtor() { }
}