![](https://raw.githubusercontent.com/franck-gaspoz/MantisBTRestAPIClient/master/mantisbt-logo2.png)

# MantisBT REST API Client

This class library implements a C# client of the MantisBT REST API (last stable version 2.21.0), allowing to use any Mantis functionality from a C# program.
Mantis data are mapped throught DTO classes written in a POCO style.

The library is almost complete and ready to use.
It is totally free/open source/modifiable/redistribuable (see MIT licence)

The following operations are mapping the MantisBT REST API services:

- ConfigGet
    
    supplementary usefull methods can be found in MantisHTTPClient:
    - GetConfigStringOption
    - GetConfigEnumOption
    - GetConfigEnum<T>
        - where T can be ConfigPriority,ConfigSeverity,ConfigReproductibility
    - GetConfigEnumOption<T>
        - where T can be ConfigPriorities,ConfigSeverities,ConfigReproductibilities
    
- IssueAdd
- IssueGet
- IssueDelete

- ProjectGet

- LangGet

- UserGetMe
- UserAdd
- UserDelete


Asynchronous methods are also provided for use with async / await C# keywords.
For that just ad ASync after method names

The C# client has been generated using the NSwag toolchain v12.2.5.0
(NJsonSchema v9.13.37.0 (Newtonsoft.JSon v11.0.0.0))

Get more info at http://NSwag.org

The client can be generated using the NSwag project file nswag.1.1.1.nswag, which is an enhanced version of this which is given with MantisBT

The library is provided as a Visual Studio project for .NET Standard 2.0, however a project targeting .NET Framework can also target this library.

You can recompile the source code in a class library project targeting .NET Framework 4.7, build Any CPU for instance if don't wish to be linked to the .NET Standard runtime support (it is integrated in .NET Framework since version 4.7.2, in older versions the VS compiler add extra DLLs for support).

## Sample usage

First instantiate a client from the factory, providing your own HTTP client, then call an operation of the api:

```csharp
HTTPClient httpClient = new HTTPClient();
MantisBTRestAPIClient.MantisHTTPClient client = MantisHTTPClientFactory.New(
    "MantisURL"                        // eg. http://host/mantisbt/api/rest/
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
