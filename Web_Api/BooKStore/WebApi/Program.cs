using System.Reflection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase("BookStoreDB"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// UYGULAMA BAŞLAMADAN ÖNCE PROVİDER OLUŞTURDUK VE GENERATOR SINIFIMIZIN METODUNA VERDİK
using(var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

app.Run(async context => System.Console.WriteLine("Middleware 1."));

app.Use(async(context,next) => {
    System.Console.WriteLine("Middleware 1 başladı.");
    await next.Invoke();
    System.Console.WriteLine("Middleware 1 sonlandırılıyor...");
});

app.Use(async(context,next) => {
    System.Console.WriteLine("Middleware 2 başladı.");
    await next.Invoke();
    System.Console.WriteLine("Middleware 2 sonlandırılıyor...");
});

app.Use(async(context,next) => {
    System.Console.WriteLine("Middleware 3 başladı.");
    await next.Invoke();
    System.Console.WriteLine("Middleware 3 sonlandırılıyor...");
});