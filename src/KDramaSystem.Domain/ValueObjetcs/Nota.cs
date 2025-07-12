namespace KDramaSystem.Domain.ValueObjetcs
{
    public class Nota
    {
        public int Valor { get; }

        public Nota(int valor)
        {
            if (valor < 1 || valor > 5)
                throw new ArgumentException("A nota deve estar entre 1 e 5.");
            
            Valor = valor;
        }

        public override bool Equals(object? obj)
        {
            return obj is Nota nota &&
                   Valor == nota.Valor;
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }

        public override string ToString() => Valor.ToString();
    }
}