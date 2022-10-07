<h1> ⚠️ Status: :construction: </h1>

<h1 align="center">CONTROLE DE ESTACIONAMENTO </h1>

## Tecnologias utilizadas:

- ASP.NET
- .NET CORE
- C#
- DAPPER
- SQL SERVER

## Projeto:

Esse projeto consiste na criação de uma api voltada para o controle de um estacionamento 

## Nugets

- DAPPER
- SERILOG
- SQLCLIENT
- SWAGGER

## Instalação

Para utilizar este projeto de forma local, é necessário fazer um
`git clone` em sua máquina. Lembre-se de conferir de você possui os Nugets necessários.

Para clonar o repositório, digite no terminal da sua máquina:

```
git clone https://github.com/notgonnaleo/VeiculosBackEnd.git
```

Acesse a pasta:
```
cd \source\repos\VeiculosBackEnd>
```

## 💾 Schema do MySQL SERVER
```
Colocar o Schema Aqui!
```

# Rotas

### Veículos

- **GET /Veiculo/getVeiculos**

Confira os Veículos registrados no banco de dados

Esquema da requisição:

>https://localhost:44312/swagger/index.html

Esquema da resposta:

```json
[
  {
    "id_veiculo": 1,
    "data_cadastro": "2022-10-05T00:22:55.997",
    "id_placa": 1,
    "id_cor": 2,
    "km": 2000,
    "id_modelo": 2
  },
  {
    "id_veiculo": 2,
    "data_cadastro": "2022-10-07T20:30:49.197",
    "id_placa": 3,
    "id_cor": 5,
    "km": 0,
    "id_modelo": 2
  }
]
```
---


