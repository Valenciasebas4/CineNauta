using Cine_Nauta.DAL;
using Cine_Nauta.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cine_Nauta.Services
{
    public class DropDownListHelper : IDropDownListHelper
    {

        public readonly DataBaseContext _context;

        public DropDownListHelper(DataBaseContext context)
        {
            _context = context;
        }

        

        public async Task<IEnumerable<SelectListItem>> GetDDLRoomsAsync()
        {
            List<SelectListItem> listRooms = await _context.Rooms
                .Select(c => new SelectListItem
                {
                    Text = c.NumberRoom,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listRooms.Insert(0, new SelectListItem
            {
                Text = "Seleccione una Sala...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listRooms;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLSeatsAsync()
        {
            List<SelectListItem> listSeats = await _context.Seats
               .Select(c => new SelectListItem
               {
                   Text = c.NumberSeat,
                   Value = c.Id.ToString(),
               })
               .OrderBy(c => c.Text)
               .ToListAsync();

            listSeats.Insert(0, new SelectListItem
            {
                Text = "Seleccione una Silla...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listSeats;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLClassificationsAsync()
        {
            List<SelectListItem> listClassifications = await _context.Classifications
               .Select(c => new SelectListItem
               {
                   Text = c.ClassificationName,
                   Value = c.Id.ToString(),
               })
               .OrderBy(c => c.Text)
               .ToListAsync();

            listClassifications.Insert(0, new SelectListItem
            {
                Text = "Seleccione una Clasificación...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listClassifications;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLGendersAsync()
        {
            List<SelectListItem> listGenders = await _context.Genders
              .Select(c => new SelectListItem
              {
                  Text = c.GenderName,
                  Value = c.Id.ToString(),
              })
              .OrderBy(c => c.Text)
              .ToListAsync();

            listGenders.Insert(0, new SelectListItem
            {
                Text = "Seleccione un Genero...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listGenders;
        }

       

        public async Task<IEnumerable<SelectListItem>> GetDDLCountriesAsync()
        {
            List<SelectListItem> listCountries = await _context.Countries
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCountries.Insert(0, new SelectListItem
            {
                Text = "Seleccione un país...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listCountries;
        }


        public async Task<IEnumerable<SelectListItem>> GetDDLStatesAsync(Guid countryId)
        {
            List<SelectListItem> listStates = await _context.States
                .Where(s => s.Country.Id == countryId)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString(),
                })
                .OrderBy(s => s.Text)
                .ToListAsync();

            listStates.Insert(0, new SelectListItem
            {
                Text = "Seleccione un estado...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listStates;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLCitiesAsync(Guid stateId)
        {
            List<SelectListItem> listCities = await _context.Cities
                .Where(c => c.State.Id == stateId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listCities.Insert(0, new SelectListItem
            {
                Text = "Seleccione una ciudad...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listCities;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLMoviesAsync()
        {
            List<SelectListItem> listMovies = await _context.Movies
              .Select(c => new SelectListItem
              {
                  Text = c.Title,
                  Value = c.Id.ToString(),
              })
              .OrderBy(c => c.Text)
              .ToListAsync();

            listMovies.Insert(0, new SelectListItem
            {
                Text = "Seleccione una Pelicula...",
                Value = 0.ToString(),
                Selected = true
            });

            return listMovies;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLMoviesAsync(int movieId)
        {
            List<SelectListItem> listMovies = await _context.Movies
                //.Where(s => s.Country.Id == movieId)
                .Select(s => new SelectListItem
                {
                    Text = s.Title,
                    Value = s.Id.ToString(),
                })
                .OrderBy(s => s.Text)
                .ToListAsync();

            listMovies.Insert(0, new SelectListItem
            {
                Text = "Seleccione una pelicula...",
                Value = 0.ToString(),
                Selected = true
            });

            return listMovies;
        }

        public async Task<IEnumerable<SelectListItem>> GetDDLRoomsAsync(int roomId)
        {
            List<SelectListItem> listRooms = await _context.Rooms
                //.Where(s => s.Country.Id == movieId)
                .Select(s => new SelectListItem
                {
                    Text = s.NumberRoom,
                    Value = s.Id.ToString(),
                })
                .OrderBy(s => s.Text)
                .ToListAsync();

            listRooms.Insert(0, new SelectListItem
            {
                Text = "Seleccione una sala...",
                Value = 0.ToString(),
                Selected = true
            });

            return listRooms;
        }
    }
}
