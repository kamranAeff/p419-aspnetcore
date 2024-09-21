> docker volume create ogani-volume

docker network create --subnet=172.18.0.0/24 --gateway=172.18.0.1 ogani

docker network connect --ip 172.18.0.6 --alias oganirabbitmq ogani rabbitmq

> docker inspect -f '{{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' mssqlserver

> docker build -f src/Presentation/WebApi/Dockerfile -t ogani-api:v1 .

> docker run -d -p 5080:80 --network=bridge --restart=always --name ogani-api -e ASPNETCORE_ENVIRONMENT=Production -e JWT__KEY=88b0ba2aaff3daa419917a9a1c85732570c4771b -e JWT__ISSUER="api.ogani.az" -e JWT__AUDIENCE="api.ogani.az" -e JWT__EXPIRATIONDURATIONMINUTES=5 -v ogani-volume:/app/wwwroot ogani-api:v1

--net bridge --ip 172.18.0.22


> docker run -d -p 5080:80 --restart=always -e "ASPNETCORE_ENVIRONMENT=Production" -e "JWT__KEY=88b0ba2aaff3daa419917a9a1c85732570c4771b" -e "JWT__ISSUER=api.ogani.az" -e "JWT__AUDIENCE=api.ogani.az" -e "JWT__EXPIRATIONDURATIONMINUTES=10" -e "ConnectionStrings__cString=Data Source=172.18.0.5;Initial Catalog=OganiDb;User Id=sa;Password=query;MultipleActiveResultSets=True;Encrypt=false;App=OganiWebApp" -v ogani-api:/app/wwwroot --name ogani-api ogani-api:v1

docker run -d --net bridge --ip 172.17.0.22 -p 5081:80 --restart=always -e "ASPNETCORE_ENVIRONMENT=Production" --name ogani-ui ogani-ui:v1


docker run -d --name rabbitmq rabbitmq:3 -e "" -e ""

docker run -d --name rabbitmq -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=password -p 5672:5672 -p 15672:15672 rabbitmq:3-management
