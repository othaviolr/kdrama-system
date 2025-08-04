using System.Globalization;

namespace KDramaSystem.Domain.ValueObjects
{
    public class ComentarioValor
    {
        public string Texto { get; }

        private ComentarioValor() { }

        public ComentarioValor(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("Comentário não pode ser vazio.");

            if (texto.Length > 1000)
                throw new ArgumentException("Comentário ultrapassa o limite de 1000 caracteres.");

            Texto = texto.Trim();
        }

        public override bool Equals(object? obj)
        {
            return obj is ComentarioValor outro && Texto == outro.Texto;
        }

        public override int GetHashCode()
        {
            return Texto.GetHashCode();
        }

        public override string ToString() => Texto;
    }
}