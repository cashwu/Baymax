# Baymax

[![Build status](https://ci.appveyor.com/api/projects/status/6685ouqjoyvl0m9h?svg=true)](https://ci.appveyor.com/project/cashwu/baymax)

[![codecov](https://codecov.io/gh/cashwu/Baymax/branch/master/graph/badge.svg)](https://codecov.io/gh/cashwu/Baymax)

[![sonarcloud](https://sonarcloud.io/api/project_badges/measure?project=Baymax&metric=alert_status)](https://sonarcloud.io/dashboard?id=Baymax)

## ASP.NET Core MVC Utility Framework 
 
### Config

#### 註冊

需要在 service 加上 `AddDefaultConfigMapping()`，並且傳入 IConfiguration

```csharp
public IConfiguration Configuration { get; }

public Startup(IConfiguration configuration)
{
    Configuration = configuration;
}

public void ConfigureServices(IServiceCollection services)
{
    services.AddDefaultConfigMapping(Configuration);
}
```

#### 單一物件

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

```
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

#### 集合物件

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

```
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
