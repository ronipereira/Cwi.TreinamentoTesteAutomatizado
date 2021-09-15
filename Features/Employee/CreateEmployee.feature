﻿#language: pt-Br

Funcionalidade: Cadastro de funcionário
	Sendo um usuário com as devidas permissões
	Quero poder cadastrar um novo funcionário

Cenario: Cadastro de funcionário sem autenticação
	Dado que o usuário não esteja autenticado
	E que seja solicitado a criação de um novo funcionário
	Então o funcionário não será cadastrado
	E será retornado uma mensagem de falha de autenticação

Cenario: Cadastro de funcionário com sucesso
	Dado que o usuário esteja autenticado
	E que seja solicitado a criação de um novo funcionário
	Então o funcionário será cadastrado