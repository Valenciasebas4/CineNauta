using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cine_Nauta.Helpers
{
    public interface IDropDownListHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLRoomsAsync(); //Lista de salas
        Task<IEnumerable<SelectListItem>> GetDDLSeatsAsync(); //Lista de Sillas
        Task<IEnumerable<SelectListItem>> GetDDLClassificationsAsync(); //Lista de Clasificaciones
        Task<IEnumerable<SelectListItem>> GetDDLGendersAsync(); //Lista de Generos
       // Task<IEnumerable<SelectListItem>> GetDDLMoviesAsync(); //Lista de Peliculas

        Task<IEnumerable<SelectListItem>> GetDDLCountriesAsync();

        Task<IEnumerable<SelectListItem>> GetDDLStatesAsync(Guid countryId);

        Task<IEnumerable<SelectListItem>> GetDDLCitiesAsync(Guid stateId);
    }
}
