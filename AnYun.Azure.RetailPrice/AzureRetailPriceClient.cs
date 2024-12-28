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
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://prices.azure.com/api/retail/prices?api-version=2023-01-01-preview";
        private string _nextPageLink;

        public AzureRetailPriceClient()
        {
            _httpClient = new HttpClient();
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync()
        {
            return await GetPricesAsync(null, false, null, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync(CurrencyCode currencyCode)
        {
            return await GetPricesAsync(currencyCode, false, null, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="nextPageLink">NextPage Link</param>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync(string nextPageLink)
        {
            return await GetPricesAsync(null, false, nextPageLink, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync(Expression<Func<AzureRetailPriceQuery, bool>> filterExpression)
        {
            return await GetPricesAsync(null, false, null, filterExpression);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="isPrimaryMeterRegion">Is Primary MeterRegion</param>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync(bool isPrimaryMeterRegion)
        {
            return await GetPricesAsync(null, isPrimaryMeterRegion, null, null);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync(CurrencyCode currencyCode, Expression<Func<AzureRetailPriceQuery, bool>> filterExpression)
        {
            return await GetPricesAsync(currencyCode, false, null, filterExpression);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <param name="isPrimaryMeterRegion">Is Primary MeterRegion</param>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync(CurrencyCode currencyCode, bool isPrimaryMeterRegion, Expression<Func<AzureRetailPriceQuery, bool>> filterExpression)
        {
            return await GetPricesAsync(currencyCode, isPrimaryMeterRegion, null, filterExpression);
        }
        /// <summary>
        /// Get Azure Retail Prices
        /// </summary>
        /// <param name="currencyCode">Currency Code</param>
        /// <param name="isPrimaryMeterRegion">Is Primary MeterRegion</param>
        /// <param name="nextPageLink">NextPage Link</param>
        /// <param name="filterExpression">filterExpression</param>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetPricesAsync(CurrencyCode? currencyCode, bool isPrimaryMeterRegion, string nextPageLink = null, Expression<Func<AzureRetailPriceQuery, bool>> filterExpression = null)
        {
            var url = "";

            if (nextPageLink == null)
            {
                url = _baseUrl;

                if (currencyCode != null) url += $"&currencyCode={currencyCode}";

                if (isPrimaryMeterRegion) url += $"&meterRegion='primary'";

                var query = new List<AzureRetailPriceQuery>().AsQueryable();

                if (filterExpression != null) query = query.Where(filterExpression);

                var filter = query.ToODataFilter();
                if (!string.IsNullOrEmpty(filter)) url += $"&$filter={filter}";
            }
            else
            {
                url = nextPageLink;
            }

            var response = await _httpClient.GetFromJsonAsync<AzureRetailPriceResponse>(url);
            response.RequestUrl = url;
            _nextPageLink = response.NextPageLink;
            return response;
        }
        /// <summary>
        /// Has Next Page
        /// </summary>
        /// <returns></returns>
        public bool HasNextPage()
        {
            return !string.IsNullOrEmpty(_nextPageLink);
        }
        /// <summary>
        /// Get Next Page
        /// </summary>
        /// <returns></returns>
        public async Task<AzureRetailPriceResponse> GetNextPageAsync()
        {
            if (string.IsNullOrEmpty(_nextPageLink)) return null;
            return await GetPricesAsync(_nextPageLink);
        }
    }
}
