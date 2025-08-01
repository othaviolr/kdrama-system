using FluentValidation;

namespace KDramaSystem.Application.UseCases.Genero.Criar;

public class CriarGeneroValidator : AbstractValidator<CriarGeneroRequest>
{
    public CriarGeneroValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome do gênero é obrigatório.")
            .MaximumLength(100).WithMessage("Nome do gênero deve ter no máximo 100 caracteres.");
    }
}