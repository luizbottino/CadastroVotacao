# CadastroVotacao
API para lidar com requisições para registro e atualização de usuários que vão cadastrar um poema, e dentre os poemas cadastrados realizar uma votação.

Para subir o programa na sua máquina.

1. Clone o repositório
2. Com ele aberto no Visual Studio abra o Console do Gerenciador de Pacotes e rode o comando Upgrade-Database
3. Faça a conexão pelo visual studio com o SQL Server pela guia Exibir -> Server Explorer
4. Nome do servidor: (localdb)\MSSQLLocalDB | Autenticação do Windows | Selecione o banco de dados "cadastrovotacao"
5. Faça um insert com os seguintes parâmetros:
6. insert into Usuario (username, senha, nome, cpf, email, uf, role, DataCadastro)
values ('alancuca', '7C4A8D09CA3762AF61E59520943DC26494F8941B', 'Alan', '12345678911', 'alancuca@gmail.com', 'DF', 'admin', GETDATE())
7. Faça o import dos request pelo Postman com a api : https://api.postman.com/collections/27689883-de3a4220-dfda-4d29-b483-e7d7614618db?access_key=PMAT-01H2GE5F2A6BQGJEAXPEFZGNH6
8. Visite a página ../Swagger para documentação
