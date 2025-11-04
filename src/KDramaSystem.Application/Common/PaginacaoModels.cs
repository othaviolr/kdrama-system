namespace KDramaSystem.Application.Common;

public class PaginacaoRequest
{
    private int _pagina = 1;
    private int _tamanhoPagina = 20;

    public int Pagina
    {
        get => _pagina;
        set => _pagina = value < 1 ? 1 : value;
    }

    public int TamanhoPagina
    {
        get => _tamanhoPagina;
        set => _tamanhoPagina = value switch
        {
            < 1 => 20,
            > 100 => 100,
            _ => value
        };
    }

    public int Skip => (Pagina - 1) * TamanhoPagina;
}

public class PaginacaoResponse<T>
{
    public List<T> Itens { get; set; } = new();
    public int PaginaAtual { get; set; }
    public int TamanhoPagina { get; set; }
    public int TotalItens { get; set; }
    public int TotalPaginas => (int)Math.Ceiling(TotalItens / (double)TamanhoPagina);
    public bool TemPaginaAnterior => PaginaAtual > 1;
    public bool TemProximaPagina => PaginaAtual < TotalPaginas;
}