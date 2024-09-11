# SDSDemo

ASP MVC .NET Framework 4.7.2 Application demo for SD Solutions using ADO.NET with Stored Procedures and utilizing several Architectural Software Design Patterns and Folder Structures.

Utilized UnityContainer as well for Dependency Injection

SQL script for creating the database,tables,and stored procedures is included in the project as script.sql

Use own's connection string on the Web.Config file.

If an error has been encountered specifically "Could not find a part of the path ... bin\roslyn\csc.exe" this is due to the nuget package bug simply paste this and execute on the "Package Manager Console" in Visual Studio 


'Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r' (without the single quotes)
