<h1>🔐 Web API com Angular - Sistema de Login com JWT</h1>

<p>Este projeto é uma aplicação <strong>full stack</strong> que implementa um sistema de login seguro com autenticação JWT, utilizando:</p>

<ul>
  <li>⚙️ Back-end em <strong>.NET 8 Web API</strong></li>
  <li>💻 Front-end em <strong>Angular 19</strong></li>
  <li>🔐 Autenticação segura com <strong>JWT</strong> e <strong>hash de senha</strong></li>
</ul>

<hr>

<h2>🚀 Tecnologias Utilizadas</h2>

<h3>🖥️ Back-end</h3>
<ul>
  <li>ASP.NET Core 8</li>
  <li>Entity Framework Core</li>
  <li>MySQL com Pomelo Provider</li>
  <li>JWT (Json Web Token)</li>
  <li>Criptografia de Senhas</li>
</ul>

<h3>🌐 Front-end</h3>
<ul>
  <li>Angular 19</li>
  <li>Angular Services para consumo da API</li>
  <li>Guards e Interceptors para controle de rotas e tokens</li>
</ul>

<hr>

<h2>🐳 Como rodar com Docker</h2>

<p>Você pode criar rapidamente um container MySQL para o seu banco de dados com o seguinte comando:</p>

<pre>
<code>
docker run -d \
  --name mysql-container \
  -e MYSQL_ROOT_PASSWORD=123456 \
  -e MYSQL_DATABASE=apidatabase \
  -e MYSQL_USER=usuario \
  -e MYSQL_PASSWORD=123456 \
  -p 3306:3306 \
  mysql:latest
</code>
</pre>
