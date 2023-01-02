# ORM (Object Relational Mapping) Nedir? ORM Araçları Nelerdir?

## ORM (Object Relational Mapping)

İlişkisel veri tabanı ile uygulama içerisinde kullandığımız modelleri/nesneleri birbirine bağlama tekniğidir. Db objelerinin kod tarafında bir replikası bir yansıması var gibi düşünebilirsiniz. ORM bu mapleme tekniğinin adıdır. ORM'i uygulamak için kullandığımız yazılımlara da ORM Araçları diyoruz. ORM araçları ilişkisel veritabanları ve uygulama arasındaki `köprüdür`.

En çok kullanılan ORM araçları şu şekildedir.

* Entity Framework
* Entity Framework Core
* Dapper
* nHibernate

```
ORM Araçlarının Avantajları:

Database teknolojisine olan bağımlılığını ortadan kaldırır. Uygulama sadece ORM'i bilir. Database hakkında fikri yoktur. Bu da her yazılımcının sevdiği bir özelliktir. :)
SQL/TSQL/PLSQL bilmeden çok kısa bir zamanda db işlemlerini çok daha az kod yazarak yapabilirsiniz.
Nesne yönelimli kod yazmayı destekler.
ORM Araçlarının çoğu açık kaynak kodludur.
```
```
ORM Araçlarının Dezavantajları:

Performans sorunları yaratabilir. DB'ye bağlanıp sql çalıştırmak her zaman için daha performanslıdır.
Orm araçlarının oluşturduğu sql lere müdahale edemezsiniz. Kontrolü developer'dan alır.
Orm aracını öğrenmek için de zamana ihtiyacınız vardır.
```

```
ORM modelleme yaklaşımları 3'e ayrılır:

DB First Yaklaşım
Code First Yaklaşım
Model First Yaklaşım
```
---
---
---

## Entity Framework Core

Entity Framework Microsoft'un ORM aracı olarak sunduğu Data Access teknolojisidir. Entity Framework yıllar içerisinde olgunlaşarak EF 6.x versiyonuna ulaştıktan sonra yerini Entity Framework Core 'a bıraktı. Entity Framework Core ise cross platform ve open source bir teknoloji.

Entity Framework .Net Core uygulamalarda kullanılmak üzere tasarlandı. Ama .Net Framework 4.5+ versiyonuyla yazılmış uygulamalarda da kullanılabilir durumdadır.

### Bir Projeye Entity Framework Core Nasıl Eklenir?

Bir .Net Core WebApi projesinde Ef Core kullanabilmek için öncelikler gerekli paketleri projeye dahil etmeliyiz. Dotnet'in paket yöneticisi `Nuget Package Manager`'dır.Localde çalışma yaparken gerçek bir veri tabanı ile çalışmak maliyetli olabilir. Bunun yerine hem implementasyonu kolay olan hem de hızlı çalışan InMemory database kullanılması önerilir. Ef Core'un tüm özelliklerini in memory database implemente ederek kullanabiliriz. BookStore uygulamamıza da In Memory database implemente ederek EF Core u kullanıcaz.

Projeyi In Memoery EF Core ile çalışır hale getirmek için izlememiz gereken adımlar şu şekildedir arkadaşlar.

1. Projeye Microsoft.EntityFrameworkCore'nin eklenmesi

* WebApi proje dizininde aşağıdaki komutu çalıştırınız.
* `dotnet add package Microsoft.EntityFrameworkCore --version 5.0.6`

2. Projeye Microsoft.EntityFrameworkCore.InMemory'nin eklenmesi.

* WebApi proje dizininde aşağıdaki komutu çalıştırınız.
* `dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 5.0.6`

3. Db operasyonları için kullanılacak olan DB Context'i yaratılması

```
public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {}
    public DbSet<Book> Books { get; set; }
  
}
```
4. Initial Data için bir Data Generator'ın yazılması

```
public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            // Look for any book.
            if (context.Books.Any())
            {
                return;   // Data was already seeded
            }

            context.Books.AddRange(
               new Book()
               {
                   Title = "Lean Startup",
                   GenreId = (int)GenreEnum.PersonalGrowth, // Personal Growth
                   PageCount = 200,
                   PublishDate = new DateTime(2001, 06, 12)
               });

            context.SaveChanges();
        }
    }
}
```

5. Uygulama ayağa kalktığından initial datanın in memory DB'ye yazılması için Program.cs içerisinde configurasyon yapılması

```
public static void Main(string[] args)
        {
            //1. Get the IWebHost which will host this application.
            var host = CreateHostBuilder(args).Build();

            //2. Find the service layer within our scope.
            using (var scope = host.Services.CreateScope())
            {
                //3. Get the instance of BoardGamesDBContext in our services layer
                var services = scope.ServiceProvider;
                //4. Call the DataGenerator to create sample data
                DataGenerator.Initialize(services);
            }

            //Continue to run the application
            host.Run();
        }
```

6. Startup.cs içerisinde ConfigureServices() içerisinde DbContext'in servis olarak eklenmesi

```
services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
```

6. Kullanmak istediğiniz yerde _context'i kurucu metot aracılığıyla ekleyerek kullabilirsiniz :)

```
readonly BookStoreDbContext _context;
public BookController(BookStoreDbContext context)
{
    _context = context;
}
```