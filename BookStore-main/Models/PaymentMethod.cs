using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            ShippingInfos = new HashSet<ShippingInfo>();
        }

        [Key]
        public int Id { get; set; }
        [Display(Name = "Način plaćanja")]
        public string Name { get; set; }
        public ICollection<ShippingInfo> ShippingInfos { get; set; }
    }
}
