namespace EverythingSucks.Services
{
    public class ExchangeRate
    {
        public string CurrencyCode { get; set; }

        public decimal Value { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public ExchangeRate()
        {
            Value = 1.0m;
        }

        public override string ToString()
        {
            return $"{CurrencyCode} : {Value}";
        }
    }
}
