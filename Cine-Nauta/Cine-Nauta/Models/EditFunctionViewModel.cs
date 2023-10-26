using Cine_Nauta.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.Models
{
    public class EditFunctionViewModel : EntityCine
    {
        [Display(Name = "Fecha Función")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime FunctionDate { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Price { get; set; }


        [Display(Name = "Pelicula")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int MovieId { get; set; }
      

        [Display(Name = "Sala")]         
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RoomId { get; set; }
        public IEnumerable<SelectListItem> Rooms { get; set; }
        public IEnumerable<SelectListItem> Movies { get; internal set; }
    }
}
