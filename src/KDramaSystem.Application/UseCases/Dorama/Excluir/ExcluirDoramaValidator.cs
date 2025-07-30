using FluentValidation;

namespace KDramaSystem.Application.UseCases.Dorama.Excluir;

public class ExcluirDoramaValidator : AbstractValidator<ExcluirDoramaRequest>
{
    public ExcluirDoramaValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("O Id do dorama é obrigatório.");
    }
}