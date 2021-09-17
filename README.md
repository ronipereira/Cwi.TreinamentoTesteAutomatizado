# Cwi.TreinamentoTesteAutomatizado
Treinamento testes automatizados: C# WebApi e SpecFlow.


## Docker
Docker é uma tecnologia que permite aos desenvolvedores empacotar, entregar e executar
aplicações em containers leves e autossuficientes.

## Instalação
Faça o [download](https://www.docker.com/get-started) do docker para o sistema operacional de sua preferência e prossiga com a instalação, em caso de dúvidas consulte o [guia de instalação](https://docs.docker.com/engine/install/).
Ao final da instalação se solicitado reinicie o sistema operacional.

## Atualização de kernel
Durante o primeiro acesso ao docker pode ser necessário a atualização do WSL 2 Kernel.

![alt text](https://i.stack.imgur.com/Tc7m4.png)

Para resolver este problema será necessário fazer o [download](https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi) e instalação do pacote de atualização do kernel.

Depois de instalar o pacote de atualização, abra o PowerShell ou o prompt de comando e execute o comando abaixo:

```wsl --set-default-version 2```

Reinicie o docker.

** O passo a passo completo para atualização do kernel pode ser acessado através do [link](https://docs.microsoft.com/pt-br/windows/wsl/install-win10#step-4---download-the-linux-kernel-update-package).

## Aplicações
Para o treinamento de automação de testes, usaremos uma Web Api desenvolvida em .Net5 com o banco de dados Postgres conteinerizados em um ambiente docker. 

## Iniciando as aplicações
Baixe o fonte da aplicação disponibilizada durante o treinamento em uma pasta de sua preferência. Abra o *PowerShell* ou *prompt de comando*(CMD) na pasta /ci e execute o comando abaixo para gerar as imagens necessárias das aplicações que serão utilizadas: 

```docker-compose build```

Após a conclusão do build, execute o comando abaixo para iniciar os containers.

```docker-compose up -d```

Se solicitado, conceda ao docker acesso a rede.

Se todos os passos forem executados com sucesso, o sistema usado para o treinamento estará disponível através da url [http://localhost:8080/swagger](http://localhost:8080/swagger).

## Encerrando as aplicações
Abra o *PowerShell* ou *prompt de comando*(CMD) na pasta /ci e execute o comando abaixo para encerrar os containers das aplicações:

```docker-compose down```

O comando acima irá parar os containers usados no treinamento, mas manterá as images e os volumes das aplicações para reuso no futuro, caso queira remover por completo os recursos usados pelas aplicações, execute o comando abaixo:

```docker-compose down -v --rmi all```
