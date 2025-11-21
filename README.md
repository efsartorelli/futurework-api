FUTUREWORK API – FUTURO DO TRABALHO 💼🤖
==================================================

API RESTful em ASP.NET Core voltada para o tema "O Futuro do Trabalho", ajudando a gerenciar:

- Profissionais 👤
- Competências (Skills) 🧠
- Carreiras do Futuro 🚀
- Recomendações de habilidades para se preparar para o mercado de trabalho que está vindo

Toda a solução foi desenvolvida atendendo aos requisitos de:
✅ Boas práticas REST
✅ Versionamento de API
✅ Integração com banco relacional usando Entity Framework Core
✅ Documentação (README, Draw.io, Swagger e vídeo)


1. INTEGRANTES DO GRUPO 👥
--------------------------------------------------

- Nome 1 – RM 94524 - Eduardo de Oliveira Nistal
- Nome 2 – RM 94618 - Enzo Vazquez Sartorelli


2. TEMA E OBJETIVO DO PROJETO 🎯
--------------------------------------------------

Tema: O Futuro do Trabalho

A FutureWork API foi criada para simular uma plataforma que ajuda profissionais a se prepararem para o futuro do trabalho, permitindo:

- Cadastro de profissionais interessados em se atualizar
- Cadastro de competências (skills) técnicas e comportamentais ligadas ao futuro do trabalho
- Cadastro de carreiras do futuro (como Engenheiro de IA, Cientista de Dados, Arquiteto de Cloud etc.)
- Geração de recomendações de skills para cada profissional conforme a área de interesse

Com isso, o sistema apoia:

- Upskilling e reskilling 📚
- Planejamento de carreira 🚀
- Adaptação ao mercado de trabalho cada vez mais digital e automatizado 🤖


3. TECNOLOGIAS UTILIZADAS 🛠
--------------------------------------------------

- Linguagem: C#
- Framework: ASP.NET Core Web API (.NET 8)
- ORM: Entity Framework Core
- Banco de dados: SQLite (banco relacional, arquivo futurework.db)
- Documentação de API: Swagger (Swashbuckle)
- Versionamento de API: Microsoft.AspNetCore.Mvc.Versioning
- Diagrama de fluxo: Draw.io (arquivo docs/fluxo-futurework.drawio)


4. ARQUITETURA DO PROJETO 🧱
--------------------------------------------------

Estrutura de pastas principal:

- src/FutureWorkApi/
  - Program.cs → Configuração da API, EF Core, Swagger e versionamento
  - appsettings.json → Connection string do banco SQLite
  - Data/AppDbContext.cs → Contexto do Entity Framework Core
  - Models/ → Modelos de domínio
    - Professional.cs
    - Skill.cs
    - Career.cs
    - ProfessionalSkill.cs
  - Controllers/
    - v1/
      - ProfessionalsController.cs
      - SkillsController.cs
      - CareersController.cs
    - v2/
      - ProfessionalController.cs
- docs/fluxo-futurework.drawio → Fluxo da aplicação
- README.txt → Documentação geral do projeto


5. MODELOS (ENTIDADES) PRINCIPAIS 📚
--------------------------------------------------

PROFESSIONAL 👤
- Id (int)
- FullName (string)
- Email (string)
- FutureArea (string) → Área de interesse no futuro do trabalho (ex.: IA, Dados, Cloud)
- CreatedAt (DateTime)
- ProfessionalSkills (relação com Skill)

SKILL 🧠
- Id (int)
- Name (string)
- Description (string?)
- Category (string?) → Ex.: Técnica, Comportamental, Digital
- ProfessionalSkills (relação com Professional)

CAREER 🚀
- Id (int)
- Name (string)
- Description (string?)
- FutureDemandLevel (int) → Ex.: 1–5
- RequiredSkillsOverview (string?) → Texto com overview das skills necessárias

PROFESSIONALSKILL (tabela de relação) 🔗
- ProfessionalId (int)
- SkillId (int)
- ProficiencyLevel (int) → Escala 1–5
- CreatedAt (DateTime)


6. BOAS PRÁTICAS REST UTILIZADAS ✅
--------------------------------------------------

A API foi construída seguindo boas práticas REST:

- Uso correto dos verbos HTTP:
  - GET → Buscar informações
  - POST → Criar novos registros
  - PUT → Atualizar registros existentes
  - DELETE → Remover registros

- Uso adequado de status codes:
  - 200 OK → Sucesso em requisições GET
  - 201 Created → Sucesso na criação (POST)
  - 204 No Content → Sucesso na atualização/remoção sem retorno de corpo
  - 400 Bad Request → Erros na requisição (ex.: id diferente no body e na rota)
  - 404 Not Found → Registro não encontrado

