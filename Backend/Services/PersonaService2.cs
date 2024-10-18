using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Services
{
    public class PersonaService2 : IPersonaServices
    {
        public bool Validate(PersonaDatos persona)
        {
            if (String.IsNullOrEmpty(persona.Name) || persona.Name.Length > 100 || persona.Name.Length < 3) 
            {
                return false;
            }
            return true;
        }

    }
}
