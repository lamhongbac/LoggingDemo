namespace MVCWeb.Models
{
    public class TransactionViewModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; } //BeneficiaryName (ng thu huong)
        public string Bank { get; set; }
        public string SWIFTCode { get; set; }
        public int Amount { get; set; }

    }
}
