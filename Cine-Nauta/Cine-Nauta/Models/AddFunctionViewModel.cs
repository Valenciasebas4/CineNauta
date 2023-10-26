using Cine_Nauta.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.Models
{
    public class AddFunctionViewModel : Function
    {
        
        [Display(Name = "Pelicula")]
        public int MovieId { get; set; }
       

        [Display(Name = "Sala")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }
    }
}
