# Controller Nedir ?

Benzer eylemleri tanımlamak ve gruplamak için kullanılır. Rest servis mimarisindeki resource'ların karşılığıdır. Benzer eylemlerin bir arada olması toplu halde yönetilebilmeleri için önemli. Yani bir controller içindeki eylemlere erişimi toplu halde yönetmek, erişimi kısıtlamak isteyebilirsiniz. Bu nedenle api tasarımı yaparken controller ları doğru tasarlamak ve eylemlerini gruplamak önemlidir.

Controller sınıfları ControllerBase sınıfından kalıtım alır. Aşağıda örnek bir kontroller sınıfı görebilirsiniz.

Örnek Controller:

```
    [ApiController]
    [Route("api/[controller]s")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
           var obj = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return obj;
        }
    }
```

[ApiController] Attribute: Controller eylemlerinin bir Http response döneceğini taahhüt eden attribute dur.

# Route Kavramı

Route Controller'a tanımlanan bir attribute dur. Web Api' a gelen istekleri Route niteliği ile yönlendirebiliriz. Controller içerisinde spesifik bir resource ile ilgili eylemler bulunur. Bu eylemlere de URL içerisinde ortak bir grup ile ulaşılması daha doğru olur. Bunu da Route attribute'u controller bazında tanımlanarak gerçekleştirir. WebApi'a gelen requestlerin hangi controller tarafından karşılanacağı bu route niteliklerine göre belirlenir.