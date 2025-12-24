# 📺 KDramaSystem

**KDramaSystem** é uma plataforma completa para amantes de doramas. Com funcionalidades inspiradas no Skoob e redes sociais modernas, o sistema permite organizar, acompanhar, avaliar e descobrir doramas de forma personalizada e social.

> Feito com arquitetura limpa, DDD, boas práticas de código e foco em escalabilidade > esse projeto é uma vitrine aplicada ao entretenimento.

---

## 🧠 Propósito

Facilitar a vida de quem assiste doramas:

- 📌 **Organizar** doramas por status e progresso  
- ⭐ **Avaliar** obras com nota, comentário e recomendação  
- 🔎 **Descobrir** doramas, atores e gêneros de forma simples  
- 👥 **Interagir** com uma comunidade apaixonada  

---

## 🎯 Funcionalidades

### 🎬 Organização & Acompanhamento

- Marcar doramas como: *Quero Ver*, *Assistindo*, *Pausado*, *Concluído*, *Abandonado*, *Reassistindo*  
- Acompanhar o progresso por **temporada e episódio**  
- Avaliar com **nota, comentário e recomendação (usuário ou nome livre)**  

### 📚 Listas e Prateleiras

- Criar listas personalizadas (nome, descrição, imagem de capa)  ex: Meus Doramas de Romance fav <3
- Controle de privacidade: público, privado ou com link para compartilhar

### 🎭 Playlists Vínculadas

- Gostou das músicas tocadas do dorama ou OSTs e abertura?
- Todo dorama vai ter anexado playlist completa ou OSTs de todas as plataformas!

### 🔍 Exploração

- Filtros por título, ator, gênero, país ou ano  
- Páginas completas de doramas, atores e gêneros  
- Visualização de temporadas, avaliações e comentários  

### 🧠 Interação Social

- Seguir e ser seguido  
- Feed de atividades (avaliações, comentários, listas, etc.)  
- Comentários em avaliações e temporadas  

### 🧩 **Conteúdo Interativo e Criado por Usuários**

 Jogos e Desafios

- Criar **quizzes personalizados**:
    - Perguntas, respostas, imagens, título, descrição
    - Compartilhamento de resultados
    - Curtidas, comentários
- Jogos de memória e desafios rápidos (ex: “complete o nome”, “associe a OST”)
- Desafios e maratonas:
    - Ex: “Assista 5 doramas históricos em 1 mês”
    - Com progresso e compartilhamento de conquistas 


### 🗞️ Notícias, Bastidores e Fofocas

- Página de **notícias sobre doramas, atores, bastidores**
- Área para **discussões e comentários**

---

## 🧩 Bounded Contexts

| Contexto | Responsabilidade |
|----------|------------------|
| **Usuários** | Autenticação, perfil, seguidores, feed |
| **DoramaCatalogo** | Obras, temporadas, episódios, elenco |
| **Progresso** | Status de visualização e andamento |
| **Avaliações** | Notas e recomendações |
| **Listas** | Organização personalizada |
| **Comentários** | Interações em avaliações ou temporadas |
| **Social** | Feed, curtidas, recomendações sociais |
| **Playlist** | Playlist, OST e Músicas dos KDramas |

---

## 🛠️ Tecnologias Utilizadas e Padrões

- **Backend**: .NET 8, Clean Architecture, DDD / PostgreSQL  
- **Frontend**: React + TypeScript, Tailwind CSS  
- **Autenticação**: JWT, Login com Google  
- **Padrões**: CQRS, SOLID, Middlewares globais, Exceptions centralizadas  

---

## 🧪 Testes

- Testes unitários para entidades e use cases  
- Validações de domínio rigorosas  
- Testes de integração com banco de dados e mocks

---


## 📸 Telas

> Protótipos e screenshots em desenvolvimento...

---

## 💡 Contribuindo

Contribuições são bem-vindas! 💜

---

## 📄 Licença

Distribuído sob a licença MIT. Veja `LICENSE.md` para mais detalhes.
