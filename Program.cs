using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Globalization;

namespace ResstApiCallX2
{
    class Program
    {
        static void Main(string[] args)
        {
            GasPriceClient.RunAsync().GetAwaiter().GetResult();
        }


        public class GasPrice

        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Street { get; set; }
            public string Place { get; set; }
            public Decimal Lat { get; set; }
            public Decimal Lng { get; set; }
            public Decimal Dist { get; set; }
            public Decimal Price { get; set; }
            public string IsOpen { get; set; }
            public string HouseNumber { get; set; }
            public string PostCode { get; set; }
        }


        public static class GasPriceClient
        {
            static HttpClient client = new HttpClient();
            static IFormatProvider format = new CultureInfo("en-US");

            static void PrintGasPrice(GasPrice gasPrice)
            {
                Console.WriteLine($"Id: {gasPrice.Id}");
                Console.WriteLine($"Name: {gasPrice.Name}");
                Console.WriteLine($"Brand: {gasPrice.Brand}");
                Console.WriteLine($"Street: {gasPrice.Street}");
                Console.WriteLine($"HouseNumber: {gasPrice.HouseNumber}");
                Console.WriteLine($"Place: {gasPrice.Place}");
                Console.WriteLine($"PostCode: {gasPrice.PostCode}");
                Console.WriteLine($"Price: {gasPrice.Price.ToString(format)}");
                Console.WriteLine($"Lat: {gasPrice.Lat.ToString(format)}");
                Console.WriteLine($"Lng: {gasPrice.Lng.ToString(format)}");
                Console.WriteLine($"IsOpen: {gasPrice.IsOpen}");            }

            
            static async Task<GasPrice> GetGasPriceAsync(string endpoint)
            {
                GasPrice gasPrice = null;
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress.AbsoluteUri + endpoint);
                if (response.IsSuccessStatusCode)
                {
                    gasPrice = await response.Content.ReadAsAsync<GasPrice>();
                }
                return gasPrice;
            }



            public static async Task RunAsync()
            {
                client.BaseAddress = new Uri("http://localhost:4999");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                GasPrice gasPrice = await GetGasPriceAsync("/api/fuel/e5/home/cheapest");

                PrintGasPrice(gasPrice);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }


            }

        }



    }
}
