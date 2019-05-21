![](https://raw.githubusercontent.com/franck-gaspoz/MantisBTRestAPIClient/master/mantisbt-logo.png)

# MantisBT REST API Client

This class library implements a C# client of the MantisBT REST API (last stable version 2.21.0), allowing to use any Mantis functionalities from a C# program.
Mantis data are mapped throught DTO classes written in a POCO style.

The library is complete and ready to use.
It is totally free/open source/modifiable/redistribuable (see MIT licence)

The following operations are mapping the MantisBT REST API services:

- ConfigGet
- IssueAdd
- IssueDelete
- IssueGet
- IssueDelete
- LangGet
- UsetGetMe

Asynchronous methods are also provided for use with async / await C# keywords:

- ConfigGetAsync
- IssueAddAsync
- IssueDeleteAsync
- IssueGetAsync
- IssueDeleteAsync
- LangGetAsync
- UsetGetMeAsync

The C# client has been generated using the NSwag toolchain v12.2.5.0
(NJsonSchema v9.13.37.0 (Newtonsoft.JSon v11.0.0.0))

Get more info at http://NSwag.org

The client can be generated using the NSwag project file nswag.nswag

The library is provided as a Visual Studio project for .NET Framework 4.7, build Any CPU.

## Sample usage

First instantiate a client from the factory, providing your own HTTP client, then call an operation of the api:

```csharp
HTTPClient httpClient = new HTTPClient();
MantisBTRestAPIClient.Client client = ClientFactory.New(
    "MantisURL"                        // eg. http://mantishost/api/mantisbt/api/rest/
    "MantisBTRestAPISecurityToken",
    httpClient
);
UserMeResponse user = client.UserGetMe();         // call the MantisBT REST API
```

## About MantisBT

MantisBT is an open source issue tracker that provides a delicate balance between simplicity and power. 
Users are able to get started in minutes and start managing their projects while collaborating with their teammates and clients effectively. 

Get more info at https://www.mantisbt.org

## About MantisBT REST API settings

The REST API is enabled by default. A Swagger sandbox and documentation for REST API is available at /api/rest/swagger/ below the MantisBT root.

Get more info at https://www.mantisbt.org/docs/master/en-US/Admin_Guide/html/admin.config.api.html

For the sandbox to work, MantisBT must be hosted at the root folder of the host. For example: http://mantishost/ rather http://host/mantisbt.  If that is not the case, then create a host alias to map it as such or edit swagger.json to change basePath to include the mantisbt folder name.
