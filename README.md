# Baymax - ASP.NET Core MVC Utility Framework 

[![Build status](https://ci.appveyor.com/api/projects/status/6685ouqjoyvl0m9h?svg=true)](https://ci.appveyor.com/project/cashwu/baymax)

[![codecov](https://codecov.io/gh/cashwu/Baymax/branch/master/graph/badge.svg)](https://codecov.io/gh/cashwu/Baymax)

[![sonarcloud](https://sonarcloud.io/api/project_badges/measure?project=Baymax&metric=alert_status)](https://sonarcloud.io/dashboard?id=Baymax)

---

[![Baymax Nuget](https://img.shields.io/badge/Nuget-Baymax-blue.svg)](https://www.nuget.org/packages/Baymax/)

[![Baymax.Tester Nuget](https://img.shields.io/badge/Nuget-Baymax.Tester-blue.svg)](https://www.nuget.org/packages/Baymax.Tester/)

---

- [Config](#Config)
- [Log](#Log)
- [Service](#Service)

---
 
## Config

> 自動對應 config 區塊到實體型別，並註冊到 DI

### 註冊

在 service 註冊 Config，並且傳入 `IConfiguration` 當參數

```csharp
public IConfiguration Configuration { get; }

public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}

public void ConfigureServices(IServiceCollection services)
{
    services.AddConfig(Configuration);
}
```

### 單一物件

新增一個類別實作 `IConfig`，並且加上 `ConfigSection` attribute，
第一個參數為 config 的物件名稱，類別的 property 對應到 config 的內容

```csharp
[ConfigSection("Test")]
public TestConfig : IConfig
{
    public int Id { get; set; }
}
```

```json
{
    "Test" : {
        "Id" : 1
    }
}
```

使用的話就直接注入 config 的類別就好了

```csharp
public class IndexController : Controller
{
    public IndexController(TestConfig testConfig){ ... }
}   
```

預設注入的生命周期為 Scope，如果需要修改的話，請傳入第二個參數 

```csharp
[ConfigSection("Test", ServiceLifetime.Singleton)]
public TestConfig : IConfig
{
    public int Id { get; set; }
}
```

### 集合物件

如果 config 是集合時，需要在 attribute 加上第三個參數 `isCollections = true`，和第四個參數 `collectionType 集合的型別`

```csharp
[ConfigSection("Test", isCollections: true, collectionType: typeof(int)))]
public TestConfig : IConfig
{
}
```

```json
{
    "Test" : [ 1, 2, 3]
}
```

使用的話就直接注入 List of 集合的型別 

```csharp
public class IndexController : Controller
{
    public IndexController(List<int> testConfig){ ... }
}   
```

集合的型別也可以是 reference type

```csharp
[ConfigSection("Test", isCollections: true, collectionType: typeof(TestConfig)))]
public TestConfig : IConfig
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

```json
{
    "Test" : [
        { "Id" : 1, "Name" : "AA" },
        { "Id" : 2, "Name" : "BB" }
    ]
}
```

---


## Log

> 註冊 LogService 到 DI，可以同時寫入多個 Log provider

### 註冊

在 service 註冊 LogService

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddLogService();
}
```

如果要指定特別的 Assemble 的話，可以傳入 Assembly 的 prefix 當參數

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddLogService("Baymax");
}
```

### 實作 Log 

建立自定義的 Log 並且實作 ILogBase
除了 message 和 exception 之外，有多一個 EnvironmentName 的參數可以使用，例如某些特定的環境就不記錄 log

```
public class SlackLog : ILogBase
{
    public Task LogAsync(System.Exception ex, string env)
    {
        return Task.CompletedTask;
    }

    public Task LogAsync(string msg, string env)
    {
        return Task.CompletedTask;
    }
}

public class NLogProvider : ILogBase
{
    public Task LogAsync(System.Exception ex, string env)
    {
        return Task.CompletedTask;
    }

    public Task LogAsync(string msg, string env)
    {
        return Task.CompletedTask;
    }
}
```

### 使用

直接注入 ILogService 就可以使用

```csharp
public class IndexController : Controller
{
    public IndexController(ILogService logService){ ... }
}   

public void Index(){
{
    logServicr.Log("TEST");
    logServicr.Log(new ArgumentException("TEST"));
}
```

---

## Service

> 自動解析註冊結尾是 Service 的型別到 DI
> 需要注意 Service class 必需要有相對應名稱的 interface

```csharp
public class TestService : ITestService
{
    public void Handle()
    {
    }
}

public interface ITestService
{
    void Handle();
}
```

### 註冊

在 service 註冊 Service，傳入 Assembly 的 prefix 當參數

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddGeneralService("Baymax");
}
```

### 註冊自定義型別的生命周期 

預設注入 Service 的生命周期為 Scope，如果需要修改的話，請傳入第二個參數 Dictionary<Type, ServiceLifetime>

```csharp
public void ConfigureServices(IServiceCollection services)
{
   var typeLifetimeDic = new Dictionary<Type, ServiceLifetime>
   {
       { typeof(TestService), ServiceLifetime.Singleton }
   };
   
   services.AddGeneralService("Baymax", typeLifetimeDic);
}
```

### 使用

直接注入 Service 的 interface 就可以使用

```csharp
public class IndexController : Controller
{
    public IndexController(ITestService testService){ ... }
}   

public void Index(){
{
    testService.handle();
}
```











