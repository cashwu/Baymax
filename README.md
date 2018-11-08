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
- [UnitOfWork](#unitofwork)
- [Repository](#repository)

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

## UnitOfWork

> 實作了 UnitOfWork Pattern

### 建立 DBContext 

基本上跟一般在使用的 DBContext 一樣，只是 DbSet 的 class 必需繼承 BaseEntity， DbQuery 的 class 必需繼承 QueryEntity，
這樣子才可以被 UnitOfWork 和 Repository 使用 

```csharp

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }
    
    public DbQuery<PersonView> PersonView { get; set; }
}

public class PersonView : QueryEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
}

public class Person : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; }
}
```

### 建立 UnitOfWork

建立 UnitOfWork interface 並且實作 IBaymaxUnitOfWork 然後把自己實作的 DbContext 當泛型參數傳入 

```csharp

public interface IAppUnitOfWork : IBaymaxUnitOfWork<AppDbContext>
{
}
```

建立 UnitOfWork class 並且繼承 BaymaxUnitOfWork 和實作自己的 IUnitOfWork，
並把自己實作的 DbContext 當泛型參數傳入 BaymaxUnitOfWork 和 constructor 注入

```csharp

public class AppUnitOfWork : BaymaxUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    public AppUnitOfWork(AppDbContext context)
            : base(context)
    {
    }
}
```

### 註冊

在 service 註冊自己的 UnitOfWork 為 Scoped (強烈建議註冊為 Scoped)

```csharp

public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IAppUnitOfWork, AppUnitOfWork>()
}
```

### 取得 Repository 

注入 UnitOfWork 

```csharp
public class IndexController : Controller
{
    public IndexController(IAppUnitOfWork unitOfWork){ ... }
}   
```

使用 DbContext 就可以拿到 DbContext 的實體 

```csharp
    var db = unitOfWork.DbContext; 
```

使用 GetRepository 並傳入 DbSet 的 class 當泛型參數就可以拿到 IBaymaxRepository<>

```csharp
    var repo = unitOfWork.GetRepository<Person>();
```

使用 GetViewRepository 並傳入 DbQuery 的 class 當泛型參數就可以拿到 IBaymaxQueryRepository<>  

```csharp
    var repoView = unitOfWork.GetViewRepository<PersonView>();
```

> 需注意如果 DbSet 和 DbQuery 的 class 沒有繼承相對應的 class 在裡會報錯

### Commit

原本 DBContext 的 SaveChange，有同步和非同步的方法

```csharp
    unitOfWork.Commit();
    unitOfWork.CommitAsync();
```

## Repository

> 實作了 Repository Pattern，並且封裝了一些對 Entity 的操作，需搭配上述的 UnitOfWork 使用

### Insert & InsertAsync

有三個多載，可以傳入單一 Entity 和 多筆 Entity 或是一個集合 (Async 方法使用相同)

```csharp
    repo.Insert(new Person { Id = 1, Name = "a" });

    repo.Insert(new Person { Id = 2, Name = "b" }, new Person { Id = 3, Name = "c" });

    repo.Insert(new List<Person>
    {
        new Person { Id = 4, Name = "d" },
        new Person { Id = 5, Name = "e" }
    });
```

### GetFirstOrDefault & GetFirstOrDefaultAsync

有兩個多載，主要的不同是返回物件是不是同一個 Entity，傳入參數如下 (Async 方法使用相同)

- Expression<Func<TEntity, TResult>> selector (兩個多載主要差在這個參數)
- Expression<Func<TEntity, bool>> predicate = null
- Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
- Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
- bool disableTracking = true

基本上所有的參數都有預設值 (selector 除外)，建議使用具名引數的方式來呼叫

```csharp
  repo.GetFirstOrDefault(selector: a => a.Name,
                         predicate: a => a.Id == 2,
                         orderBy: a => a.OrderBy(b => b.Id),
                         include: a => a.Include(b => b.Phones),
                         disableTracking: true);
                         
  repo.GetFirstOrDefault(predicate: a => a.Id == 1,
                         orderBy: a => a.OrderBy(b => b.Id),
                         include: a => a.Include(b => b.Phones),
                         disableTracking: true);
```

### GetAll

有兩個多載，使用方法同 GetFirstOrDefault，返回型態為 IQueryable

```csharp
  repo.GetAll(selector: a => a.Name,
              predicate: a => a.Id > 1,
              orderBy: a => a.OrderBy(b => b.Id),
              include: a => a.Include(b => b.Phones),
              disableTracking: true);
                         
  repo.GetAll(predicate: a => a.Id == 1,
              orderBy: a => a.OrderBy(b => b.Id),
              include: a => a.Include(b => b.Phones),
              disableTracking: true);
```















