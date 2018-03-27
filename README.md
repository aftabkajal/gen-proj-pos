# NURTEX-POS

A Point of Sale Application for a small business named NURTEX.

## Getting Started

To test the application just download Debug folder. Open NURTEX-POS.exe.config file and change the connection string here

```
<connectionStrings>
    <add name="NurTexDb" connectionString="Data Source=DESKTOP-ADPGDPU;Initial Catalog=NurTexDb;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```
as your Server. Run NURTEX-POS.exe file to explore the beauty. Entity Framework will create Database for the first Time and creates a default Username & Password.
Default credentials are.

```
Username = admin
Password = 12345
```
### Prerequisites

To run this application you need
* .NET 4.6
* SQL Server 2012
* Adobe Acrobat Reader
in your Computer.

## Built With

* ASP.NET MVC
* Razor Page
* Entity Framework 6
* LINQ
* Visual C#
* SQL Server 2017
* .NET Framework 4.6
* Layer Architecture
* Generic Repository Pattern
* Visual Studio 2017


## Authors

* **Md. Aftab Uddin Kajal** - [In Facebook](https://facebook.com/aftab.kajal)
