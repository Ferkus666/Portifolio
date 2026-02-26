--------||KITCHEN ROUTING SYSTEM (THE OPERATOR V.1)||--------

-DESCRIÇÃO

Este projeto implementa um sistema de roteamento de pedidos de restaurante.
Pedidos enviados por múltiplos terminais POS (Point of Sale) são recebidos via HTTP e roteados para filas específicas da cozinha, de acordo com a área responsável pelo preparo de cada item.

O sistema foi desenvolvido em ASP.NET Core Web API (.NET 6), utilizando armazenamento em memória, com foco em clareza de código, concorrência segura e separação de responsabilidades.


→ARQUITETURA E ORGANIZAÇÃO←

Estrutura principal do projeto:

KitchenRoutingSystem

Controllers -> Endpoints HTTP
Contracts -> DTOs (entrada e saída da API)
Domain -> Modelos de domínio (imutáveis)
Services -> Lógica de aplicação (roteamento)
Infrastructure -> Armazenamento em memória (filas)
Program.cs -> Configuração da aplicação

||Principais decisões de arquitetura||

DTOs são utilizados para desacoplar o contrato da API do domínio.
O domínio é imutável, garantindo previsibilidade e segurança.
O estado em memória é tratado com estruturas thread-safe.
O controller apenas coordena o fluxo HTTP, sem conter regras de negócio.


→ÁREAS DA COZINHA←

As áreas da cozinha são representadas por um enum: (0 -4)

Fries
Grill
Salad
Drink
Dessert


→CONCORRÊNCIA E ARMAZENAMENTO←

As filas da cozinha são mantidas em memória.
Foi utilizado ConcurrentDictionary para mapear áreas da cozinha.
Cada área possui uma ConcurrentQueue para garantir segurança em cenários concorrentes.
O armazenamento é registrado como Singleton, permitindo que todas as requisições compartilhem o mesmo estado.


→→TESTES SWAGGER←←

ENDPOINTS DISPONÍVEIS

_______________________
Criar Pedido
Endpoint: POST /orders

Area :
0 = Fries
1 = Grill
2 = Salad
3 = Drink
4 = Dessert

name: Nome do item do pedido 
ex : Burger, Fries, Soda, etc.

Exemplo de requisição:

{
"items": [
{ "name": "Burger", "area": 1 },
{ "name": "Fries", "area": 0 }
]
}

Exemplo de resposta:

{
"orderId": "guid",
"itemsRouted": 2
}

A API retorna o status 202 (Accepted), indicando que o pedido foi recebido e roteado para as filas corretas.
_______________________

_______________________
Consultar filas existentes da cozinha 
Endpoint: GET /orders/queues

Exemplo de resposta:

{
"Grill": 1,
"Fries": 1
}

Este endpoint é utilizado para debug, validação do roteamento e testes de integração.
______________________


→→VALIDAÇÃO←←

A API utiliza Data Annotations para validação automática dos dados.
Requisições inválidas retornam 400 (Bad Request) com detalhes do erro(mensagem).
O binding dos dados é feito diretamente a partir do corpo da requisição.

--TESTES--

O projeto possui um projeto de testes separado, contendo:
Testes unitários para validar a lógica de roteamento sem dependência de HTTP.
Testes de integração para validar os endpoints da API utilizando WebApplicationFactory.
Os testes garantem que:
Os itens são roteados corretamente para as filas da cozinha.
O endpoint de criação de pedidos responde corretamente.
As validações da API funcionam conforme esperado.

→→COMO EXECUTAR O PROJETO←←

Executar a aplicação:

dotnet run

A API ficará disponível no navegador através do Swagger.

→→Executar os testes:

dotnet test

----||CONSIDERAÇÕES FINAIS||----

Este projeto foi desenvolvido com foco em:
Legibilidade de código
Separação de responsabilidades
Concorrência segura
Facilidade de manutenção e evolução
A estrutura atual permite futuras melhorias, como:
Persistência em banco de dados
Novas regras de roteamento
Monitoramento e métricas
Expansão da suíte de testes