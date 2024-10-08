#docker rm -f ogani-api

#docker rmi ogani-api:v1

#docker build -f src/Presentation/WebApi/Dockerfile -t ogani-api:v1 .

#docker run -d -p 5080:80 --net ogani --ip 172.18.0.20 -e "SOURCE_URL=http://localhost:5080" -e "ASPNETCORE_ENVIRONMENT=Development" -e "JWT__KEY=88b0ba2aaff3daa419917a9a1c85732570c4771b" -e "JWT__ISSUER=api.ogani.az" -e "JWT__AUDIENCE=api.ogani.az" -e "JWT__EXPIRATIONDURATIONMINUTES=10" -e "RABBIT_URL=amqp://admin:password@172.18.0.6:5672" -e "ConnectionStrings__cString=Data Source=172.18.0.5;Initial Catalog=OganiDb;User Id=sa;Password=query;MultipleActiveResultSets=True;Encrypt=false;App=OganiWebApp" -v ogani-api:/app/wwwroot --name ogani-api ogani-api:v1



#docker rm -f ogani-ui

#docker rmi ogani-ui:v1

#docker build -f src/Presentation/WebUI/Dockerfile -t ogani-ui:v1 .

#docker run -d -p 5081:80 --net ogani --restart=always -e "ASPNETCORE_ENVIRONMENT=Development" -e "API_URL=http://api.ogani.az" --add-host api.ogani.az:172.18.0.20 --name ogani-ui ogani-ui:v1


docker rm -f ogani-api
docker rmi ogani-api:v1

docker rm -f ogani-ui
docker rmi ogani-ui:v1

docker-compose -f .\docker-compose.yml --env-file local.env up -d