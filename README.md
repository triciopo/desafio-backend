# Teste Técnico .NET


**Objetivo:** Criar uma API REST que consulta CEPs na API da @ViaCep e armazena os resultados em um banco de dados.

## Rodando a API
Clone o projeto:
```bash
$ git clone https://github.com/triciopo/desafio-backend.git
```
> [!IMPORTANT]
> Configure a string de conexão com o banco de dados em [`appsettings.json`](https://github.com/triciopo/desafio-backend/blob/master/API/appsettings.json):
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=test;User Id=user;Password=pass"
}
```

Execute a API:
```bash
$ cd desafio-backend/API
$ dotnet run
```

> [!TIP]
> Abra o navegador em localhost:61997/swagger **ou** utilize o [Postman](https://github.com/triciopo/desafio-backend/blob/master/API/Teste_Negocie_Online_-_Backend.postman_collection.json)
## **Endpoints:**

-   **POST /api/cep:**
    
    -   **Entrada:**  CEP (8 dígitos, JSON).
    -   **Validação:**  Formato do CEP.
    -   **Ações:**
        -   Consulta do cep na API (ViaCep).
        -   Armazenamento do resultado no banco de dados.
    -   **Retorno:**
        -   Status da operação.
        -   CEP criado (201 Created) ou mensagem de erro (400 Bad Request, 409 Conflict ou 500 Internal Server Error).
-   **GET /api/cep/{cep}:**
    
    -   **Entrada:**  CEP (8 dígitos).
    -   **Validação:**  Formato do CEP.
    -   **Ações:**  Busca do endereço no banco de dados.
    -   **Retorno:**
        -   Status da operação.
        -   CEP encontrado (200 OK) ou mensagem de erro (400 Bad Request, 404 Not Found ou 500 Internal Server Error).

## **Tecnologias:**

-   .NET 8
-   Dapper
-   PostgreSQL

## **Estrutura do Projeto:**

-   Apresentação (API)
    -   Controladores
-   Aplicação
    -   DTOs
    -   Responses
    -   Serviços
-   Dominio
    -   Models
-   Infraestrutura
    -   Contexto do Banco de Dados
    -   Repositórios


