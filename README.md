# Baymax - ASP.NET Core MVC Utility Framework 

[![Build status](https://ci.appveyor.com/api/projects/status/6685ouqjoyvl0m9h?svg=true)](https://ci.appveyor.com/project/cashwu/baymax)

[![codecov](https://codecov.io/gh/cashwu/Baymax/branch/master/graph/badge.svg)](https://codecov.io/gh/cashwu/Baymax)

[![sonarcloud](https://sonarcloud.io/api/project_badges/measure?project=Baymax&metric=alert_status)](https://sonarcloud.io/dashboard?id=Baymax)

---

[![Baymax Nuget](https://img.shields.io/badge/Nuget-Baymax-blue.svg)](https://www.nuget.org/packages/Baymax/)

[![Baymax.Tester Nuget](https://img.shields.io/badge/Nuget-Baymax.Tester-blue.svg)](https://www.nuget.org/packages/Baymax.Tester/)

---
- [Baymax](#baymax)
    - [Config](#config)
    - [Log](#log)
    - [Service](#service)
    - [BackgroundService](#backgroundservice)
    - [UnitOfWork](#unitofwork)
    - [Repository](#repository)
        - [GetFirstOrDefault & GetFirstOrDefaultAsync](#getfirstordefault--getfirstordefaultasync)
        - [GetAll](#getall)
        - [GetPagedList & GetPagedListAsync](#getpagedlist--getpagedlistasync)
        - [Find](#find)
        - [Count](#count)
        - [Any](#any)
        - [FromSql](#fromsql)
        - [Insert & InsertAsync](#insert--insertasync)
        - [Update](#update)
        - [Delete](#delete)
    - [ViewRepository](#viewrepository) 
        - [GetAll](#getall-1)
        - [FromSql](#fromsql-1)
    - [Util](#util)
        - [Enumeration](#ennumeration)

- [Baymax.Tester](#baymaxtester)   
    - [Integration Test](#integration-test) 
    - [Unit Test](#unit-test) 
---

# Baymax
 
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

在 config 裡面新增一個 BackgroundService 的區段，裡面的 KEY 就是實作 IBackgroundProcessService 的類別名稱，
Value 就是需要定期執行的周期

> 注意單位為 `毫秒`，以下面的程式為例就是 1 秒會執行一次

```json
{
    "BackgroundService" : {
        "TestBackgroundService" : 1000
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

無需寫任何程式碼，設定的時間周期就會定期執行，沒有設定時間的只會執行一次 DoWork 

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

### ExecuteSqlCommand

UnitOfWork 可以直接執行原始 SQL 語句，可以使用字串內插的方式或是使用 Parameter 的方式傳參數

```csharp
unitOfWork.ExecuteSqlCommand($"delete from Phone where id = {1}");

unitOfWork.ExecuteSqlCommand("delete from Phone where id = @id", new SqlParameter("id", 1));
```

### Entity Validation

可以在 Commit 之前針對 Insert 和 Update 的 Entity 作 Validation，
必需在 Service 註冊 AddEntityValidation<TEntity>，然後在裡面寫 Validation 的檢查條件，
注意 Func 的傳入參數為 object，傳出為 ValidationResult

> 一個 Entity 的型別必須要加一次 AddEntityValidation

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddEntityValidation<Person>(o => {
        var p = o as Person;
        if (p.Name.Length < 5)
        {
            return new ValidationResult("Name Error", new[]
            {
                "Name"
            });
        }

        return ValidationResult.Success;
    });
}
```

之後在 Commit 的時候就會自動針對註冊的 Entity 作檢查，有問題的話會 throw EntityValidationException，
裡面的 Exception，會有所有 Entity 的 ValidationException

```csharp
try
{
    unitOfWork.Commit();
}
catch (EntityValidationException ex)
{
     List<ValidationException> validationExceptions = ex.Exceptions;
}
```

## Repository

> 實作了 Repository Pattern，並且封裝了一些對 Entity 的操作，需搭配上述的 UnitOfWork 使用

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

### GetPagedList & GetPagedListAsync

有兩個多載，使用方法同 GetFirstOrDefault，傳入參數多了 PageIndex 和 PageSize (Async 方法使用相同)
PageIndex 從 0 開始，預設 PageSize 為 20

```csharp
repo.GetPagedList(selector: a => a.Name,
                predicate: a => a.Id > 1,
                orderBy: a => a.OrderBy(b => b.Id),
                include: a => a.Include(b => b.Phones),
                pageIndex = 0, 
                pageSize = 10, 
                disableTracking: true);
                     
repo.GetPagedList(predicate: a => a.Id > 1,
                orderBy: a => a.OrderBy(b => b.Id),
                include: a => a.Include(b => b.Phones),
                pageIndex = 0, 
                pageSize = 10, 
                disableTracking: true);
```

返回型態為 IPagedList<TResult>，資料在 Items 裡面

```csharp
public interface IPagedList<T>
{
    int IndexFrom { get; } 

    int PageIndex { get; } 

    int PageSize { get; } 

    int TotalCount { get; }

    int TotalPages { get; }

    IList<T> Items { get; }

    bool HasPreviousPage { get; }

    bool HasNextPage { get; }
}
```

### Find

傳入參數為 Key (Async 方法使用相同)

```csharp
repo.Find(1);

repo.Find(1, "key");
```

### Count

可以傳入 Predicate，取得數量

```csharp
repo.Count();

repo.Count(a => a.Id > 1);
```

### Any

可以傳入 Predicate，取得是否有資料

```csharp
repo.Any();

repo.Any(a => a.Id > 1);
```

### FromSql

執行原始 SQL 語句，有兩個多載，可以使用字串內插的方式或是使用 Parameter 的方式傳參數


```csharp
repo.FromSql($"select * from Person where id = {1}");

repo.FromSql("select * from Person where id = @id", new SqlParameter("id", 1));
```

### Insert & InsertAsync

有三個多載，可以傳入單一 Entity、多筆 Entity 或是一個集合 (Async 方法使用相同)

```csharp
repo.Insert(new Person { Id = 1, Name = "a" });

repo.Insert(new Person { Id = 2, Name = "b" }, new Person { Id = 3, Name = "c" });

repo.Insert(new List<Person>
{
    new Person { Id = 4, Name = "d" },
    new Person { Id = 5, Name = "e" }
});
```

### Update 

有三個多載，可以傳入單一 Entity、多筆 Entity 或是一個集合 

```csharp
var persons = repo.GetAll();

persons[0].Name = "123";
persons[1].Name = "456";

repo.Update(persons[0]);

repo.Update(persons[0], person[1]);

repo.Insert(persons);
```

### Delete

有四個多載，可以傳入 Entity 的 Key、單一 Entity、多筆 Entity 或是一個集合 

```csharp
var persons = repo.GetAll();

persons[0].Name = "123";
persons[1].Name = "456";

repo.Delete(1);

repo.Delete(persons[0]);

repo.Delete(persons[0], person[1]);

repo.Delete(persons);
```

## ViewRepository

> 基本上和 Repository 一樣，只是給 View 使用 

### GetAll

有兩個多載，使用方法同 Repository，只是少了 include 的參數 

```csharp
repo.GetAll(selector: a => a.Name,
          predicate: a => a.Id > 1,
          orderBy: a => a.OrderBy(b => b.Id),
          disableTracking: true);
                     
repo.GetAll(predicate: a => a.Id == 1,
          orderBy: a => a.OrderBy(b => b.Id),
          disableTracking: true);
```

### FromSql

執行原始 SQL 語句，有兩個多載，可以使用字串內插的方式或是使用 Parameter 的方式傳參數，使用方法同 Repository

```csharp
repo.FromSql($"select * from PersonView where id = {1}");

repo.FromSql("select * from PersonView where id = @id", new SqlParameter("id", 1));
```


## Util 

### Enumeration

> Enum Object

#### 建立

建立一個 Enum 物件去繼承 Enumeration，並傳入建立的物件當泛型參數

```csharp
public class EnumTest : Enumeration<EnumTest>
{
}
```

建立 `private` constructor，並傳入 value 和 displayName 兩個參數

```csharp
public class EnumTest : Enumeration<EnumTest>
{
    private EnumTest(int value, string displayName)
            : base(value, displayName)
    {
    }
}
```

建立 static field 當成 Enum 的內容，Value 就是 Enum 的值，DisplayName 就是 Enum 的名稱

```csharp
public class EnumTest : Enumeration<EnumTest>
{
    public static readonly EnumTest A = new EnumTest(1, "A");
    public static readonly EnumTest B = new EnumTest(2, "B");

    private EnumTest(int value, string displayName)
            : base(value, displayName)
    {
    }
}
```

#### 使用

跟使用一般 Enum 一樣可以直接用物件點出

```csharp
var a = EnumTest.A;
var b = EnumTest.B;
```

如果要拿到名稱或是值的話，可以使用 DisplayName 和 Value，ToString 也可以直接拿到 DisplayName

```csharp
var e = EnumTest.A;
e.Value; // 1
e.DisplayName; // "A"
e.ToString(); // "A"
```

也可以使用 GetAll 拿到全部 Enum Object 的內容

```csharp
EnumTest.GetAll(); // IEnumerable<EnumTest>
```

#### 轉換

可以透過名稱或是值來轉換成 Enum Object

```csharp
EnumTest.FromValue(1); // EnumTest.A
EnumTest.FromDisplayName("A"); // EnumTest.A
```

#### 比較

可以使用 Equals 或是 CompareTo 來比較是否相等

```csharp
EnumTest.A.Equals(EnumTest.B) // false
EnumTest.A.CompareTo(EnumTest.A) // true
```

#### JsonConvert

如果有使用 Json.NET 來 Convert Enum Object 的話，需要在屬性加上 `JsonConverter` 的 attribute，
傳入的參數為 `typeof(EnumerationJsonCovert)`，這樣子 SerializeObject 或是 DeserializeObject 的內容才會正確

```csharp
public class Test
{
    public int Id { get; set; }

    [JsonConverter(typeof(EnumerationJsonCovert))]
    public EnumTest EnumTest { get; set; }
}
```

---
---

# Baymax.Tester

## Integration Test

> 可以讓你容昜的建立 Test Server 和 InMemoryDB 作測試

### 建立 TestBase

建立一個 class 去實作 IClassFixture，並傳入 ApplicationFactory 當泛型參數，
而 ApplicationFactory 的泛型參數是你 Web App 的 Startup 和 DBContext 類別，
constructor 需要注入 ApplicationFactory 並且建立一個 field 給外部 test class 使用

> 在這裡是使用 xUnit test framework

> 建立出來的測試 client EnvironmentName 為 Test，可以使用 IHostingEnvironmentExtensions 的 IsTest 來判斷
    

```csharp
public class TestBase : IClassFixture<ApplicationFactory<Startup, AppDbContext>>
{
    protected readonly ApplicationFactory<Startup, AppDbContext> Factory;

    public TestBase(ApplicationFactory<Startup, AppDbContext> factory)
    {
        Factory = factory;
    }
}
```

### DB Init Data

ApplicationFactory 裡面有一個 InitDataEvent 可以使用，
可以在 TestBase 的 constructor 註冊事件塞初始資料

```csharp
public TestBase(ApplicationFactory<Startup, AppDbContext> factory)
{
    Factory.InitDataEvent += OnInitDataEvent;
}

private void OnInitDataEvent(object sender, InitDataEventArgs<AppDbContext> e)
{
    var dbcontext = e.DbContext;
    // insert data here
    dbcontext.SaveChanges();
}
```

### 使用 HttpClient

test class 去繼承前面建立的 TestBase，並在 constructor 注入 ApplicationFactory

```csharp
public class WebTests : TestBase
{
    public WebTests(ApplicationFactory<Startup, AppDbContext> factory) : base(factory)
    {
    }
}
``` 

在 Factory 裡面有 HttpClient 可以發送 http request 到網站，HttpClient 有基本的 Http Method Extension 可以讓你更方便的使用，
需要注意的是 url 是不包含 host 

- PostHttpResult
- GetHttpResult
- PutHttpResult
- DeleteHttpResult

> 更多的使用方式請參考 [測試](/Tests/Baymax.Tester.Tests/Integration/WebTests.cs) 

```csharp
[Fact]
public void GetAll()
{
    var result = Factory.HttpClient.GetHttpResult<List<Info>>("/api/values");
}
```

### 使用 DBContext

在 Factory 裡面有 DbOperator method 可以使用，傳入參數為 DbContext 的 Action 

> 在測試使用的是 InMemoryDatabase，所以有些 DB 的操作是不支援的

```csharp
Factory.DbOperator(db =>
{
    db.Info.Add(new Info { Id = 1, Name = "Test123" });
    db.Info.Add(new Info { Id = 2, Name = "Test456" });
    db.SaveChanges();
});
```

### 取得其它類別

使用 Factory 的 Operator method，傳入參數為泛型類別的 Action，
例如有一個 RedisService

```csharp
Factor.Operator<RedisService>(redis => 
{
    // do something here
});
```

## Unit Test

> 可以讓你使用 Fluent 的方式測試 controller 的 action 

### 取得 Action 的執行結果 

取得 controller 的實體之後使用擴充方法 `.AsTester()`，然後在用 Action 方法，
傳入 Func 呼叫 controller 的 action 就可以拿到 action 的執行結果

```csharp
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

public class Test
{
    [Fact]
    public void Test()
    {
        var result = new HomeController()
                            .AsTester()
                            .Action(c => c.Index());
    }
}
```


### 驗證 ActionResult

取得結果後使用 `ShouldBeXXXResult` 就可以驗證 ActionResult 的型別，
XXX 取決 Action 真實的返回結果，以下面的程式碼來說就是返回 ViewResult

```csharp
[Fact]
public void Test()
{
    new HomeController()
        .AsTester()
        .Action(c => c.Index())
        .ShouldBeViewResult();
}
```

> 支援非同步 ActionResult

### 驗證 ActionResult 裡面的屬性

可以用 `WithXXX` 來驗證 ActionResult 的公開屬性，
XXX 取決 ActionResult 的型別，以下面的程式碼來說 ViewResult 可以驗證
 Model (WithModel)、ViewBag (WithViewBag)、ViewData (WithViewData) ...

```csharp
[Fact]
public void Test()
{
    new HomeController()
        .AsTester()
        .Action(c => c.Index())
        .ShouldBeViewResult();
        .WithModel(new { id = 1 });
        .WithViewBag("viewbag", 123)
        .WithViewData("viewdata", 456) 
}

```

### 目前支援的 ActionResult

相關的用法可以參考 [測試](/Tests/Baymax.Tester.Tests/UnitTest)

- AcceptedResult
- CreatedAtActionResult
- CreatedAtRoutedResult
- JsonResult
- RedirectResult
- RedirectToActionResult
- RedirectToRouteResult
- LocalRedirectResult
- PartialViewResult
- ViewResult
- StatusCodeResult
- ContentResult

