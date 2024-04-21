# WarehouseManager

Warehouse Management Software for the IT-Storage

<br>

## Structure

1. API: an API to interact with the database
2. UI: Blazor Server frontend to interact with the API
    - the UI communicates with the API via the following URL: http://localhost:7249/ -> e.g. http://localhost:7249/api/Group (gets all available groups)

<br>

## API Documentation

Swagger is implemented. Clone the repository & start the API project. Then you can see the documentation when opening this url: https://localhost:7249/swagger/index.html

Before the API project can start properly you have to create a local version of the database. I already created a migration so you just need to execute the migration code.

1. Enter in Package Manager Console: `dotnet tool install --global dotnet-ef`
2. Enter in Package Manager Console: `dotnet ef migrations add InitialMigration --project Persistence`
    - no need for that -> Migration is already created & can be found under Infrastructure/Persistence/Migrations
    - you can delete the Migrations folder, execute the command and create a new migration if you want to
3. Enter in Package Manager Console: `dotnet ef database update --project --project Persistence`
    - this will create the database & tables
