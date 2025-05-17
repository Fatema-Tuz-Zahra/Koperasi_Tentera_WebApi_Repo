namespace Koperasi_Tentera_WebApi.Model
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ICNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Pin { get; set; }
        public string OTPEmail { get; set; }
        public string OTPPhone { get; set; }
    }
}
