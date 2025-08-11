using FluentValidation;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarProgresso;

public class AtualizarProgressoTemporadaValidator : AbstractValidator<AtualizarProgressoTemporadaRequest>
{
    public AtualizarProgressoTemporadaValidator()
    {
        RuleFor(x => x.TemporadaId)
            .NotEmpty().WithMessage("O Id da temporada é obrigatório.");

        RuleFor(x => x.EpisodiosAssistidos)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade de episódios assistidos não pode ser negativa.");
    }
}