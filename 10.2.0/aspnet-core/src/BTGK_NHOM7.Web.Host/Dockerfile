FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/BTGK_NHOM7.Web.Host/BTGK_NHOM7.Web.Host.csproj", "src/BTGK_NHOM7.Web.Host/"]
COPY ["src/BTGK_NHOM7.Web.Core/BTGK_NHOM7.Web.Core.csproj", "src/BTGK_NHOM7.Web.Core/"]
COPY ["src/BTGK_NHOM7.Application/BTGK_NHOM7.Application.csproj", "src/BTGK_NHOM7.Application/"]
COPY ["src/BTGK_NHOM7.Core/BTGK_NHOM7.Core.csproj", "src/BTGK_NHOM7.Core/"]
COPY ["src/BTGK_NHOM7.EntityFrameworkCore/BTGK_NHOM7.EntityFrameworkCore.csproj", "src/BTGK_NHOM7.EntityFrameworkCore/"]
WORKDIR "/src/src/BTGK_NHOM7.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/BTGK_NHOM7.Web.Host", "src/BTGK_NHOM7.Web.Host"]
COPY ["src/BTGK_NHOM7.Web.Core", "src/BTGK_NHOM7.Web.Core"]
COPY ["src/BTGK_NHOM7.Application", "src/BTGK_NHOM7.Application"]
COPY ["src/BTGK_NHOM7.Core", "src/BTGK_NHOM7.Core"]
COPY ["src/BTGK_NHOM7.EntityFrameworkCore", "src/BTGK_NHOM7.EntityFrameworkCore"]
WORKDIR "/src/src/BTGK_NHOM7.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "BTGK_NHOM7.Web.Host.dll"]
