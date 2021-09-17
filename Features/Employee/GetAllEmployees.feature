#language: pt-Br

Funcionalidade: Obter todos os funcionários

Contexto: 
	Dado que a base de dados esteja limpa

Cenário: Obter os funcionários sem registro na base
	Dado que o usuário esteja autenticado
	Quando realizar uma chama do tipo 'GET' para o endpoint 'v1/employees'
	Então o código do retorno será '204'
