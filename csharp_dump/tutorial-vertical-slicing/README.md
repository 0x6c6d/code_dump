# Dashboard

Dummy Dashboard for a warehouse & store manager.

## Local Debugging

<ins>Create DB</ins>

``` Package Manager Console
dotnet ef migrations add InitialMigration --project Application -o "Common/Persistence/Migrations"
dotnet ef database update --project Application
```

<ins>Stored Procedure</ins>
- create a stored procedure in your local db
- the query under "Master Kennung -> Stored Procedure" (below) can be executed on your local database to create the stored procedure

## Store Kennung 

<ins>Stored Procedure</ins>
- used to do a full text search on the dbo.Technologies table
- the query below can be executed on the eng-db\engbers\SupportDashboard database to create the stored procedure

``` Sql
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE SearchTechnologies
    @SearchInput NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT StoreId
    FROM Technologies
    WHERE
    (
        Phone LIKE '%' + @SearchInput + '%' OR
        CashDeskIp LIKE '%' + @SearchInput + '%' OR
        CashDeskName LIKE '%' + @SearchInput + '%' OR
        TerminalId LIKE '%' + @SearchInput + '%' OR
        TerminalIp LIKE '%' + @SearchInput + '%' OR
        RouterIp LIKE '%' + @SearchInput + '%' OR
        RouterStoragePlace LIKE '%' + @SearchInput + '%' OR
        TkStoragePlace LIKE '%' + @SearchInput + '%' OR
        InternetUserName LIKE '%' + @SearchInput + '%' OR
        InternetPassword LIKE '%' + @SearchInput + '%' OR
        InternetCustomerId LIKE '%' + @SearchInput + '%' OR
        Comments LIKE '%' + @SearchInput + '%' OR
        FiscalSN LIKE '%' + @SearchInput + '%' OR
        FiscalPlace LIKE '%' + @SearchInput + '%' OR
        VideoSystem LIKE '%' + @SearchInput + '%' OR
        KeyNumber LIKE '%' + @SearchInput + '%' OR
        EcDevice LIKE '%' + @SearchInput + '%' OR
        Switch LIKE '%' + @SearchInput + '%' OR
        SwitchText LIKE '%' + @SearchInput + '%' OR
        FritzBoxIp LIKE '%' + @SearchInput + '%' OR
        AccessPoint LIKE '%' + @SearchInput + '%' OR
        VideoroIpFirst LIKE '%' + @SearchInput + '%' OR
        VideoroIpSecond LIKE '%' + @SearchInput + '%' OR
        AirConditionerIp LIKE '%' + @SearchInput + '%' OR
        StoreEverIp LIKE '%' + @SearchInput + '%' OR
        KfzIpFirst LIKE '%' + @SearchInput + '%' OR
        KfzIpSecond LIKE '%' + @SearchInput + '%' OR
        Router LIKE '%' + @SearchInput + '%' OR
        MusicMaticIP LIKE '%' + @SearchInput + '%' OR
        InternetConnectionId LIKE '%' + @SearchInput + '%' OR
        InternetAccessId LIKE '%' + @SearchInput + '%'
    );
END
GO
```