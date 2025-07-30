using FluentValidation;

namespace KDramaSystem.Application.UseCases.Dorama.Criar;

public class CriarDoramaValidator : AbstractValidator<CriarDoramaRequest>
{
    public CriarDoramaValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(100).WithMessage("O título pode ter no máximo 100 caracteres.");

        RuleFor(x => x.TituloOriginal)
            .MaximumLength(100).WithMessage("O título original pode ter no máximo 100 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.TituloOriginal));

        RuleFor(x => x.PaisOrigem)
            .NotEmpty().WithMessage("O país de origem é obrigatório.")
            .MaximumLength(60).WithMessage("O país de origem pode ter no máximo 60 caracteres.");

        RuleFor(x => x.AnoLancamento)
            .InclusiveBetween(1950, DateTime.Now.Year + 1)
            .WithMessage($"Ano de lançamento deve ser entre 1950 e {DateTime.Now.Year + 1}.");

        RuleFor(x => x.GeneroIds)
            .NotNull().WithMessage("A lista de gêneros é obrigatória.")
            .Must(g => g.Any()).WithMessage("É necessário informar pelo menos um gênero.");

        RuleFor(x => x.Plataforma)
            .IsInEnum().WithMessage("Plataforma inválida.");

        RuleFor(x => x.Sinopse)
            .MaximumLength(1000).WithMessage("A sinopse pode ter no máximo 1000 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Sinopse));

        RuleFor(x => x.AtorIds)
            .Must(lista => lista!.Distinct().Count() == lista.Count)
            .WithMessage("A lista de atores contém IDs duplicados.")
            .When(x => x.AtorIds is not null && x.AtorIds.Any());

        RuleFor(x => x.ImagemCapaUrl)
            .NotEmpty().WithMessage("A imagem de capa é obrigatória.")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("A URL da imagem de capa é inválida.");
    }
}