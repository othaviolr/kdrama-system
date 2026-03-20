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

> Home
<img width="1905" height="911" alt="1" src="https://github.com/user-attachments/assets/e4e24edc-fe21-4dfb-bdfb-c99c726c3bba" />
<img width="1905" height="911" alt="2" src="https://github.com/user-attachments/assets/27c5db15-2191-419b-9135-c3ffe81fbd95" />
<img width="1905" height="911" alt="3" src="https://github.com/user-attachments/assets/71b00824-9b27-47ac-9178-02dae70f9e44" />
<img width="1905" height="911" alt="4" src="https://github.com/user-attachments/assets/c9e73ee4-62f0-47b6-8d64-38ca0f1475bb" />
<img width="1905" height="911" alt="5" src="https://github.com/user-attachments/assets/f5a0a599-5044-4660-9db7-e87afcdb566c" />
<img width="1905" height="911" alt="6" src="https://github.com/user-attachments/assets/a929843d-b196-4113-bf05-db2564c54b0c" />

> Catalogo
<img width="1905" height="911" alt="1" src="https://github.com/user-attachments/assets/689a371b-d0fe-4bf1-89c8-58139a354511" />
![2](https://github.com/user-attachments/assets/ae723728-19a1-41b2-a477-bc1b2ec6a6f5)

> Review
<img width="1905" height="912" alt="rALQnW3DHg" src="https://github.com/user-attachments/assets/618a2b37-45b7-4484-86d4-3578b9626c11" />


---

## 💡 Contribuindo

Contribuições são bem-vindas! 💜

---

## 📄 Licença

Distribuído sob a licença MIT. Veja `LICENSE.md` para mais detalhes.
