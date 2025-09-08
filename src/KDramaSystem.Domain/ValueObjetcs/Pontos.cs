namespace KDramaSystem.Domain.ValueObjetcs;

public class Pontos
{
    public int Valor { get; private set; }

    public Pontos(int valor)
    {
        if (valor < 0)
            throw new ArgumentException("Pontos não podem ser negativos.");
        Valor = valor;
    }
}