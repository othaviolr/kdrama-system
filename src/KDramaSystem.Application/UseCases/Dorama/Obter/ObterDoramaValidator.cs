using FluentValidation;

namespace KDramaSystem.Application.UseCases.Dorama.Obter;

public class ObterDoramaValidator : AbstractValidator<ObterDoramaRequest>
{
    public ObterDoramaValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id do dorama é obrigatório.");
    }
}