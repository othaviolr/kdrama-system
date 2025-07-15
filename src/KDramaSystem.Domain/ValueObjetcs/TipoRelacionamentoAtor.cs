using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Domain.ValueObjetcs;

public class TipoRelacionamentoAtor
{
    public TipoRelacionamentoAtorEnum Tipo { get; private set; }

    protected TipoRelacionamentoAtor() { } 

    public TipoRelacionamentoAtor(TipoRelacionamentoAtorEnum tipo)
    {
        Tipo = tipo;
    }

    public override string ToString() => Tipo.ToString();

    public override bool Equals(object? obj)
    {
        return obj is TipoRelacionamentoAtor other &&
               Tipo == other.Tipo;
    }

    public override int GetHashCode() => Tipo.GetHashCode();
}