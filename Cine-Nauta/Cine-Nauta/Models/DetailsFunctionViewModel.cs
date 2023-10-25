using Cine_Nauta.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Cine_Nauta.Models
{
    public class DetailsFunctionViewModel : EntityCine
    {
        [Display(Name = "Titulo")]
        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Title { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(400, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Description { get; set; }

        [Display(Name = "Director")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Director { get; set; }

        [Display(Name = "Año de lanzamiento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string LaunchYear { get; set; }

        [Display(Name = "Duracion")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Duration { get; set; }


        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int GenderId { get; set; }


        [Display(Name = "Clasificacion")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ClassificationId { get; set; }

        public Gender Gender { get; set; }
        public Classification Classification { get; set; }

        [Display(Name = "Fecha Función")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime FunctionDate { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public decimal Price { get; set; }



        [Display(Name = "Pelicula")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int MovieId { get; set; }

        [Display(Name = "Sala")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RoomId { get; set; }

        public Movie Movie { get; set; }
        public Room Room { get; set; }
    }
}
