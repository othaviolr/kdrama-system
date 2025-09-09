using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;

public static class BadgeCatalogo
{
    public static List<Badge> ObterTodas()
    {
        return new List<Badge>
        {
            //CONQUISTAS DE PROGRESSÃO
            new Badge(Guid.Parse("09415C73-7D78-4587-BF5D-7ED619B888D6"), "Primeiro Passo", "Assistiu seu primeiro dorama", RaridadeBadge.Comum, 1, "Assistir 1 dorama", "/icons/primeiro-passo.png", CategoriaBadge.Progressao),
            new Badge(Guid.Parse("AD83A4DD-260F-4CCF-A4B7-CD781EA309F7"), "Novato", "Assistiu 5 doramas", RaridadeBadge.Comum, 2, "Assistir 5 doramas", "/icons/novato.png", CategoriaBadge.Progressao),
            new Badge(Guid.Parse("8430A976-422E-4ADC-9032-F34020208FCF"), "Entusiasta", "Assistiu 15 doramas", RaridadeBadge.Comum, 3, "Assistir 15 doramas", "/icons/entusiasta.png", CategoriaBadge.Progressao),
            new Badge(Guid.Parse("2C62BE7C-448B-4AB8-ACAB-6E4E9067B040"), "Aficionado", "Assistiu 30 doramas", RaridadeBadge.Comum, 5, "Assistir 30 doramas", "/icons/aficionado.png", CategoriaBadge.Progressao),
            new Badge(Guid.Parse("0CE53181-A9E3-4BFA-8240-A0AA17FA7966"), "Profissional", "Assistiu 50 doramas", RaridadeBadge.Epica, 8, "Assistir 50 doramas", "/icons/expert.png", CategoriaBadge.Progressao),
            new Badge(Guid.Parse("044C3ADC-CAC1-4EDE-81AA-363AF701E95A"), "Expert", "Assistiu 100 doramas", RaridadeBadge.Epica, 15, "Assistir 100 doramas", "/icons/mestre.png", CategoriaBadge.Progressao),
            new Badge(Guid.Parse("B21FA14A-B2E8-42AF-9416-55591D9BF0B0"), "Mestre", "Assistiu 200 doramas", RaridadeBadge.Lendaria, 30, "Assistir 200 doramas", "/icons/mestre.png", CategoriaBadge.Progressao),

            //CONQUISTAS DE EPISÓDIOS
            new Badge(Guid.Parse("AA70AD2F-82F3-4068-8BBE-460A7C2D5965"), "Maratonista", "Assistiu 50 episódios", RaridadeBadge.Comum, 2, "Assistir 50 episódios", "/icons/maratonista.png", CategoriaBadge.Episodios),
            new Badge(Guid.Parse("8B6763C5-07D1-42AF-BEDA-718E3D0B30D4"), "Viciado", "Assistiu 100 episódios", RaridadeBadge.Rara, 4, "Assistir 100 episódios", "/icons/viciado.png", CategoriaBadge.Episodios),
            new Badge(Guid.Parse("E460617F-D0F2-448D-971D-30823F766384"), "Insaciável", "Assistiu 300 episódios", RaridadeBadge.Epica, 8, "Assistir 300 episódios", "/icons/insaciavel.png", CategoriaBadge.Episodios),
            new Badge(Guid.Parse("5D0B7443-0BD4-4550-8862-2393B27EB577"), "Lendário", "Assistiu 500 episódios", RaridadeBadge.Lendaria, 15, "Assistir 500 episódios", "/icons/lendario.png", CategoriaBadge.Episodios),

            // CONQUISTAS DE GÊNEROS
            new Badge(Guid.Parse("D10E034E-9D5A-4A36-B633-57F03B9C77B2"), "Romântico", "Assistiu 10 doramas românticos", RaridadeBadge.Comum, 3, "Assistir 10 doramas românticos", "/icons/romantico.png", CategoriaBadge.Genero),
            new Badge(Guid.Parse("EFBD76A4-D60F-44D8-A70E-A4B032B05D68"), "Detetive", "Assistiu 5 doramas de mistério/crime", RaridadeBadge.Comum, 3, "Assistir 5 doramas de mistério", "/icons/detetive.png", CategoriaBadge.Genero),
            new Badge(Guid.Parse("747A84D0-D480-4048-A05A-3053A96FC40B"), "Comédia", "Assistiu 8 doramas de comédia", RaridadeBadge.Comum, 2, "Assistir 8 doramas de comédia", "/icons/comedia.png", CategoriaBadge.Genero),
            new Badge(Guid.Parse("6ADA29A9-AE8C-400B-9315-532DF871B7BE"), "Melodrama", "Assistiu 6 melodramas", RaridadeBadge.Comum, 2, "Assistir 6 melodramas", "/icons/melodrama.png", CategoriaBadge.Genero),
            new Badge(Guid.Parse("3EA7A757-8BBD-4AD6-BCC1-FD0580D30CEF"), "Ação", "Assistiu 5 doramas de ação", RaridadeBadge.Rara, 4, "Assistir 5 doramas de ação", "/icons/acao.png", CategoriaBadge.Genero),
            new Badge(Guid.Parse("8E59EF2F-D04F-411A-BAC6-FF9286090AA2"), "Histórico", "Assistiu 5 doramas históricos", RaridadeBadge.Rara, 4, "Assistir 5 doramas históricos", "/icons/historico.png", CategoriaBadge.Genero),

            //CONQUISTAS DE PAÍSES
            new Badge(Guid.Parse("32227200-8605-4143-8BC1-FD040E82EFA9"), "K-Drama Lover", "Assistiu 20 K-dramas", RaridadeBadge.Comum, 4, "Assistir 20 K-dramas", "/icons/k-drama-lover.png", CategoriaBadge.Pais),
            new Badge(Guid.Parse("A34B0CB1-41AC-4138-B6B8-72F41124BADF"), "J-Drama Explorer", "Assistiu 8 J-dramas", RaridadeBadge.Rara, 5, "Assistir 8 J-dramas", "/icons/j-drama-explorer.png", CategoriaBadge.Pais),
            new Badge(Guid.Parse("367F1DFA-3E9B-4383-984B-9DED5D32F56B"), "C-Drama Fan", "Assistiu 10 C-dramas", RaridadeBadge.Rara, 5, "Assistir 10 C-dramas", "/icons/c-drama-fan.png", CategoriaBadge.Pais),
            new Badge(Guid.Parse("E02C9C68-F147-4BC5-99C8-AB5DD9625A8B"), "Thai Drama", "Assistiu 5 dramas tailandeses", RaridadeBadge.Rara, 4, "Assistir 5 dramas tailandeses", "/icons/thai-drama.png", CategoriaBadge.Pais),
            new Badge(Guid.Parse("450A4268-0E3B-4FBB-9019-6A2F94F4BA71"), "Explorador Global", "Assistiu doramas de 4 países diferentes", RaridadeBadge.Epica, 8, "Assistir doramas de 4 países", "/icons/explorador-global.png", CategoriaBadge.Pais),

            //CONQUISTAS DE AVALIAÇÃO
            new Badge(Guid.Parse("BDE5A26B-D375-4493-8DAB-6EADBFA8AC6B"), "Crítico", "Avaliou 20 doramas", RaridadeBadge.Comum, 3, "Avaliar 20 doramas", "/icons/critico.png", CategoriaBadge.Avaliacao),
            new Badge(Guid.Parse("5D0B880D-C214-439B-ABEC-6896D19069ED"), "Fã Incondicional", "Deu nota 5 para 10 doramas", RaridadeBadge.Rara, 4, "Dar nota 5 para 10 doramas", "/icons/fa-incondicional.png", CategoriaBadge.Avaliacao),
            new Badge(Guid.Parse("0736818E-5F1E-4D12-9374-175A50CC8A37"), "Exigente", "Mantém média de avaliação abaixo de 4", RaridadeBadge.Rara, 3, "Média 4 em 15+ doramas", "/icons/exigente.png", CategoriaBadge.Avaliacao),
            new Badge(Guid.Parse("E9E81FBC-07FA-47B0-99DB-EF54D6738D2F"), "Equilibrado", "Mantém média entre 4-5", RaridadeBadge.Comum, 2, "Média 4-5 em 15+ doramas", "/icons/equilibrado.png", CategoriaBadge.Avaliacao),

            //CONQUISTAS DE FINALIZAÇÃO
            new Badge(Guid.Parse("D9D26232-9CCB-478C-9D4C-6DAF9C986BAC"), "Finalizador", "Completou 15 doramas", RaridadeBadge.Comum, 3, "Completar 15 doramas", "/icons/finalizador.png", CategoriaBadge.Finalizacao),
            new Badge(Guid.Parse("22CE5D56-03D3-41AE-B568-3F8420FFED02"), "Completista", "Completou 40 doramas", RaridadeBadge.Rara, 6, "Completar 40 doramas", "/icons/completista.png", CategoriaBadge.Finalizacao),
            new Badge(Guid.Parse("2148F96E-3F8D-4C18-86D9-F6042EEEAA24"), "Perfeccionista", "Completou 70 doramas", RaridadeBadge.Epica, 10, "Completar 70 doramas", "/icons/perfeccionista.png", CategoriaBadge.Finalizacao),

            //CONQUISTAS DE DESCOBERTA
            new Badge(Guid.Parse("1F063D83-1F08-4954-BBC2-4699D92CAE26"), "Garimpeiro", "Descobriu 5 doramas com nota 2", RaridadeBadge.Rara, 4, "5 doramas com nota baixa", "/icons/garimpeiro.png", CategoriaBadge.Descoberta),
            new Badge(Guid.Parse("B1B24FB8-0352-4CE8-9513-EF343FD21865"), "Vintage", "Assistiu 8 doramas de antes de 2015", RaridadeBadge.Rara, 5, "8 doramas pré-2015", "/icons/vintage.png", CategoriaBadge.Descoberta),
            new Badge(Guid.Parse("62BCC78E-ADAC-4527-82AA-687A15494EC2"), "Atualizado", "Assistiu 15 doramas de 2023-2024", RaridadeBadge.Comum, 3, "15 doramas recentes", "/icons/atualizado.png", CategoriaBadge.Descoberta),

            //CONQUISTAS DE ATORES
            new Badge(Guid.Parse("C931A111-4256-42E2-AC3F-CA78E1BA72DF"), "Stalker de Ator", "Assistiu 5 doramas do mesmo ator", RaridadeBadge.Comum, 3, "5 doramas do mesmo ator", "/icons/stalker-ator.png", CategoriaBadge.Ator),
            new Badge(Guid.Parse("90FCEE83-4A4D-427C-89C5-E145AB4F4367"), "Fã Clube", "Assistiu 8 doramas do mesmo ator", RaridadeBadge.Rara, 5, "8 doramas do mesmo ator", "/icons/fa-clube.png", CategoriaBadge.Ator),

            //CONQUISTAS DE LISTA/ORGANIZAÇÃO
            new Badge(Guid.Parse("0DD798E8-8779-4100-AEEC-1DBC87EA152C"), "Organizador", "Criou 3 listas personalizadas", RaridadeBadge.Comum, 2, "Criar 3 listas", "/icons/organizador.png", CategoriaBadge.Lista),
            new Badge(Guid.Parse("19D08651-BDF9-4826-B271-5B54E26B5FAB"), "Curador", "Adicionou 30 doramas em listas", RaridadeBadge.Rara, 4, "30 doramas em listas", "/icons/curador.png", CategoriaBadge.Lista),

            //CONQUISTAS DE REWATCH
            new Badge(Guid.Parse("DAD701B3-266D-4DC3-9ED0-2F7D9CE116A5"), "Nostálgico", "Reassistiu 3 doramas", RaridadeBadge.Rara, 4, "Reassistir 3 doramas", "/icons/nostalgico.png", CategoriaBadge.Rewatch),

            //CONQUISTAS ESPECIAIS
            new Badge(Guid.Parse("35605020-D422-4D07-BFFF-B4CFF63A6E2B"), "Primeiro do Dia", "Primeiro a avaliar dorama lançado", RaridadeBadge.Epica, 8, "Primeira avaliação do dia", "/icons/primeiro-do-dia.png", CategoriaBadge.Especial),

            //CONQUISTAS ÉPICAS/LENDÁRIAS
            new Badge(Guid.Parse("EA2CC009-A6FD-4444-9A0A-0D6D8DE191D0"), "Enciclopédia", "Assistiu todos os gêneros disponíveis", RaridadeBadge.Epica, 10, "Todos os gêneros", "/icons/enciclopedia.png", CategoriaBadge.Especial),
            new Badge(Guid.Parse("D193F091-C28E-4B4B-A7B2-00C3A754E9B1"), "Imortal", "6 meses de atividade contínua", RaridadeBadge.Lendaria, 20, "6 meses ativos", "/icons/imortal.png", CategoriaBadge.Especial),
            new Badge(Guid.Parse("156B0B31-9D2F-4F11-90B6-287DF1B43EBB"), "Lenda Viva", "Conquistou 80% das conquistas", RaridadeBadge.Lendaria, 25, "80% das conquistas", "/icons/lenda-viva.png", CategoriaBadge.Especial),
            new Badge(Guid.Parse("9BBA505F-8D65-46FC-AA3F-CF6A89C397E5"), "Alma dos Doramas", "1 ano completo na plataforma", RaridadeBadge.Lendaria, 30, "1 ano na plataforma", "/icons/alma-doramas.png", CategoriaBadge.Especial)
        };
    }
}