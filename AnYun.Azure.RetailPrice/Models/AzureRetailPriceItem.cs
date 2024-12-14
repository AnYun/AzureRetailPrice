using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnYun.Azure.RetailPrice.Models
{
    public class AzureRetailPriceItem
    {
        public string CurrencyCode { get; set; }
        public double TierMinimumUnits { get; set; }
        public string ReservationTerm { get; set; }
        public double RetailPrice { get; set; }
        public double UnitPrice { get; set; }
        public string ArmRegionName { get; set; }
        public string Location { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public string MeterId { get; set; }
        public string MeterName { get; set; }
        public string ProductId { get; set; }
        public string SkuId { get; set; }
        public string AvailabilityId { get; set; }
        public string ProductName { get; set; }
        public string SkuName { get; set; }
        public string ServiceName { get; set; }
        public string ServiceId { get; set; }
        public string ServiceFamily { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Type { get; set; }
        public bool IsPrimaryMeterRegion { get; set; }
        public string ArmSkuName { get; set; }
        public List<SavingsPlan> SavingsPlan { get; set; }
    }
    public class SavingsPlan
    {
        public double UnitPrice { get; set; }
        public double RetailPrice { get; set; }
        public string Term { get; set; }
    }

    public class AzureRetailPriceResponse
    {
        public string RequestUrl { get; set; }
        public string BillingCurrency { get; set; }
        public string CustomerEntityId { get; set; }
        public string CustomerEntityType { get; set; }
        public List<AzureRetailPriceItem> Items { get; set; }
        public string NextPageLink { get; set; }
        public int Count { get; set; }
    }
}
