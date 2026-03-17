FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/BTGK_NHOM7.Web.Mvc/BTGK_NHOM7.Web.Mvc.csproj", "src/BTGK_NHOM7.Web.Mvc/"]
COPY ["src/BTGK_NHOM7.Web.Core/BTGK_NHOM7.Web.Core.csproj", "src/BTGK_NHOM7.Web.Core/"]
COPY ["src/BTGK_NHOM7.Application/BTGK_NHOM7.Application.csproj", "src/BTGK_NHOM7.Application/"]
COPY ["src/BTGK_NHOM7.Core/BTGK_NHOM7.Core.csproj", "src/BTGK_NHOM7.Core/"]
COPY ["src/BTGK_NHOM7.EntityFrameworkCore/BTGK_NHOM7.EntityFrameworkCore.csproj", "src/BTGK_NHOM7.EntityFrameworkCore/"]
WORKDIR "/src/src/BTGK_NHOM7.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/BTGK_NHOM7.Web.Mvc", "src/BTGK_NHOM7.Web.Mvc"]
COPY ["src/BTGK_NHOM7.Web.Core", "src/BTGK_NHOM7.Web.Core"]
COPY ["src/BTGK_NHOM7.Application", "src/BTGK_NHOM7.Application"]
COPY ["src/BTGK_NHOM7.Core", "src/BTGK_NHOM7.Core"]
COPY ["src/BTGK_NHOM7.EntityFrameworkCore", "src/BTGK_NHOM7.EntityFrameworkCore"]
WORKDIR "/src/src/BTGK_NHOM7.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "BTGK_NHOM7.Web.Mvc.dll"]
