
using Cine_Nauta.DAL.Entities;

namespace Cine_Nauta.Models
{
    public class CityViewModel : City
    {
        public Guid StateId { get; set; }
    }
}
