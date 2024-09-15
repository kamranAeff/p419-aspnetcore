docker rm -f ogani-api

docker rmi ogani-api:v1

docker build -f src/Presentation/WebApi/Dockerfile -t ogani-api:v1 .

docker run -d -p 5080:80 --restart=always -e "ASPNETCORE_ENVIRONMENT=Development" -e "JWT__KEY=88b0ba2aaff3daa419917a9a1c85732570c4771b" -e "JWT__ISSUER=api.ogani.az" -e "JWT__AUDIENCE=api.ogani.az" -e "JWT__EXPIRATIONDURATIONMINUTES=10" -e "ConnectionStrings__cString=Data Source=172.17.0.2;Initial Catalog=OganiDb;User Id=sa;Password=query;MultipleActiveResultSets=True;Encrypt=false;App=OganiWebApp" -v ogani-api:/app/wwwroot --name ogani-api ogani-api:v1