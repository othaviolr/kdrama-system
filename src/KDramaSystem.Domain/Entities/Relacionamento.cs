using KDramaSystem.Domain.ValueObjetcs;

namespace KDramaSystem.Domain.Entities;

public class Relacionamento
{
    public Guid Id { get; private set; }

    public Guid AtorPrincipalId { get; private set; }
    public Ator AtorPrincipal { get; private set; }

    public Guid AtorRelacionadoId { get; private set; }
    public Ator AtorRelacionado { get; private set; }

    public TipoRelacionamentoAtor Tipo { get; private set; }

    protected Relacionamento() { }

    public Relacionamento(Guid id, Ator atorPrincipal, Ator atorRelacionado, TipoRelacionamentoAtor tipo)
    {
        if (atorPrincipal == null || atorRelacionado == null)
            throw new ArgumentException("Atores não podem ser nulos.");

        if (atorPrincipal.Id == atorRelacionado.Id)
            throw new ArgumentException("Um ator não pode ter relacionamento com ele mesmo.");

        Id = id;
        AtorPrincipal = atorPrincipal;
        AtorRelacionado = atorRelacionado;
        AtorPrincipalId = atorPrincipal.Id;
        AtorRelacionadoId = atorRelacionado.Id;
        Tipo = tipo;
    }
}