namespace Kiosk_App.Models
{
    public class BillPayment
    {
        public int PaymentId { get; set; }
        public string Amount { get; set; }
        public string Biller { get; set; }
        public string DateTime { get; set; }
    }
}