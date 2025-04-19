🔐 Web API com Angular - Sistema de Login com JWT
Este projeto é uma aplicação full stack que implementa um sistema de login seguro com autenticação JWT, utilizando:

⚙️ Back-end em .NET 8 Web API

💻 Front-end em Angular 19

🔐 Autenticação segura com JWT e hash de senha

🚀 Tecnologias Utilizadas
🖥️ Back-end
ASP.NET Core 8

Entity Framework Core

MySQL com Pomelo Provider

JWT (Json Web Token)

Criptografia de Senhas

🌐 Front-end
Angular 19

Angular Services para consumo da API

Guards e Interceptors para controle de rotas e tokens

🐳 Como rodar com Docker
Você pode criar rapidamente um container MySQL para o seu banco de dados com o seguinte comando:

bash
Copiar
Editar
docker run -d \
  --name mysql-container \
  -e MYSQL_ROOT_PASSWORD=123456 \
  -e MYSQL_DATABASE=apidatabase \
  -e MYSQL_USER=usuario \
  -e MYSQL_PASSWORD=123456 \
  -p 3306:3306 \
  mysql:latest
