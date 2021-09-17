#language: pt-Br

@Employee
Funcionalidade: Cadastro de funcionário
	Sendo um usuário com as devidas permissões
	Quero poder cadastrar um novo funcionário

Contexto: 
	Dado que a base de dados esteja limpa

@CreateEmployee
Cenario: Cadastro de funcionário sem autenticação
	Dado que o usuário não esteja autenticado
	E que seja solicitado a criação de um novo funcionário
	Então o funcionário não será cadastrado
	E será retornado uma mensagem de falha de autenticação

@CreateEmployee
Cenario: Cadastro de funcionário com sucesso
	Dado que o usuário esteja autenticado
	E que seja solicitado a criação de um novo funcionário
	Então o funcionário será cadastrado
	E o registro estará disponível na tabela 'Employee' da base de dados
		| Id | Name            | Email                         | Active |
		| 1  | 'Funcionario 1' | 'funcionario1@empresa.com.br' | True   |