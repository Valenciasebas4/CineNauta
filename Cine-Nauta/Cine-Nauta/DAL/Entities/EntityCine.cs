using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cine_Nauta.DAL.Entities
{
    public class EntityCine
    {
        [Key]
        public virtual int Id { get; set; }

        [Display(Name = "Fecha de creación")]
        public virtual DateTime? CreatedDate { get; set; }

        [Display(Name = "Fecha de modificación")]
        public virtual DateTime? ModifiedDate { get; set; }
    }
}
