using System.Collections.Generic;

namespace Kiosk_App.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public List<BillPayment> BillPayments { get; set; }

        public Customer()
        {
            BillPayments = new List<BillPayment>();
        }
    }
}