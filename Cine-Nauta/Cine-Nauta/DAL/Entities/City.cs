using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class City : Entity 
    {
        [Display(Name = "Ciudad")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Estado")]
        [JsonIgnore]
        public State State { get; set; }

        //[Display(Name = "Usuarios")]
        //public ICollection<User> Users { get; set; }
    }
}
