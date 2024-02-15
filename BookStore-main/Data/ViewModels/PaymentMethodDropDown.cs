using BookStore.Models;

namespace BookStore.Data.ViewModels
{
    public class PaymentMethodDropDown
    {
        public PaymentMethodDropDown()
        {
            PaymentMethods = new List<PaymentMethod>();
        }

        public List<PaymentMethod> PaymentMethods { get; set; }
    }
}
