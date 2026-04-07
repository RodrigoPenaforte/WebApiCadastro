# WebApiCadastro

API REST em **ASP.NET Core** para cadastro e gestão de usuários, com autenticação por **JWT**, persistência em **SQL Server** via **Entity Framework Core**, mapeamento com **AutoMapper** e documentação interativa com **Scalar** (OpenAPI).

Projeto de estudo com separação em camadas (Controllers, Services, DTOs, Models, Data), pensado para demonstrar boas práticas de API, segurança de credenciais e rastreabilidade de alterações.

---

## O que o sistema faz

- **Cadastro e login** de usuários (registro público; demais operações exigem token).
- **CRUD de usuários** (listagem, busca por ID, atualização e exclusão) protegido por autorização.
- **Senhas** armazenadas com **hash HMAC-SHA512** e salt — nunca em texto plano.
- **Autenticação JWT** após login; o token é associado ao usuário para chamadas autenticadas.
- **Auditoria** de operações sensíveis (ex.: atualização e remoção), registrando ação, usuário e dados antes/depois quando aplicável.

---

## Stack tecnológico

| Área | Tecnologia |
|------|------------|
| Runtime | .NET 10 |
| API | ASP.NET Core Web API |
| ORM | Entity Framework Core 10 |
| Banco | Microsoft SQL Server |
| Auth | JWT Bearer |
| Mapeamento | AutoMapper |
| Documentação | OpenAPI + Scalar (ambiente Development) |

---

## Arquitetura do repositório

```
WebApiCadastro/
├── Controllers/      # Endpoints HTTP (Login, Usuários, Auditoria)
├── Services/         # Regras de negócio (usuário, senha/token, auditoria)
├── Dtos/             # Contratos de entrada/saída da API
├── Models/           # Entidades e modelos de resposta
├── Data/             # DbContext e acesso a dados
├── Mapping/          # Perfis AutoMapper
└── Migrations/       # Migrações EF Core
```

---

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download) (ou compatível com o `TargetFramework` do `.csproj`)
- SQL Server (LocalDB ou instância acessível)
- Ferramentas EF (já referenciadas no projeto) para aplicar migrações, se necessário

---

## Como executar localmente

1. **Clone o repositório**

   ```bash
   git clone <url-do-seu-repositorio>
   cd WebApiCadastro
   ```

2. **Configure a conexão e o segredo do JWT** em `appsettings.json` (ou use [User Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) / variáveis de ambiente em produção):

   - `ConnectionStrings:DefaultConnection` — aponte para o seu servidor e banco.
   - `AppSettings:Token` — chave simétrica forte e longa para assinatura do JWT (não commite segredos reais em repositórios públicos).

3. **Aplicar o banco de dados** (com o projeto na pasta correta):

   ```bash
   cd WebApiCadastro
   dotnet ef database update --project WebApiCadastro/WebApiCadastro.csproj
   ```

   Ajuste o caminho se a estrutura de pastas no seu clone for diferente.

4. **Executar a API**

   ```bash
   dotnet run --project WebApiCadastro/WebApiCadastro.csproj
   ```

5. **Documentação interativa (Development)**  
   Com `ASPNETCORE_ENVIRONMENT=Development`, a UI Scalar costuma estar em:

   - `http://localhost:5032/scalar`  
   (confira portas em `Properties/launchSettings.json`.)

---

## Endpoints (visão geral)

| Método | Rota | Autenticação | Descrição |
|--------|------|--------------|-----------|
| `POST` | `/api/Login/Register` | Não | Cadastro de usuário |
| `POST` | `/api/Login/Login` | Não | Login (retorna dados do usuário incluindo token JWT) |
| `GET` | `/api/Usuarios` | JWT | Lista usuários |
| `GET` | `/api/Usuarios/{id}` | JWT | Busca por ID |
| `PUT` | `/api/Usuarios` | JWT | Atualiza usuário |
| `DELETE` | `/api/Usuarios/DeletarUsuario/{id}` | JWT | Remove usuário |
| `GET` | `/api/Auditoria` | JWT | Lista registros de auditoria |

Para rotas protegidas, envie o header: `Authorization: Bearer <seu_token>`.

---

## Destaques para recrutadores

- API REST com **controllers finos** e lógica em **services** injetados (`Scoped`).
- **DTOs** separados das entidades, com **AutoMapper** para manter contratos estáveis.
- **EF Core** com migrações versionadas e `DbContext` explícito.
- **Segurança**: JWT, hash de senha com salt, endpoints sensíveis com `[Authorize]`.
- **Auditoria** para rastrear alterações relevantes no domínio de usuários.

---

## Observação de segurança

Evite versionar connection strings, senhas de banco e chaves JWT reais. Prefira **User Secrets**, **variáveis de ambiente** ou um vault em ambientes compartilhados e produção.

---

## Licença

Defina aqui a licença do projeto (por exemplo MIT), se desejar uso ou contribuição por terceiros.
