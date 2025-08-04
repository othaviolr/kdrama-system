using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Domain.ValueObjects
{
    public class StatusDorama
    {
        public StatusDoramaEnum Valor { get; }

        private StatusDorama() { }

        public StatusDorama(StatusDoramaEnum valor)
        {
            Valor = valor;
        }

        public bool EhFinalizado() =>
            Valor == StatusDoramaEnum.Concluido || Valor == StatusDoramaEnum.Abandonado;

        public override string ToString() => Valor.ToString();

        public override bool Equals(object? obj)
        {
            return obj is StatusDorama outro && Valor == outro.Valor;
        }

        public override int GetHashCode() => Valor.GetHashCode();
    }
}