using FluentValidation;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarStatus;

public class AtualizarStatusTemporadaValidator : AbstractValidator<AtualizarStatusTemporadaRequest>
{
    public AtualizarStatusTemporadaValidator()
    {
        RuleFor(x => x.TemporadaId)
            .NotEmpty().WithMessage("O Id da temporada é obrigatório.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status é inválido.");
    }
}