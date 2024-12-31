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
            var client = new AzureRetailPriceClient();
            var result = new List<AzureRetailPriceItem>();

            await client.GetPricesAsync(x => x.ServiceFamily == "AI + Machine Learning");
            result.AddRange(client.response.Items);

            while (client.HasNextPage)
            {
                await client.NextPageAsync();
                result.AddRange(client.response.Items);
            }

            Console.WriteLine($"Total items: {result.Count}");
        }
    }
}
```

## License  
This project is licensed under the MIT License. For details, please refer to [LICENSE](LICENSE.txt).
