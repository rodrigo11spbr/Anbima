# Anbima Consumer API

#### Description
- .Net Core 3.1
- Swagger
- Docker
- Hexagonal Architecture
- EF Core Sql Server
- Repository and Unit Of Work Pattern

#### How to Start

- <b>Migration</b>
> When the project starts, the migration will run and create the necessary table, 
> for this to work it is necessary to have the sql server with the "ANBIMA" database, 
> in <code> appsettings.json </code> can be changed the connection and the name of the bank

- <b>Log</b>
> Log provider is <code> seq </code> the address is in <code> appsettings.json </code>, 
> if you create an instance in the docker the address by default is the same

- <b>Docker</b>
> For create a image use <code>docker build -t anbima/api .</code> </br>
> For run image use <code>docker run -p 5000:5000 anbima/api</code>

- <b>Attention</b>
> For the Sql server and Seq to work, it is necessary to inform the ip of both. 
If they are in a container, 
enter the command <code> docker inspect -f '{{range .NetworkSettings.Networks}} {{. IP_Address}} {{end}} 'container_name </code> 
this will show the IP address. After you can change the address in <code>appsettings.json</code>

- Request
> [GET]<code> http://localhost:5000/api/Anbima?date=yyyy-MM-dd </code>