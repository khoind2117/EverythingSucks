using System.Globalization;
using System.Xml;

namespace EverythingSucks.Services
{
    public class FloatRatesExchangeRateProvider : IExchangeRateProvider
    {
        #region Fields

        private readonly ILogger<FloatRatesExchangeRateProvider> _logger;

        #endregion

        #region Ctor

        public FloatRatesExchangeRateProvider(ILogger<FloatRatesExchangeRateProvider> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Methods

        public async Task<IList<ExchangeRate>> GetCurrencyLiveRatesAsync(string exchangeRateCurrencyCode)
        {
            if (string.IsNullOrEmpty(exchangeRateCurrencyCode))
                throw new ArgumentNullException(nameof(exchangeRateCurrencyCode));

            var ratesToUsd = new List<ExchangeRate>();

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync("https://www.floatrates.com/daily/usd.xml");

                // Load XML document
                var document = new XmlDocument();
                document.LoadXml(response);

                // Get exchange rates
                var itemNodes = document.SelectNodes("//item");

                foreach (XmlNode item in itemNodes)
                {
                    var currencyCode = item.SelectSingleNode("targetCurrency")?.InnerText;
                    if (string.IsNullOrEmpty(currencyCode)) continue;

                    if (!decimal.TryParse(item.SelectSingleNode("exchangeRate")?.InnerText, NumberStyles.Currency, CultureInfo.InvariantCulture, out var exchangeRate))
                        continue;

                    ratesToUsd.Add(new ExchangeRate()
                    {
                        CurrencyCode = currencyCode,
                        Value = exchangeRate,
                        LastModifiedDate = DateTime.UtcNow
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FloatRates exchange rate provider");
            }

            // Return result for the USD
            if (exchangeRateCurrencyCode.Equals("usd", StringComparison.InvariantCultureIgnoreCase))
                return ratesToUsd;

            // Use only currencies that are supported by FloatRates
            var exchangeRateCurrency = ratesToUsd.FirstOrDefault(rate => rate.CurrencyCode.Equals(exchangeRateCurrencyCode, StringComparison.InvariantCultureIgnoreCase));
            if (exchangeRateCurrency == null)
                throw new Exception("You can use FloatRates exchange rate provider only when the primary exchange rate currency is supported by FloatRates.");

            // Return result for the selected (not USD) currency
            return ratesToUsd.Select(rate => new ExchangeRate
            {
                CurrencyCode = rate.CurrencyCode,
                Value = Math.Round(exchangeRateCurrency.Value / rate.Value, 4),
                LastModifiedDate = rate.LastModifiedDate
            }).ToList();
        }
        #endregion

        public async Task<decimal> GetVndToUsdRateAsync()
        {
            // Lấy tỷ giá từ USD sang VND
            var rates = await GetCurrencyLiveRatesAsync("usd");
            var rateToVnd = rates.FirstOrDefault(r => r.CurrencyCode.Equals("vnd", StringComparison.InvariantCultureIgnoreCase));
            if (rateToVnd == null)
                throw new Exception("USD to VND rate not found.");

            // Tính tỷ giá từ VND sang USD
            var vndToUsdRate = 1 / rateToVnd.Value;
            return vndToUsdRate;
        }
    }
}

