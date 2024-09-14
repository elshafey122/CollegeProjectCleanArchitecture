FROM mcr.microsoft.com/dotnet/sdk:6.0 as Build-dev
WORKDIR /app

COPY ["CollegeProject.Api/CollegeProject.Api.csproj","CollegeProject.Api/"]
COPY ["CollegeProject.Core/CollegeProject.Core.csproj","CollegeProject.Core/"]
COPY ["CollegeProject.Infrucstructure/CollegeProject.Infrucstructure.csproj","CollegeProject.Infrucstructure/"]
COPY ["CollegeProject.Service/CollegeProject.Service.csproj","CollegeProject.Service/"]
COPY ["CollegeProject.Data/CollegeProject.Data.csproj","CollegeProject.Data/"]

RUN dotnet restore "CollegeProject.Api/CollegeProject.Api.csproj"
RUN dotnet restore "CollegeProject.Core/CollegeProject.Core.csproj"
RUN dotnet restore "CollegeProject.Infrucstructure/CollegeProject.Infrucstructure.csproj"
RUN dotnet restore "CollegeProject.Service/CollegeProject.Service.csproj"
RUN dotnet restore "CollegeProject.Data/CollegeProject.Data.csproj"

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
EXPOSE 80 
COPY --from=Build-dev /app/out .
ENTRYPOINT [ "dotnet","SchoolProject.dll" ]







