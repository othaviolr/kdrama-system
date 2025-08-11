using FluentValidation;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.ExcluirProgresso;

public class ExcluirProgressoTemporadaValidator : AbstractValidator<ExcluirProgressoTemporadaRequest>
{
    public ExcluirProgressoTemporadaValidator()
    {
        RuleFor(x => x.TemporadaId)
            .NotEmpty().WithMessage("O Id da temporada é obrigatório.");
    }
}