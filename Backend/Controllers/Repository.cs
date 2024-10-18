namespace Backend.Controllers
{
    public class Repository
    {
        public static List<PersonaDatos> persona = new List<PersonaDatos>()
        {
            new PersonaDatos()
            {
                ID = 1,
                Name = "Bryan",
                Age = 19,
                Email = "brrrr@gmail.com"
            },

            new PersonaDatos()
            {
                ID= 2,
                Name = "Rocio",
                Age = 19,
                Email = "hermosa@gmail.com"
            },

            new PersonaDatos()
            {
                ID = 3,
                Name = "Marbelly",
                Age = 19,
                Email = "hola@gmail.com"
            }
        };
    }
}

public class PersonaDatos
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}
