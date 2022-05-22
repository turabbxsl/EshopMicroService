#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["src/Clients/Blazor.WebApp/WebApp/WebApp.csproj", "src/Clients/Blazor.WebApp/WebApp/"]
#RUN dotnet restore "src/Clients/Blazor.WebApp/WebApp/WebApp.csproj"
#COPY . .
#WORKDIR "/src/src/Clients/Blazor.WebApp/WebApp"
#RUN dotnet build "WebApp.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WebApp.dll"]


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app
COPY . ./ # Proyekti /app klasorune kopyaladiq
RUN dotnet publish "src/CLients/Blazor.WebApp/WebApp/WebApp.csproj"	-c Release -o /ap/build

FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=build-env /app/build/wwwroot .
COPY src/Clients/Blazor.WebApp/WebApp/nginx.conf /etc/nginx/nginx.conf