# Instalación

## a) Lo primero es instalar dotnet en el equipo

https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu

## b) Instalar las herramientas de entity framework

```
dotnet tool install --global dotnet-
```

## c) Correr las migraciones

```
dotnet ef migrations add InitialCreate
```
```
dotnet ef database update
```

## d) Verificar el host en el archivo appsettings.json

```
"DefaultConnection": "Host=postgres;Port=5432;Username=decreto-dotnet;Password=7nx%?U%YWVDmAU%?;Database=decreto-dotnet;"
```



