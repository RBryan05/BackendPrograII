using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomControlLer : ControllerBase
    {
        private IRandomService _singleton;
        private IRandomService _scope;
        private IRandomService _transient;

        private IRandomService _singleton2;
        private IRandomService _scope2;
        private IRandomService _transient2;

        public RandomControlLer(
            [FromKeyedServices("randomSingleton")] IRandomService randomSingleton,
            [FromKeyedServices("randomScoped")] IRandomService randomScope,
            [FromKeyedServices("randomTransient")] IRandomService randomTransient,
            [FromKeyedServices("randomSingleton")] IRandomService randomSingleton2,
            [FromKeyedServices("randomScoped")] IRandomService randomScope2,
            [FromKeyedServices("randomTransient")] IRandomService randomTransient2
            )
        {
            _singleton = randomSingleton;
            _scope = randomScope;
            _transient = randomTransient;

            _singleton2 = randomSingleton2;
            _scope2 = randomScope2;
            _transient2 = randomTransient2;

        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();
            result.Add("Singleton 1", _singleton.Value);
            result.Add("Scope 1", _scope.Value);
            result.Add("Transient 1", _transient.Value);

            result.Add("Singleton 2", _singleton2.Value);
            result.Add("Scope 2", _scope2.Value);
            result.Add("Transient 2", _transient2.Value);

            return result;
        }
    }
}
