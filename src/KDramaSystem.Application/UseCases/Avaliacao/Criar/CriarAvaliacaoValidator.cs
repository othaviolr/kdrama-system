using FluentValidation;

namespace KDramaSystem.Application.UseCases.Avaliacao.Criar;

public class CriarAvaliacaoValidator : AbstractValidator<CriarAvaliacaoRequest>
{
    public CriarAvaliacaoValidator()
    {
        RuleFor(x => x.TemporadaId)
            .NotEmpty().WithMessage("A temporada é obrigatória.");

        RuleFor(x => x.Nota)
            .InclusiveBetween(1, 5).WithMessage("A nota deve estar entre 1 e 5.");

        RuleFor(x => x.Comentario)
            .MaximumLength(1000).WithMessage("Comentário ultrapassa o limite de 1000 caracteres.");

        RuleFor(x => x)
            .Must(r => !(r.RecomendadoPorUsuarioId.HasValue && !string.IsNullOrWhiteSpace(r.RecomendadoPorNomeLivre)))
            .WithMessage("A recomendação deve ser por usuário ou por nome livre, não ambos.");
    }
}