#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY InterviewBallast.Api/*.csproj ./InterviewBallast.Api/
COPY InterviewBallast.Core/*.csproj ./InterviewBallast.Core/
COPY InterviewBallast.Domain/*.csproj ./InterviewBallast.Domain/
COPY InterviewBallast.Infrastructure/*.csproj ./InterviewBallast.Infrastructure/
COPY InterviewBallast.Service/*.csproj ./InterviewBallast.Service/
COPY InterviewBallast.Common/*.csproj ./InterviewBallast.Common/
RUN dotnet restore ./InterviewBallast.Core/InterviewBallast.Core.csproj
RUN dotnet restore ./InterviewBallast.Domain/InterviewBallast.Domain.csproj
RUN dotnet restore ./InterviewBallast.Infrastructure/InterviewBallast.Infrastructure.csproj
RUN dotnet restore ./InterviewBallast.Service/InterviewBallast.Service.csproj
RUN dotnet restore ./InterviewBallast.Common/InterviewBallast.Common.csproj
RUN dotnet restore ./InterviewBallast.Api/InterviewBallast.Api.csproj
COPY . .
WORKDIR "/src/InterviewBallast.Api"
RUN dotnet build "InterviewBallast.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InterviewBallast.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InterviewBallast.Api.dll"]