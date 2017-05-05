FROM microsoft/dotnet
ARG source=.
WORKDIR /DapperProject1
EXPOSE 80
COPY $source .
ENTRYPOINT ["dotnet", "DapperProject1.dll"]
