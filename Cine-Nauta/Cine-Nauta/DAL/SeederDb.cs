using Cine_Nauta.DAL.Entities;
using Cine_Nauta.Emun;
using Cine_Nauta.Helpers;

namespace Cine_Nauta.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;


        public SeederDb(DataBaseContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
           
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateCountriesStatesCitiesAsync();
            await PopulateGenderAsync();
            await PopulateClassificationAsync();
            await PopulateRoomSeatAsync();
            await PopulateRolesAsync();
            await PopulateUserAsync("Sebastian", "Londoño", "sebas@yopmail.com", "3142393101", "Barbosa", "1035234145",  UserType.Admin);
            await PopulateUserAsync("Jessica", "Gomez", "jess@yopmail.com", "3188955943", "Barbosa", "1035232261", UserType.User);


            await _context.SaveChangesAsync();

        }


        private async Task PopulateRolesAsync()
        {
            await _userHelper.AddRoleAsync(UserType.Admin.ToString());
            await _userHelper.AddRoleAsync(UserType.User.ToString());
        }


        private async Task PopulateUserAsync(string firstName, string lastName, string email, string phone, string address, string document,  UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                

                user = new User
                {
                    CreatedDate = DateTime.Now,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                    
                };

                await _userHelper.AddUserAsync(user, "123456"); //se establece contraseña para el usuario
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());//agrega el usuario con el rol
            }
        }


        private async Task PopulateGenderAsync()
        {
            if (!_context.Genders.Any())
            {
                _context.Genders.Add(new Gender { GenderName = "Acción", Description = "Películas llenas de emocionantes escenas de acción.", CreatedDate = DateTime.Now });
                _context.Genders.Add(new Gender { GenderName = "Comedia", Description = "Películas divertidas y humorísticas.", CreatedDate = DateTime.Now });
                _context.Genders.Add(new Gender { GenderName = "Drama", Description = "Películas con un enfoque en la narrativa y las emociones.", CreatedDate = DateTime.Now });
                _context.Genders.Add(new Gender { GenderName = "Ciencia Ficción", Description = "Películas que exploran mundos futuristas y tecnología avanzada.", CreatedDate = DateTime.Now });
                _context.Genders.Add(new Gender { GenderName = "Animación", Description = "Películas animadas para todas las edades.", CreatedDate = DateTime.Now });

            }

        }

        private async Task PopulateClassificationAsync()
        {
            if (!_context.Classifications.Any())
            {
                _context.Classifications.Add(new Classification { ClassificationName = "G", Description = "Apto para todos los públicos: Las películas con esta clasificación son adecuadas para todo tipo de audiencia, incluidos niños. No contienen contenido violento, sexual, lenguaje fuerte ni temáticas perturbadoras.", CreatedDate = DateTime.Now });
                _context.Classifications.Add(new Classification { ClassificationName = "PG", Description = "Supervisión de los padres recomendada: Las películas con esta clasificación pueden contener algún contenido que no sea apropiado para niños pequeños. Los padres pueden considerar adecuado ver estas películas con sus hijos más jóvenes.", CreatedDate = DateTime.Now });
                _context.Classifications.Add(new Classification { ClassificationName = "PG-13", Description = "Se recomienda la orientación de los padres para menores de 13 años: Las películas PG-13 pueden contener un material más fuerte que las películas PG, como violencia moderada, lenguaje fuerte, temáticas más intensas y situaciones sugerentes. No son apropiadas para niños menores de 13 años sin la orientación de los padres.", CreatedDate = DateTime.Now });
                _context.Classifications.Add(new Classification { ClassificationName = "R", Description = "Restringida: Las películas con clasificación R son apropiadas solo para adultos o para adolescentes con la orientación de un adulto. Pueden contener violencia intensa, contenido sexual explícito, lenguaje fuerte y/o consumo de drogas.", CreatedDate = DateTime.Now });
                _context.Classifications.Add(new Classification { ClassificationName = "NC-17 o X", Description = "Solo para adultos: Las películas con clasificación NC-17 (o X en algunos sistemas de clasificación) están destinadas exclusivamente para adultos. Pueden contener contenido sexual explícito, violencia extrema u otras temáticas adultas.", CreatedDate = DateTime.Now });
                _context.Classifications.Add(new Classification { ClassificationName = "NR o Sin clasificación", Description = "Algunas películas no reciben una clasificación oficial, lo que significa que no han sido evaluadas por una junta de clasificación. Esto puede deberse a razones varias, como películas independientes o lanzamientos en línea que no pasaron por el proceso de clasificación.", CreatedDate = DateTime.Now });


            }

        }

        private async Task PopulateCountriesStatesCitiesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(
                    new Country
                    {
                        Name = "Colombia",
                        CreatedDate = DateTime.Now,
                        States = new List<State>()
                        {
                    new State
                    {
                        Name = "Antioquia",
                        CreatedDate = DateTime.Now,
                        Cities = new List<City>()
                        {
                            new City { Name = "Medellín", CreatedDate = DateTime.Now },
                            new City { Name = "Bello", CreatedDate = DateTime.Now },
                            new City { Name = "Itagüí", CreatedDate = DateTime.Now },
                            new City { Name = "Sabaneta", CreatedDate = DateTime.Now },
                            new City { Name = "Envigado", CreatedDate = DateTime.Now },
                            new City { Name = "Copacabana", CreatedDate = DateTime.Now },
                            new City { Name = "Barbosa", CreatedDate = DateTime.Now },
                            new City { Name = "Girardota", CreatedDate = DateTime.Now },
                        }
                    },

                    new State
                    {
                        Name = "Cundinamarca",
                        CreatedDate = DateTime.Now,
                        Cities = new List<City>()
                        {
                            new City { Name = "Bogotá", CreatedDate = DateTime.Now },
                            new City { Name = "Fusagasugá", CreatedDate = DateTime.Now },
                            new City { Name = "Funza", CreatedDate = DateTime.Now },
                            new City { Name = "Sopó", CreatedDate = DateTime.Now },
                            new City { Name = "Chía", CreatedDate = DateTime.Now },
                        }
                    },

                    new State
                    {
                        Name = "Atlántico",
                        CreatedDate = DateTime.Now,
                        Cities = new List<City>()
                        {
                            new City { Name = "Barranquilla", CreatedDate = DateTime.Now },
                            new City { Name = "La Chinita", CreatedDate = DateTime.Now },
                        }
                    },
                        }
                    });
                _context.Countries.Add(
               new Country
               {
                   Name = "Argentina",
                   CreatedDate = DateTime.Now,
                   States = new List<State>()
                   {
                        new State
                        {
                            Name = "Buenos Aires",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Avellaneda", CreatedDate= DateTime.Now },
                                new City { Name = "Ezeiza", CreatedDate= DateTime.Now },
                                new City { Name = "La Boca", CreatedDate= DateTime.Now },
                                new City { Name = "Río de la Plata", CreatedDate= DateTime.Now },
                            }
                        },

                        new State
                        {
                            Name = "La Pampa",
                            CreatedDate = DateTime.Now,
                            Cities = new List<City>()
                            {
                                new City { Name = "Santa María", CreatedDate= DateTime.Now },
                                new City { Name = "Obrero", CreatedDate= DateTime.Now },
                                new City { Name = "Rosario", CreatedDate= DateTime.Now }
                            }
                        }
                   }
               });
            }
        }

        private async Task PopulateRoomSeatAsync()
        {
            if (!_context.Rooms.Any())
            {
                _context.Rooms.Add(
                    new Room
                    {
                        NumberRoom = "UNO",
                        Capacity = 30,
                        CreatedDate = DateTime.Now,
                        TypeRoom = "Sencilla",
                        Seats = new List<Seat>()
                        {

                            new Seat {  NumberSeat  = "A1", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A2", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A3", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A5", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B1", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B2", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B3", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B5", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C1", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C2", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C3", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C5", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D1", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D2", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D3", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D5", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E1", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E2", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E3", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E5", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E6", Busy = true, CreatedDate = DateTime.Now },



                        }
                    });
                _context.Rooms.Add(
               new Room
               {
                   NumberRoom = "DOS",
                   Capacity = 20,
                   CreatedDate = DateTime.Now,
                   TypeRoom = "3D",
                   Seats = new List<Seat>()
                        {

                            new Seat {  NumberSeat  = "A1", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A2", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A3", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A5", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "A6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B1", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B2", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B3", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B5", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "B6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C1", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C2", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C3", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C5", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "C6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D1", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D2", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D3", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D5", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "D6", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E1", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E2", Busy = true, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E3", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E4", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E5", Busy = false, CreatedDate = DateTime.Now },
                            new Seat {  NumberSeat  = "E6", Busy = true, CreatedDate = DateTime.Now },


                        }
               });
            }
        }

    }
}
