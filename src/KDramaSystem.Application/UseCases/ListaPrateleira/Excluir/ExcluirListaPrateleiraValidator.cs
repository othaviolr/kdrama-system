using FluentValidation;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Excluir;

public class ExcluirListaPrateleiraValidator : AbstractValidator<ExcluirListaPrateleiraRequest>
{
    public ExcluirListaPrateleiraValidator()
    {
        RuleFor(x => x.ListaId)
            .NotEmpty().WithMessage("O Id da lista é obrigatório.");

        RuleFor(x => x.UsuarioId)
            .NotEmpty().WithMessage("O Id do usuário é obrigatório.");
    }
}