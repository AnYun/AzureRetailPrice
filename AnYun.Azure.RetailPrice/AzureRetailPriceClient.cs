using AnYun.Azure.RetailPrice.Enums;
using AnYun.Azure.RetailPrice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AnYun.Azure.RetailPrice
{
    public class AzureRetailPriceClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string BaseUrl = "https://prices.azure.com/api/retail/prices";
        private const string ApiVersion = "2023-01-01-preview";
        private AzureRetailPriceResponse _response;
        public AzureRetailPriceResponse response => _response;

        public AzureRetailPriceClient()
        {

        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <returns></returns>
        public async Task GetPricesAsync()
        {
            await GetPricesAsync(null, false, null, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <returns></returns>
        public async Task GetPricesAsync(CurrencyCode currencyCode)
        {
            await GetPricesAsync(currencyCode, false, null, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="nextPageLink">NextPage Link</param>
        /// <returns></returns>
        public async Task GetPricesAsync(string nextPageLink)
        {
            await GetPricesAsync(null, false, nextPageLink, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task GetPricesAsync(Expression<Func<AzureRetailPriceQuery, bool>> filterExpression)
        {
            await GetPricesAsync(null, false, null, filterExpression);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="isPrimaryMeterRegion">Is Primary MeterRegion</param>
        /// <returns></returns>
        public async Task GetPricesAsync(bool isPrimaryMeterRegion)
        {
            await GetPricesAsync(null, isPrimaryMeterRegion, null, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task GetPricesAsync(CurrencyCode currencyCode, Expression<Func<AzureRetailPriceQuery, bool>> filterExpression)
        {
            await GetPricesAsync(currencyCode, false, null, filterExpression);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <param name="isPrimaryMeterRegion">Is Primary MeterRegion</param>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task GetPricesAsync(CurrencyCode currencyCode, bool isPrimaryMeterRegion, Expression<Func<AzureRetailPriceQuery, bool>> filterExpression)
        {
            await GetPricesAsync(currencyCode, isPrimaryMeterRegion, null, filterExpression);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <param name="isPrimaryMeterRegion">Is Primary MeterRegion</param>
        /// <param name="nextPageLink">NextPage Link</param>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task GetPricesAsync(CurrencyCode? currencyCode, bool isPrimaryMeterRegion, string nextPageLink = null, Expression<Func<AzureRetailPriceQuery, bool>> filterExpression = null)
        {
            try
            {
                var url = BuildUrl(currencyCode, isPrimaryMeterRegion, nextPageLink, filterExpression);
                _response = await _httpClient.GetFromJsonAsync<AzureRetailPriceResponse>(url);
                _response.RequestUrl = url;
            }
            catch (HttpRequestException ex)
            {
                // Handle request errors
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Build Url
        /// </summary>
        /// <param name="currencyCode"></param>
        /// <param name="isPrimaryMeterRegion"></param>
        /// <param name="nextPageLink"></param>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        private string BuildUrl(CurrencyCode? currencyCode, bool isPrimaryMeterRegion, string nextPageLink, Expression<Func<AzureRetailPriceQuery, bool>> filterExpression)
        {
            if (nextPageLink != null)
            {
                return nextPageLink;
            }

            var url = new StringBuilder($"{BaseUrl}?api-version={ApiVersion}");

            if (currencyCode != null)
            {
                url.Append($"&currencyCode={currencyCode}");
            }

            if (isPrimaryMeterRegion)
            {
                url.Append("&meterRegion='primary'");
            }

            if (filterExpression != null)
            {
                var query = new List<AzureRetailPriceQuery>().AsQueryable().Where(filterExpression);
                var filter = query.ToODataFilter();
                if (!string.IsNullOrEmpty(filter))
                {
                    url.Append($"&$filter={filter}");
                }
            }

            return url.ToString();
        }
        /// <summary>
        /// Has Next Page
        /// </summary>
        /// <returns></returns>
        public bool HasNextPage => !string.IsNullOrEmpty(_response?.NextPageLink);
        /// <summary>
        /// Get Next Page
        /// </summary>
        /// <returns></returns>
        public async Task NextPageAsync()
        {
            if (HasNextPage == false)
                _response = null;
            else
                await GetPricesAsync(_response.NextPageLink);
        }
    }
}
