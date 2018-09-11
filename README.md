# EzApiUrfNet - Easy API URF .NEt

The goal of this project is to use a variety of open source projects to point to a database and easily create an API.

## Getting Started

### Prerequisites
You will need MSSQL with some database installed.  If you need a sample database,  feel free to look for the [World Wide Importers](https://github.com/Microsoft/sql-server-samples/releases/tag/wide-world-importers-v1.0) samples.

### Using this project:

####  From NuGet

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

Many thanks to the following projects that have helped in this project
* Unit of Work & Repositories Framework - [URF](https://github.com/urfnet/URF.NET)
* TrackableEntities - [TrackableEntities](https://github.com/TrackableEntities)
* Swashbuckle - [Swashbuckle.OData](https://github.com/andyward/Swashbuckle.OData)
* EzDbCodeGen - [ez-db-codegen-core](https://github.com/rvegajr/ez-db-codegen-core)

## Known Issues

Swashbuckle.OData has not been updatd for ASP.NET 7.  I included a compiled version from https://github.com/andyward/Swashbuckle.OData which fixed the issues that were preventing it from rendering swagger.
