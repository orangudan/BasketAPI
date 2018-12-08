FROM microsoft/dotnet:2.1-sdk AS build
COPY BasketAPI/* ./app/BasketAPI/
WORKDIR /app/BasketAPI
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/BasketAPI/out ./
ENTRYPOINT ["dotnet", "BasketAPI.dll"]