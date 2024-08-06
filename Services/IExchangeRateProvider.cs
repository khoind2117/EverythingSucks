namespace EverythingSucks.Services
{
    public interface IExchangeRateProvider
    {
        Task<IList<ExchangeRate>> GetCurrencyLiveRatesAsync(string exchangeRateCurrencyCode);
        Task<decimal> GetVndToUsdRateAsync();
    }
}
