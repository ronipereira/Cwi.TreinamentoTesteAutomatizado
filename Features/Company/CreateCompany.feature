#language: pt-Br

Funcionalidade: Criar empresa

Contexto: 
	Dado que a base de dados esteja limpa

Cenario: Criação de empresa com sucesso
	Dado que o usuário esteja autenticado
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

	Exemplos: 
		| Name      |
		| Empresa 1 |
		| Empresa 2 |