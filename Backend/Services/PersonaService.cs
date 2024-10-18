using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Services
{
    public class PersonaService : IPersonaServices
    {
        public bool Validate(PersonaDatos persona)
        {
            if (String.IsNullOrEmpty(persona.Name) || persona.Name.Length > 10) 
            {
                return false;
            }
            return true;
        }

    }
}
