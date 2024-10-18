using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private IPersonaServices _personaServices;

        public PersonaController([FromKeyedServices("personaservices2")] IPersonaServices personaService)
        {
            _personaServices = personaService;
        }

        [HttpGet("all")]
        public List<PersonaDatos> GetPersonaDatos() => Repository.persona;

        [HttpGet("{id}")]
        public ActionResult<PersonaDatos> GetPersonaDatos(int id) 
        {
            var persona = Repository.persona.FirstOrDefault(x => x.ID == id); 

            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }


        [HttpGet("search/{search}")]

        public ActionResult<List<PersonaDatos>> Get(string search)
        {
            var personas = Repository.persona.Where(x => x.Name.ToUpper().Contains(search.ToUpper())).ToList();

            if (personas == null)
            {
                return NotFound();
            }
            return Ok(personas);
        }

        [HttpPost]
        public IActionResult Add(PersonaDatos gente)
        {
            if (!_personaServices.Validate(gente))
            {
                return BadRequest();
            }
            Repository.persona.Add(gente);
            return NoContent();
        }
    }
}
