using FluentValidation;

namespace KDramaSystem.Application.UseCases.Temporada.Excluir;

public class ExcluirTemporadaValidator : AbstractValidator<ExcluirTemporadaRequest>
{
    public ExcluirTemporadaValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id da temporada é obrigatório.");
    }
}