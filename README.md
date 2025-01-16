# API de Caixa Registradora

Esta é uma API desenvolvida em **C# .NET 8** que simula o funcionamento de uma caixa registradora. A aplicação permite realizar transações, adicionar dinheiro no caixa e verificar o conteúdo do caixa.

## 📝 Descrição

A API é projetada para gerenciar uma caixa registradora, permitindo as seguintes funcionalidades:
- Verificar a disponibilidade da API.
- Consultar as notas e moedas disponíveis no caixa.
- Adicionar dinheiro ao caixa.
- Realizar transações e calcular troco.

## 📚 Documentação da API

### Endpoints Principais

#### **Verificar Disponibilidade da API**
- **Método:** `GET`
- **URL:** `/hccaixa`
- **Descrição:** Retorna a mensagem "Tudo certo!" junto com a hora de ativação da API.
- **Códigos de Resposta:**
  - `200 OK` - API disponível.
  - `500 Server Error` - Erro interno.

---

#### **Verificar Notas no Caixa**
- **Método:** `GET`
- **URL:** `/vercaixa`
- **Descrição:** Exibe a quantidade de cada nota disponível na caixa registradora.
- **Códigos de Resposta:**
  - `200 OK` - Notas listadas com sucesso.
  - `204 No Content` - Caixa vazio.

---

#### **Adicionar Dinheiro no Caixa**
- **Método:** `POST`
- **URL:** `/add`
- **Descrição:** Adiciona notas ou moedas no caixa.
- **Body da Requisição:**
  ```json
  {
    "valor": 1.0,
    "qtd": 2
  }
  ```
- **Códigos de Resposta:**
  - `201 Created` - Dinheiro adicionado com sucesso.
  - `400 Bad Request` - Dados inválidos enviados.

---

#### **Realizar Transação**
- **Método:** `POST`
- **URL:** `/transacao`
- **Descrição:** Realiza uma transação e devolve o troco.
- **Body da Requisição:**
  ```json
  {
    "valor": 10.0,
    "notasRecebidas": [
      {
        "valor": 20.0,
        "qtd": 1
      }
    ]
  }
  ```
- **Códigos de Resposta:**
  - `201 Created` - Transação realizada com sucesso.
  - `400 Bad Request` - Dados inválidos enviados.
  - `406 Not Acceptable` - Valor recebido insuficiente.
  - `500 Server Error` - Troco indisponível.

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C#
- **Framework:** .NET 8
- **Formato de Documentação:** OpenAPI 3.0.1
- **Gerenciador de Dependências:** NuGet

## 🚀 Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/raonhn/caixa-registradora.git
   ```
2. Navegue para o diretório do projeto:
   ```bash
   cd caixa-registradora
   ```
3. Restaure as dependências:
   ```bash
   dotnet restore
   ```
4. Execute o projeto:
   ```bash
   dotnet run --project src/CaixaRegistradora
   ```
5. Acesse a API em `http://localhost:5031`.

## 🧪 Testes

Execute os testes com o seguinte comando:
```bash
dotnet test
```

## 📬 Contato

Desenvolvido por **Nathan Oliveira**  
Email: [nathanraoliveira@gmail.com](mailto:nathanraoliveira@gmail.com)

## 📄 Licença

Este projeto foi desenvolvido por **Nathan Oliveira** e está sob a licença **All Rights Reserved**. Todos os direitos são reservados.  
Para obter permissão de uso, entre em contato: [nathanraoliveira@gmail.com](mailto:nathanraoliveira@gmail.com).

