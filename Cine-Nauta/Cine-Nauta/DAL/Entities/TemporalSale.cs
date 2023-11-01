using System.ComponentModel.DataAnnotations;

namespace Cine_Nauta.DAL.Entities
{
    public class TemporalSale : EntityCine
    {
        //public ICollection<OrderDetail> OrderDetails { get; set; }

        public User User { get; set; }

        public Function Function { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => Function == null ? 0 : (decimal)Quantity * Function.Price;
    }
}
