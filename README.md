# Desafio-Stefanini
Este projeto em .Net e Angular, é uma aplicação simples que consite em realizar um CRUD para cadastro de pedidos e produtos. Onde a maneira de armazenamento foi InMemory. _Leia mais em: **https://learn.microsoft.com/en-us/ef/core/providers/in-memory/?tabs=dotnet-core-cli**_

## Requisitos

Para executar este projeto, você precisará do seguinte software instalado em seu sistema:

- Visual Studio 2022
- VsCode
- Node.js
- SDK do dotNet

## Como usar o Bakcend

Siga estas etapas para executar o projeto em seu ambiente local:

1. Clone o repositório (caso você não tenha feito isso anteriormente):
      https://github.com/GustavoSousa4/Desafio-Stefanini.git)
   
3. Abra o projeto no Visual Studio 2022.

4. Defina na pasta (1 - APIs/ApiPedidos) como projeto de inicialização.

5. Pressione F5 na IDE para compilar e executar o projeto, que estará ativo em https://localhost:7190/ ou poderá ser executado em http://localhost:5274.

6. Será aberto em seu navegador o swagger, onde poderão ser testados os endpoints desenvolvidos:

### Pedido
- **[GET] /Pedidos/GetAll**: Retorna todos os pedidos contidos no banco.
- **[GET] /Pedidos/id**: Retorna o pedido a partir do id escrito.
- **[POST] /Pedidos**: Permite a criação de um novo pedido.
- **[PUT] /Pedidos/id**: Permite alterar os dados do pedido selecionado pelo id.
- **[DELTE] /Pedidos/id**: Permite excluir um pedido a partir do id.
### Produto
- **[GET] /Produto/GetAll**: Retorna todos os produtos contidos no banco.
- **[GET] /Produto/GetById/id**: Retorna o produto a partir do id escrito.
- **[GET] /Produto/GetByName/nomeProduto**: Retorna o produto a partir do nome escrito.
- **[GET] /Produto/GetByPrice/id**: Retorna o valor do produto a partir do id escrito.
- **[POST] /Produto**: Permite a criação de um novo produto.
- **[PUT] /Produto/id**: Permite alterar os dados do produto selecionado pelo id.
- **[DELTE] /Produto/id**: Permite excluir um produto a partir do id.


## Estrutura do Projeto
Foi desenvolvido utilizando alguns padrões de SOLID, DDD e Clean Architecture

- `1 - APIs`: Contém a parte de configurações do projeto, o banco de dados "conectado" e as controllers.
- `2 - Application`: Contém a modelagem dos objetos de request e response, e as services das respectivas entidades.
- `3 - Domain`: Contém as entidades e as interfaces dos repostitórios.
- `4 - Infrastructure`: Contém a injeção de dependência do projeto, o DbContext, os mappings das entidades, as migrations e a classe concreta dos repositórios. 

## Como usar o Ffrontend

1. Abra a pasta fontend na IDE (VsCode).

2. No terminal da IDE, vá até a pasta src/app.

3. Execute o comando 'npm i' para instalar os pacotes nececssários do projeto.

4. Execute o comando 'ng serve' ou pressione a tecla [F5].

5. O projeto será executado na rota localhost:4200 e abrirá uma página no seu navegador.

6. No início da página será possível ir para pedidos ou produto, para realizar as devidas funcionalidades (criar, excluir e alterar).

## Contato
- Linkedin: linkedin.com/in/gustavoefsousa/
- Email: gustavosousa.adm@hotmail.com
