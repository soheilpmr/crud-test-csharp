#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mc2.CrudTest.Client/Mc2.CrudTest.Client.csproj", "Mc2.CrudTest.Client/"]
RUN dotnet restore "Mc2.CrudTest.Client/Mc2.CrudTest.Client.csproj"
COPY . .
WORKDIR "/src/Mc2.CrudTest.Client"
RUN dotnet build "Mc2.CrudTest.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mc2.CrudTest.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Client.dll"]