using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Domain.ValueObjects
{
    public class TipoAtividade
    {
        public TipoAtividadeEnum Valor { get; }
        private TipoAtividade() { }

        public TipoAtividade(TipoAtividadeEnum valor)
        {
            Valor = valor;
        }

        public override string ToString() => Valor.ToString();

        public override bool Equals(object? obj)
        {
            return obj is TipoAtividade tipoAtividade &&
                   Valor == tipoAtividade.Valor;
        }

        public override int GetHashCode() => Valor.GetHashCode();

        public static implicit operator TipoAtividadeEnum(TipoAtividade tipo) => tipo.Valor;
        public static implicit operator TipoAtividade(TipoAtividadeEnum valor) => new(valor);
    }
}