# Baymax - ASP.NET Core MVC Utility Framework 

[![Build status](https://ci.appveyor.com/api/projects/status/6685ouqjoyvl0m9h?svg=true)](https://ci.appveyor.com/project/cashwu/baymax)

[![codecov](https://codecov.io/gh/cashwu/Baymax/branch/master/graph/badge.svg)](https://codecov.io/gh/cashwu/Baymax)

[![sonarcloud](https://sonarcloud.io/api/project_badges/measure?project=Baymax&metric=alert_status)](https://sonarcloud.io/dashboard?id=Baymax)

---

[![Baymax Nuget](https://img.shields.io/badge/Nuget-Baymax-blue.svg)](https://www.nuget.org/packages/Baymax/)

[![Baymax.Tester Nuget](https://img.shields.io/badge/Nuget-Baymax.Tester-blue.svg)](https://www.nuget.org/packages/Baymax.Tester/)

---

- [Config](#config)
- [Log](#log)
- [Service](#service)
- [BackgroundService](#backgroundservice)

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

如果要指定特別的 Assemble 的話，可以傳入 Assembly 的 prefix 當參數

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddConfig(Configuration, "Baymax");
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

```csharp
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

預設注入 Service 的生命周期為 Scope，如果需要修改的話，請傳入第二個參數 Dictionary<Type, ServiceLifetime>，
KEY 為 Service class 的 Type，VALUE 為 ServiceLifetime

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

---

## BackgroundService

> 可定期 (排程) 執行程式

### 實作 IBackgroundProcessService

把需要定期執行的程式碼寫在 DoWork，把停止時需要取消的程式碼寫在 StopWork

> 有時 StopWork 不一定會有程式碼
   
```csharp
public class TestBackgroundService : IBackgroundProcessService
{
    public TestBackgroundService()
    {
    }
    
    public void DoWork()
    {
    }

    public void StopWork()
    {
    }
}
``` 

### Config 

在 config 裡面新增一個 BackgroundService 的區段，裡面的 KEY 就是實作 IBackgroundProcessService 的類別名稱加上 Interval，
VALUE 就是需要定期執行的周期

> 注意單位為 `毫秒`，以下面的程式為例就是 1 秒會執行一次

```json
{
    "BackgroundService" : {
        "TestBackgroundServiceInterval" : "1000"
    }
}
```

### 註冊

在 service 註冊 BackgroundService，傳入實作 IBackgroundProcessService 的 type 當參數

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddBackgroundService(typeof(TestBackgroundService))
}
```

可實作多個 IBackgroundProcessService 同時傳入

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddBackgroundService(typeof(TestBackgroundService1), typeof(TestBackgroundService2))
}
```

### 使用

無需寫任何程式碼，設定的時間周期就會定期執行

> 需注意的是第一次註冊 BackgroundService 時就會執行一次程式碼

---











