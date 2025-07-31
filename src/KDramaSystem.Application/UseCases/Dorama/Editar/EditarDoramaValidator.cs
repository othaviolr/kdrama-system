using FluentValidation;

namespace KDramaSystem.Application.UseCases.Dorama.Editar;

public class EditarDoramaValidator : AbstractValidator<EditarDoramaRequest>
{
    public EditarDoramaValidator()
    {
        RuleFor(x => x.DoramaId)
            .NotEmpty().WithMessage("Id do dorama é obrigatório.");

        RuleFor(x => x.UsuarioEditorId)
            .NotEmpty().WithMessage("Id do usuário editor é obrigatório.");

        RuleFor(x => x.Titulo)
            .MaximumLength(100).WithMessage("O título pode ter no máximo 100 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Titulo));

        RuleFor(x => x.TituloOriginal)
            .MaximumLength(100).WithMessage("O título original pode ter no máximo 100 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.TituloOriginal));

        RuleFor(x => x.PaisOrigem)
            .MaximumLength(60).WithMessage("O país de origem pode ter no máximo 60 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.PaisOrigem));

        RuleFor(x => x.AnoLancamento)
            .InclusiveBetween(1950, DateTime.Now.Year + 1)
            .WithMessage($"Ano de lançamento deve ser entre 1950 e {DateTime.Now.Year + 1}.")
            .When(x => x.AnoLancamento.HasValue);

        RuleFor(x => x.GeneroIds)
            .Must(list => list == null || list.Distinct().Count() == list.Count)
            .WithMessage("A lista de gêneros não pode conter duplicatas.");

        RuleFor(x => x.Plataforma)
            .IsInEnum().WithMessage("Plataforma inválida.")
            .When(x => x.Plataforma.HasValue);

        RuleFor(x => x.ImagemCapaUrl)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("A URL da imagem de capa é inválida.")
            .When(x => !string.IsNullOrWhiteSpace(x.ImagemCapaUrl));

        RuleFor(x => x.Sinopse)
            .MaximumLength(1000).WithMessage("A sinopse pode ter no máximo 1000 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Sinopse));
    }
}