Os controllers fazem uso de ActionResult<> e retornam objetos tipados com:
- Ok()
- CreatedAtAction()
- NoContent()
- BadRequest()
- NotFound()


7. VERSIONAMENTO DA API 🔢
--------------------------------------------------

Foi implementado versionamento de API via rotas:

- Versão 1: /api/v1/...
- Versão 2: /api/v2/...

Configuração via AddApiVersioning no Program.cs, com:

- Versão padrão 1.0
- Suporte a múltiplas versões
- Versionamento explícito na URL (api/v{version:apiVersion})

VERSÃO 1 (v1) – CRUDs principais:

- Controle de Professionals, Skills e Careers
- Endpoints REST convencionais (GET, POST, PUT, DELETE)

VERSÃO 2 (v2) – Recomendações focadas no Futuro do Trabalho:

- Endpoint: GET /api/v2/Professional/{id}/recommendations
- Retorna uma lista de skills recomendadas para o profissional de acordo com seu campo FutureArea.

Exemplo de resposta v2:

{
  "professionalId": 1,
  "fullName": "João Silva",
  "futureArea": "IA",
  "recommendedSkills": [
    "Fundamentos de IA",
    "Redes Neurais",
    "Prompt Engineering",
    "Ética em IA"
  ]
}

No README está explicitado que a v1 centraliza o CRUD e a v2 adiciona lógica de recomendação (Futuro do Trabalho).


8. INTEGRAÇÃO E PERSISTÊNCIA DE DADOS 💾
--------------------------------------------------

Banco de dados: SQLite
- Banco relacional, atendendo ao requisito de usar banco relacional com EF Core.
- Arquivo: futurework.db, criado automaticamente na primeira execução.

Entity Framework Core:
- Classe AppDbContext mapeando:
  - DbSet<Professional>
  - DbSet<Skill>
  - DbSet<Career>
  - DbSet<ProfessionalSkill>

- Mapeamento de relacionamento muitos-para-muitos entre Professional e Skill via ProfessionalSkill.

Criação automática do banco:
- No Program.cs, ao iniciar a aplicação é criado um escopo (CreateScope) e chamado db.Database.EnsureCreated().
- Isso garante a criação do banco e das tabelas caso não existam.

Benefícios:
- Atende ao requisito de integração com banco usando EF Core
- Facilita a execução do projeto em qualquer máquina (não precisa instalar SQL Server)


9. FUNCIONALIDADES E ENDPOINTS PRINCIPAIS 🌐
--------------------------------------------------

Todos os endpoints usam prefixo /api/v{version}/...

VERSÃO 1 – /api/v1:

PROFESSIONALS 👤

- GET /api/v1/Professionals
  → Lista todos os profissionais.

- GET /api/v1/Professionals/{id}
  → Retorna um profissional específico.

- POST /api/v1/Professionals
  → Cria um novo profissional.
  Exemplo de body (JSON):

  {
    "fullName": "João Silva",
    "email": "joao.silva@teste.com",
    "futureArea": "IA"
  }

- PUT /api/v1/Professionals/{id}
  → Atualiza um profissional existente.

- DELETE /api/v1/Professionals/{id}
  → Remove um profissional.


SKILLS 🧠

- GET /api/v1/Skills
- GET /api/v1/Skills/{id}
- POST /api/v1/Skills

  Body exemplo:

  {
    "name": "Machine Learning Básico",
    "description": "Fundamentos de modelos supervisionados e não supervisionados.",
    "category": "Técnica"
  }

- PUT /api/v1/Skills/{id}
- DELETE /api/v1/Skills/{id}


CAREERS 🚀

- GET /api/v1/Careers
- GET /api/v1/Careers/{id}
- POST /api/v1/Careers

  Body exemplo:

  {
    "name": "Engenheiro de IA",
    "description": "Profissional focado em soluções com Inteligência Artificial.",
    "futureDemandLevel": 5,
    "requiredSkillsOverview": "IA, Machine Learning, Programação, Ética em IA"
  }

- PUT /api/v1/Careers/{id}
  → Atualiza carreira.

- DELETE /api/v1/Careers/{id}
  → Remove carreira.


VERSÃO 2 – /api/v2:

PROFESSIONAL (RECOMENDAÇÕES) 🤖

- GET /api/v2/Professional/{id}/recommendations

Lê a área de interesse (FutureArea) do Professional e retorna uma lista de skills recomendadas.

Exemplos de comportamento:

- Se FutureArea contém "dados" → recomenda skills de Dados (SQL, BI, ML básico).
- Se contém "IA" → recomenda skills de Inteligência Artificial.
- Se contém "cloud" → recomenda skills de Cloud.
- Caso contrário → recomenda soft skills gerais (pensamento crítico, comunicação etc.).


