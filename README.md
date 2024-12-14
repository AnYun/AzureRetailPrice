# AnYun.Azure.RetailPrice

## Introduction  
`AnYun.Azure.RetailPrice` is an SDK for querying Azure service prices. It provides an easy-to-use API that allows developers to effortlessly retrieve pricing information for various Azure services.

## Features  
- Query prices for specific Azure services  
- Support for multiple Azure services  
- Support for regional price queries  

## Installation  
You can install `AnYun.Azure.RetailPrice` using the NuGet package manager:

```
dotnet add package AnYun.Azure.RetailPrice
```

## Usage  
Below is an example of how to use AnYun.Azure.RetailPrice:

```csharp
using AnYun.Azure.RetailPrice.Models;
using AnYun.Azure.RetailPrice;

namespace PriceTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var service = new AzureRetailPriceClient();
            string nextPageLink = null;
            var result = new List<AzureRetailPriceItem>();

            var prices = await service.GetPricesAsync(x => x.ServiceFamily == "AI + Machine Learning");
            nextPageLink = prices.NextPageLink;
            result.AddRange(prices.Items);

            while (!string.IsNullOrEmpty(nextPageLink))
            {
                prices = await service.GetPricesAsync(nextPageLink);
                result.AddRange(prices.Items);
                nextPageLink = prices.NextPageLink;
            }

            Console.WriteLine($"Total items: {result.Count}");
        }
    }
}
```

## License  
This project is licensed under the MIT License. For details, please refer to [LICENSE](LICENSE).
