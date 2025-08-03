using FluentValidation;

namespace KDramaSystem.Application.UseCases.Ator.Obter;

public class ObterAtorValidator : AbstractValidator<ObterAtorRequest>
{
    public ObterAtorValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id é obrigatório.");
    }
}