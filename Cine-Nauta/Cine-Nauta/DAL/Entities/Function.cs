using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class Function : EntityCine
    {

        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        [Display(Name = "Fecha Función")]
        public DateTime FunctionDate { get; set; }

        [Display(Name = "Precio")]
        public decimal Price { get; set; }  

    }
}