10. COMO EXECUTAR A APLICAÇÃO ▶️
--------------------------------------------------

Pré-requisitos:

- .NET 8 SDK instalado

Passo a passo:

1) Clonar o repositório:
   git clone https://github.com/SEU-USUARIO/futurework-api.git
   cd futurework-api/src/FutureWorkApi

2) Restaurar os pacotes:
   dotnet restore

3) Executar a aplicação:
   dotnet run

4) Acessar a documentação Swagger pelo navegador:
   http://localhost:5000/swagger

Observações:

- O banco futurework.db será criado automaticamente na pasta do projeto.
- Todos os endpoints podem ser testados diretamente pelo Swagger (GET, POST, PUT, DELETE).


11. DOCUMENTAÇÃO DA API COM SWAGGER 📜
--------------------------------------------------

Swagger foi configurado no Program.cs com:

- AddEndpointsApiExplorer()
- AddSwaggerGen()
- app.UseSwagger()
- app.UseSwaggerUI()

A UI do Swagger permite:

- Visualizar todos os endpoints das versões v1 e v2
- Ver os parâmetros de rota e body
- Testar as requisições (Try it out)
- Ver os códigos de resposta e JSON retornado

Endpoint da documentação:
- http://localhost:5000/swagger


12. FLUXO DA APLICAÇÃO (DRAW.IO) 🔁
--------------------------------------------------

O fluxo de dados da aplicação foi documentado em:

- docs/fluxo-futurework.drawio

Fluxo resumido:

1. O CLIENTE (Postman, navegador, outra aplicação) faz uma requisição HTTP para a FutureWork API.
2. A requisição chega em um CONTROLLER (v1 ou v2), responsável por tratar a rota e aplicar a lógica de negócios básica.
3. O controller utiliza o AppDbContext (Entity Framework Core) para interagir com o banco de dados relacional (SQLite).
4. O EF Core executa operações de SELECT, INSERT, UPDATE, DELETE sobre o arquivo futurework.db.
5. A resposta retorna ao cliente em formato JSON, com o status code adequado (200, 201, 204, 400, 404, etc.).

Esse fluxo representa claramente a integração entre:

- Camada de API (Controllers)
- Camada de Persistência (EF Core + SQLite)
- Cliente externo que consome a API


13. LINK DO VÍDEO DE DEMONSTRAÇÃO 🎥
--------------------------------------------------

Vídeo (até 5 minutos) demonstrando o funcionamento integrado da solução:

- Link: https://youtube.com/SEU_VIDEO_AQUI

Conteúdo sugerido do vídeo:

1. Apresentação rápida do tema “Futuro do Trabalho” e da FutureWork API
2. Execução da API (dotnet run)
3. Acesso ao Swagger (/swagger)
4. Demonstração dos endpoints de:
   - Professionals (POST, GET, PUT, DELETE)
   - Skills
   - Careers
5. Demonstração do endpoint de v2 com recomendações de skills
6. Explicação rápida do fluxo de dados (mostrar o Draw.io ou explicar na tela)


14. PUBLICAÇÃO EM CLOUD (OPCIONAL) ☁️
--------------------------------------------------

Caso a API seja publicada em um ambiente de cloud (item opcional do trabalho), podem ser adicionadas informações como:

- Plataforma: ex. Azure App Service, Render, Railway etc.
- URL pública da API:
  - Ex.: https://futurework-api.azurewebsites.net


15. RESUMO DO ATENDIMENTO AOS REQUISITOS DO PROFESSOR ✅
--------------------------------------------------

1) BOAS PRÁTICAS – API RESTful (30 pts)
   - Verbos HTTP corretos (GET, POST, PUT, DELETE)
   - Status codes adequados (200, 201, 204, 400, 404)
   - Controllers retornando ActionResult com responses padronizadas

2) VERSIONAMENTO DA API (10 pts)
   - Estrutura de rotas com /api/v1/... e /api/v2/...
   - Configuração com AddApiVersioning
   - Explicação detalhada do versionamento neste README

3) INTEGRAÇÃO E PERSISTÊNCIA (30 pts)
   - Uso de Entity Framework Core
   - Banco de dados relacional: SQLite
   - Criação automática das tabelas via EnsureCreated()
   - Modelagem das entidades e relacionamentos (incluindo muitos-para-muitos)

4) DOCUMENTAÇÃO (30 pts)
   - README completo (este arquivo) 🌟
   - Fluxo da aplicação em Draw.io (docs/fluxo-futurework.drawio)
   - Documentação da API com Swagger (/swagger)
   - Vídeo demonstrando funcionamento integrado (link no README)


FIM ✅
==================================================
