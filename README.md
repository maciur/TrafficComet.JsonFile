# TrafficComet
Tool for saving information about (request/response) traffic from website written in .Net Core 2.1 to json files

## Simple Instalation 
#### Run command in Nuget Package Manager 
```csharp
Install-Package TrafficComet.JsonFile.LogWriter
``` 

#### Edit Startup file like below :
```csharp 
public class Startup
{
  	public IConfiguration Configuration { get; }
  
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddMvc();
		services.AddTrafficCometJsonFileLogWriter(Configuration);
	}

	public void Configure(IApplicationBuilder app, IHostingEnvironment env)
	{
		app.UseTrafficComet();
		app.UseMvc();
	}
}
```

#### Add configuration to appsettings.js :
```json 
"TrafficComet": {
    "Generator": {
      "ClientId": {
        "CookieName": "Name of cookie where Traffic Comet will store ClientId",
        "HeaderName": "Name of header where Traffic Comet will read ClientId"
      },
      "TraceId": {
        "HeaderName": "Name of header where Traffic Comet will read TraficId"
      }
    },
    "Writers": {
      "JsonFile": {
        "Folder": "Path to folder where you want to store all log files"
      }
    },
    "Middleware": {
      "Root": {
        "ApplicationId": "Application Name or Id"
      }
    }
  }
``` 
  
  
