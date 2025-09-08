using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;

public static class BadgeCatalogo
{
    public static List<Badge> ObterTodas()
    {
        return new List<Badge>
        {
            //CONQUISTAS DE PROGRESSÃO
            new Badge(Guid.NewGuid(), "Primeiro Passo", "Assistiu seu primeiro dorama", RaridadeBadge.Comum, 1, "Assistir 1 dorama", "/icons/primeiro-passo.png", CategoriaBadge.Progressao),
            new Badge(Guid.NewGuid(), "Novato", "Assistiu 5 doramas", RaridadeBadge.Comum, 2, "Assistir 5 doramas", "/icons/novato.png", CategoriaBadge.Progressao),
            new Badge(Guid.NewGuid(), "Entusiasta", "Assistiu 15 doramas", RaridadeBadge.Comum, 3, "Assistir 15 doramas", "/icons/entusiasta.png", CategoriaBadge.Progressao),
            new Badge(Guid.NewGuid(), "Aficionado", "Assistiu 30 doramas", RaridadeBadge.Comum, 5, "Assistir 30 doramas", "/icons/aficionado.png", CategoriaBadge.Progressao),
            new Badge(Guid.NewGuid(), "Expert", "Assistiu 50 doramas", RaridadeBadge.Epica, 8, "Assistir 50 doramas", "/icons/expert.png", CategoriaBadge.Progressao),
            new Badge(Guid.NewGuid(), "Mestre", "Assistiu 100 doramas", RaridadeBadge.Epica, 15, "Assistir 100 doramas", "/icons/mestre.png", CategoriaBadge.Progressao),
            new Badge(Guid.NewGuid(), "Mestre", "Assistiu 200 doramas", RaridadeBadge.Lendaria, 30, "Assistir 200 doramas", "/icons/mestre.png", CategoriaBadge.Progressao),

            //CONQUISTAS DE EPISÓDIOS
            new Badge(Guid.NewGuid(), "Maratonista", "Assistiu 50 episódios", RaridadeBadge.Comum, 2, "Assistir 50 episódios", "/icons/maratonista.png", CategoriaBadge.Episodios),
            new Badge(Guid.NewGuid(), "Viciado", "Assistiu 100 episódios", RaridadeBadge.Rara, 4, "Assistir 100 episódios", "/icons/viciado.png", CategoriaBadge.Episodios),
            new Badge(Guid.NewGuid(), "Insaciável", "Assistiu 300 episódios", RaridadeBadge.Epica, 8, "Assistir 300 episódios", "/icons/insaciavel.png", CategoriaBadge.Episodios),
            new Badge(Guid.NewGuid(), "Lendário", "Assistiu 500 episódios", RaridadeBadge.Lendaria, 15, "Assistir 500 episódios", "/icons/lendario.png", CategoriaBadge.Episodios),

            // CONQUISTAS DE GÊNEROS
            new Badge(Guid.NewGuid(), "Romântico", "Assistiu 10 doramas românticos", RaridadeBadge.Comum, 3, "Assistir 10 doramas românticos", "/icons/romantico.png", CategoriaBadge.Genero),
            new Badge(Guid.NewGuid(), "Detetive", "Assistiu 5 doramas de mistério/crime", RaridadeBadge.Comum, 3, "Assistir 5 doramas de mistério", "/icons/detetive.png", CategoriaBadge.Genero),
            new Badge(Guid.NewGuid(), "Comédia", "Assistiu 8 doramas de comédia", RaridadeBadge.Comum, 2, "Assistir 8 doramas de comédia", "/icons/comedia.png", CategoriaBadge.Genero),
            new Badge(Guid.NewGuid(), "Melodrama", "Assistiu 6 melodramas", RaridadeBadge.Comum, 2, "Assistir 6 melodramas", "/icons/melodrama.png", CategoriaBadge.Genero),
            new Badge(Guid.NewGuid(), "Ação", "Assistiu 5 doramas de ação", RaridadeBadge.Rara, 4, "Assistir 5 doramas de ação", "/icons/acao.png", CategoriaBadge.Genero),
            new Badge(Guid.NewGuid(), "Histórico", "Assistiu 5 doramas históricos", RaridadeBadge.Rara, 4, "Assistir 5 doramas históricos", "/icons/historico.png", CategoriaBadge.Genero),

            //CONQUISTAS DE PAÍSES
            new Badge(Guid.NewGuid(), "K-Drama Lover", "Assistiu 20 K-dramas", RaridadeBadge.Comum, 4, "Assistir 20 K-dramas", "/icons/k-drama-lover.png", CategoriaBadge.Pais),
            new Badge(Guid.NewGuid(), "J-Drama Explorer", "Assistiu 8 J-dramas", RaridadeBadge.Rara, 5, "Assistir 8 J-dramas", "/icons/j-drama-explorer.png", CategoriaBadge.Pais),
            new Badge(Guid.NewGuid(), "C-Drama Fan", "Assistiu 10 C-dramas", RaridadeBadge.Rara, 5, "Assistir 10 C-dramas", "/icons/c-drama-fan.png", CategoriaBadge.Pais),
            new Badge(Guid.NewGuid(), "Thai Drama", "Assistiu 5 dramas tailandeses", RaridadeBadge.Rara, 4, "Assistir 5 dramas tailandeses", "/icons/thai-drama.png", CategoriaBadge.Pais),
            new Badge(Guid.NewGuid(), "Explorador Global", "Assistiu doramas de 4 países diferentes", RaridadeBadge.Epica, 8, "Assistir doramas de 4 países", "/icons/explorador-global.png", CategoriaBadge.Pais),

            //CONQUISTAS DE AVALIAÇÃO
            new Badge(Guid.NewGuid(), "Crítico", "Avaliou 20 doramas", RaridadeBadge.Comum, 3, "Avaliar 20 doramas", "/icons/critico.png", CategoriaBadge.Avaliacao),
            new Badge(Guid.NewGuid(), "Fã Incondicional", "Deu nota 5 para 10 doramas", RaridadeBadge.Rara, 4, "Dar nota 5 para 10 doramas", "/icons/fa-incondicional.png", CategoriaBadge.Avaliacao),
            new Badge(Guid.NewGuid(), "Exigente", "Mantém média de avaliação abaixo de 4", RaridadeBadge.Rara, 3, "Média 4 em 15+ doramas", "/icons/exigente.png", CategoriaBadge.Avaliacao),
            new Badge(Guid.NewGuid(), "Equilibrado", "Mantém média entre 4-5", RaridadeBadge.Comum, 2, "Média 4-5 em 15+ doramas", "/icons/equilibrado.png", CategoriaBadge.Avaliacao),

            //CONQUISTAS DE FINALIZAÇÃO
            new Badge(Guid.NewGuid(), "Finalizador", "Completou 15 doramas", RaridadeBadge.Comum, 3, "Completar 15 doramas", "/icons/finalizador.png", CategoriaBadge.Finalizacao),
            new Badge(Guid.NewGuid(), "Completista", "Completou 40 doramas", RaridadeBadge.Rara, 6, "Completar 40 doramas", "/icons/completista.png", CategoriaBadge.Finalizacao),
            new Badge(Guid.NewGuid(), "Perfeccionista", "Completou 70 doramas", RaridadeBadge.Epica, 10, "Completar 70 doramas", "/icons/perfeccionista.png", CategoriaBadge.Finalizacao),

            //CONQUISTAS DE DESCOBERTA
            new Badge(Guid.NewGuid(), "Garimpeiro", "Descobriu 5 doramas com nota 2", RaridadeBadge.Rara, 4, "5 doramas com nota baixa", "/icons/garimpeiro.png", CategoriaBadge.Descoberta),
            new Badge(Guid.NewGuid(), "Vintage", "Assistiu 8 doramas de antes de 2015", RaridadeBadge.Rara, 5, "8 doramas pré-2015", "/icons/vintage.png", CategoriaBadge.Descoberta),
            new Badge(Guid.NewGuid(), "Atualizado", "Assistiu 15 doramas de 2023-2024", RaridadeBadge.Comum, 3, "15 doramas recentes", "/icons/atualizado.png", CategoriaBadge.Descoberta),

            //CONQUISTAS DE ATORES
            new Badge(Guid.NewGuid(), "Stalker de Ator", "Assistiu 5 doramas do mesmo ator", RaridadeBadge.Comum, 3, "5 doramas do mesmo ator", "/icons/stalker-ator.png", CategoriaBadge.Ator),
            new Badge(Guid.NewGuid(), "Fã Clube", "Assistiu 8 doramas do mesmo ator", RaridadeBadge.Rara, 5, "8 doramas do mesmo ator", "/icons/fa-clube.png", CategoriaBadge.Ator),

            //CONQUISTAS DE LISTA/ORGANIZAÇÃO
            new Badge(Guid.NewGuid(), "Organizador", "Criou 3 listas personalizadas", RaridadeBadge.Comum, 2, "Criar 3 listas", "/icons/organizador.png", CategoriaBadge.Lista),
            new Badge(Guid.NewGuid(), "Curador", "Adicionou 30 doramas em listas", RaridadeBadge.Rara, 4, "30 doramas em listas", "/icons/curador.png", CategoriaBadge.Lista),

            //CONQUISTAS DE REWATCH
            new Badge(Guid.NewGuid(), "Nostálgico", "Reassistiu 3 doramas", RaridadeBadge.Rara, 4, "Reassistir 3 doramas", "/icons/nostalgico.png", CategoriaBadge.Rewatch),

            //CONQUISTAS ESPECIAIS
            new Badge(Guid.NewGuid(), "Primeiro do Dia", "Primeiro a avaliar dorama lançado", RaridadeBadge.Epica, 8, "Primeira avaliação do dia", "/icons/primeiro-do-dia.png", CategoriaBadge.Especial),

            //CONQUISTAS ÉPICAS/LENDÁRIAS
            new Badge(Guid.NewGuid(), "Enciclopédia", "Assistiu todos os gêneros disponíveis", RaridadeBadge.Epica, 10, "Todos os gêneros", "/icons/enciclopedia.png", CategoriaBadge.Especial),
            new Badge(Guid.NewGuid(), "Imortal", "6 meses de atividade contínua", RaridadeBadge.Lendaria, 20, "6 meses ativos", "/icons/imortal.png", CategoriaBadge.Especial),
            new Badge(Guid.NewGuid(), "Lenda Viva", "Conquistou 80% das conquistas", RaridadeBadge.Lendaria, 25, "80% das conquistas", "/icons/lenda-viva.png", CategoriaBadge.Especial),
            new Badge(Guid.NewGuid(), "Alma dos Doramas", "1 ano completo na plataforma", RaridadeBadge.Lendaria, 30, "1 ano na plataforma", "/icons/alma-doramas.png", CategoriaBadge.Especial)
        };
    }
}