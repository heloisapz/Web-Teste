# Web API com Angular - Sistema de Login com JWT

Este projeto é uma aplicação full stack com:
- Back-end em .NET 8 Web API
- Front-end em Angular 19
- Autenticação com JWT e hash de senha

---

## Tecnologias utilizadas

- ASP.NET Core 8
- Entity Framework Core
- MySQL + Pomelo
- Angular 19
- JWT (Json Web Token)
- Criptografia de senhas

##Como criar um container Docker para essa aplicação

docker run -d \
  --name mysql-container \
  -e MYSQL_ROOT_PASSWORD=123456 \
  -e MYSQL_DATABASE=apidatabase \
  -e MYSQL_USER=usuario \
  -e MYSQL_PASSWORD=123456 \
  -p 3306:3306 \
  mysql:latest
