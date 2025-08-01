using FluentValidation;

namespace KDramaSystem.Application.UseCases.Genero.Editar;

public class EditarGeneroValidator : AbstractValidator<EditarGeneroRequest>
{
    public EditarGeneroValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id do gênero é obrigatório.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome do gênero é obrigatório.")
            .MaximumLength(100).WithMessage("Nome do gênero deve ter no máximo 100 caracteres.");
    }
}