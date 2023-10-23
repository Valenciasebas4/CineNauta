using Cine_Nauta.DAL.Entities;

namespace Cine_Nauta.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;


        public SeederDb(DataBaseContext context)
        {
            _context = context;
            
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateCountriesStatesCitiesAsync();
            //await PopulateRolesAsync();


            await _context.SaveChangesAsync();

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
    }
}
