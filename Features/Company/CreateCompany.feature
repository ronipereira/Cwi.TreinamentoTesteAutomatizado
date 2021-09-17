#language: pt-Br

Funcionalidade: Criar empresa

Contexto: 
	Dado que a base de dados esteja limpa

Cenario: Criação de empresa com sucesso
	Dado que o usuário esteja autenticado
	E que os seguintes registros estejam inseridos na tabela 'Employee'
		| Name            | Email                         | Active |
		| 'Funcionario 1' | 'funcionario1@empresa.com.br' | True   |
	Quando realizar uma chama do tipo 'POST' para o endpoint 'v1/companies' com o corpo da requisição
		"""
			{
			  "name": "<Name>",
			  "code": "001",
			  "maxEmployeesNumber": 5
			}
		"""
	Então o código do retorno será '201'
	E o registro estará disponível na tabela 'Company' da base de dados
		| Id | Code  | Name     | MaxEmployeesNumber | Active |
		| 1  | '001' | '<Name>' | 5                  | True   |
	E o registro estará disponível na tabela 'Employee' da base de dados
		| Id | Name            | Email                         | Active |
		| 1  | 'Funcionario 1' | 'funcionario1@empresa.com.br' | True   |

	Exemplos: 
		| Name      |
		| Empresa 1 |
		| Empresa 2 |