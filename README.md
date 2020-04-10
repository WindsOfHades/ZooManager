# ZooManager
This is a C# WPF application that talks to a database.

The database used is a docker image of MS SQL server (created by the following command):

``` bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pass@word' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

to check database is up and running:
``` bash
winpty docker exec -it [container ID] bash
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Pass@word

1> select name from sys.databases;
2> go
name
--------------------------------------------------------------------------------------------------------------------------------
master
tempdb
model
msdb
```

The intention of the project is to use various WPF elements with handled events. I have added a database with 2 tables "Zoo", "Animal" and a relation table to determine which animal belongs to which zoo.

There are some buttons to CRUD zoos and animals as well.

![Zoo_Manager](https://i.ibb.co/7t4VsjT/Zoo-Manager.png)
