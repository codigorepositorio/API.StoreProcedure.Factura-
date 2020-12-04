FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app

COPY ./publish/ /app
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT docker
EXPOSE 8080

ENTRYPOINT ["dotnet","/app/CANVIA.RETO.Factura.API.dll"]


#crear contendor
#PASO :
#1. dotnet publish -c Release --output ./publish
#2. docker build -t apifactura .
#3. docker run -d  -p 5011:8080 --name app-factura --network aforo255-test apifactura