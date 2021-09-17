#language: pt-Br

@Employee
Funcionalidade: Cadastro de funcionário com steps genéricos
	Sendo um usuário com as devidas permissões
	Quero poder cadastrar um novo funcionário

Contexto: 
	Dado que a base de dados esteja limpa

Cenario: Criação de funcionário com sucesso
	Dado que o usuário esteja autenticado
	Quando realizar uma chama do tipo 'POST' para o endpoint 'v1/employees' com o corpo da requisição
		"""
			{
			  "name": "<Name>",
			  "email": "<Email>"
			}
		"""
	Então o código do retorno será '201'
	E o registro estará disponível na tabela 'Employee' da base de dados
		| Id | Name     | Email     | Active |
		| 1  | '<Name>' | '<Email>' | True   |

	Exemplos: 
		| Name          | Email                       |
		| Funcionario 1 | funcionario1@empresa.com.br |
		| Funcionario 2 | funcionario2@empresa.com.br |