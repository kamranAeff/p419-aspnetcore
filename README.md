> docker volume create ogani-volume

docker network create --subnet=172.18.0.0/24 --gateway=172.18.0.1 ogani

docker network connect --ip 172.18.0.6 --alias oganirabbitmq ogani rabbitmq
docker network disconnect ogani rabbitmq

> docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' mssqlserver

> docker build -f src/Presentation/WebApi/Dockerfile -t ogani-api:v1 .

> docker run -d -p 5080:80 --network=bridge --restart=always --name ogani-api -e ASPNETCORE_ENVIRONMENT=Production -e JWT__KEY=88b0ba2aaff3daa419917a9a1c85732570c4771b -e JWT__ISSUER="api.ogani.az" -e JWT__AUDIENCE="api.ogani.az" -e JWT__EXPIRATIONDURATIONMINUTES=5 -v ogani-volume:/app/wwwroot ogani-api:v1

--net bridge --ip 172.18.0.22


> docker run -d -p 5080:80 --restart=always -e "ASPNETCORE_ENVIRONMENT=Production" -e "JWT__KEY=88b0ba2aaff3daa419917a9a1c85732570c4771b" -e "JWT__ISSUER=api.ogani.az" -e "JWT__AUDIENCE=api.ogani.az" -e "JWT__EXPIRATIONDURATIONMINUTES=10" -e "ConnectionStrings__cString=Data Source=172.18.0.5;Initial Catalog=OganiDb;User Id=sa;Password=query;MultipleActiveResultSets=True;Encrypt=false;App=OganiWebApp" -v ogani-api:/app/wwwroot --name ogani-api ogani-api:v1

docker run -d --net bridge --ip 172.17.0.22 -p 5081:80 --restart=always -e "ASPNETCORE_ENVIRONMENT=Production" --name ogani-ui ogani-ui:v1


docker run -d --name rabbitmq rabbitmq:3 -e "" -e ""

docker run -d --name rabbitmq -v rabbit:/etc/rabbitmq/ -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=password -p 5672:5672 -p 15672:15672 rabbitmq:3-management

docker update --restart=always rabbitmq

docker update-v rabbit:"/etc/rabbitmq/" rabbitmq


[Serilog](https://medium.com/@brucycenteio/adding-serilog-to-asp-net-core-net-7-8-5cba1d0dea2)


> docker run -d -p 27017:27017 --net ogani --ip 172.18.0.3 --name ogani-mongo -e MONGO_INITDB_ROOT_USERNAME=ogani -e MONGO_INITDB_ROOT_PASSWORD=ogani123 -v ogani-mongo:/data/db mongo



{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console", "Serilog.Sinks.MongoDB" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "MongoDB",
        "Args": {
          "connectionString": "mongodb://ogani:ogani123@172.18.0.3:27017/?authSource=admin&authMechanism=SCRAM-SHA-1",
          "databaseName": "ogani",
          "collectionName": "logs",
          "autoCreateCollection": true
        }
      }
      //{
      //  "Name": "File",    ##### , "Serilog.Sinks.File"
      //  "Args": {
      //    "path": "Logs/log-ogani-api-.json",
      //    "rollingInterval": "Day",
      //    "rollOnFileSizeLimit": false,
      //    "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog"
      //  }
      //},
      //{
      //  "Name": "MSSqlServer",
      //  "Args": {
      //    "connectionString": "data source=.;initial catalog=BigonP516;user id=sa;password=query;MultipleActiveResultSets=True;Encrypt=false",
      //    "sinkOptionsSection": {
      //      "tableName": "Logs",
      //      "schemaName": "Serilog",
      //      "autoCreateSqlTable": true
      //    }
      //  }
      //}
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId" ],
    "Properties": {
      "Application": "Ogani Web Api"
    }
  }
}
