# EzApiUrfNet - Easy API URF .NET

The goal of this project is to use a variety of open source projects to point to a database and easily create an API.  The application's purpose is to deal with 
schemas that change rapidly.  This application will use powershell and EzDbSchema/EzDbCodeGen to read a database schema and generate all the entity specific classes (model, controller, services, etc).  The api will be served up using a Swagger for easy querying.  Route Tests will also be generated to allow the user to test if schema changes should work as expected.

## Getting Started

### Prerequisites
You will need MSSQL with some database installed.  If you need a sample database,  feel free to look for the [World Wide Importers](https://github.com/Microsoft/sql-server-samples/releases/tag/wide-world-importers-v1.0) samples.

This application was created using VS2017.   These instructions are assuming that you are using VS2017 to perform these steps.  Yes, there is future plans to bring this to ASP.NET core soon. :)

Note that work is currently being done on CakeBuild to allow for easy deployent to target folders (hence the cake build files in the repo).  Consider this feature incomplete for now,  but I am almost there.  

### Using this project:

####  From NuGet
0. Have you not set the execution for powershell yet? if not, then open a powershell prompt as an admin and type C:\>`Set-ExecutionPolicy RemoteSigned` and hit enter, type 'y' to confirm anc close the window.  
1. in the command prompt or windows explorer line,  type C:\>`powershell` and hit enter
2. Navigating to the path that you want to download the code to,  type:  C:\>`git clone https://github.com/rvegajr/ez-api-urf-net` 
3. Open {gitroot}\ez-api-urf-net\Src\EzApi.sln
4. Build the Solution (need the restore all the Nuget packages)
5. Using Solution Explorer, Navigate to {Solution Root}\EzApi.Web and make sure it is selected as the "Startup Project"
6. Using Solution Explorer, Navigate to {Solution Root}\EzApi.Web\hostappsettings.json and edit it
7. Edit DefaultSettings->Settings->ConnectionString from `Server=localhost;Database=WideWorldImportersDW;user id=sa;password=sa` to your valid SQL connection 
8. Using Solution Explorer, Navigate to {Solution Root}\EzApi.Web\EzDbCodeGen\ezapicodegen.ps1, right click on this and select `Open with Powershell ISE`
9. Press the ISE Green Arrow (or press F5) to execute the script (it will ask if you are sure because it will ask the reload,  press 'y' + enter)
10. If all is good, allow the solution to reload.  Run the application.

## Deployment

This project was design to be hosted and distributed with nuget.com.

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/rvegajr/651875c08acb76009e563db128f33e7e) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/rvegajr/tags). 

## Authors

* **Ricky Vega** - *Initial work* - [Noctusoft](https://github.com/rvegajr)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

Many thanks to the following projects that have helped in this project.  This project is indebted to the hours upon hours or work that these projects have in them:
* Unit of Work & Repositories Framework - [URF](https://github.com/urfnet/URF.NET)
* TrackableEntities - [TrackableEntities](https://github.com/TrackableEntities)
* Swashbuckle - [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle)
* Swashbuckle.OData - [Swashbuckle.OData](https://github.com/andyward/Swashbuckle.OData)
* EzDbSchema - [ez-db-codegen-core](https://github.com/rvegajr/ez-db-schema)
* EzDbCodeGen - [ez-db-codegen-core](https://github.com/rvegajr/ez-db-codegen-core)
* Fody - [Fody](https://github.com/Fody/Fody)
* Fody.NullGuard - [NullGuard](https://github.com/Fody/NullGuard)
* Handlebars - [Handlebars.Net](https://github.com/rexm/Handlebars.Net)
* jquery - [jquery](http://jquery.com/)
* Json.Comparer - [Json.Comparer](https://github.com/rvegajr/Json.Comparer)
* Modernizr - [Modernizr](https://modernizr.com/)
* NLog - [NLog](https://nlog-project.org/)
* Pluralize - [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET)
* WebActivator - [WebActivator](https://github.com/davidebbo/WebActivator)
* CakeBuild - [Cakebuild.NET](https://cakebuild.net/)

## Known Issues

Swashbuckle.OData has not been updatd for ASP.NET 7.  I included a compiled version from https://github.com/andyward/Swashbuckle.OData which fixed the issues that were preventing it from rendering swagger.
The DLL was compiled and included in repo under "ez-api-urf-net\tools\Swashbuckle.OData"
