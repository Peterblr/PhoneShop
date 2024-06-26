﻿using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PhoneShop.Client.Services
{
    public class ClientServices(HttpClient httpClient) : IProductService
    {
        private const string BaseUrl = "api/product";
        private static string SerializeObl(object modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());
        private static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;
        private static StringContent GenerateStringContent(string serialiazedObj) => new(serialiazedObj, System.Text.Encoding.UTF8, "application/json");
        private static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;

        private static JsonSerializerOptions JsonOptions()
        {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
            };
        }

        public async Task<ServiceResponse> AddProduct(Product product)
        {
            var response = await httpClient.PostAsync(BaseUrl, GenerateStringContent(SerializeObl(product)));

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse(false, "Error occured. Try again later...");
            }
            var apiResponse = await response.Content.ReadAsStringAsync();

            return DeserializeJsonString<ServiceResponse>(apiResponse);
        }

        public async Task<List<Product>> GetAllProducts(bool featuredProducts)
        {
            var response = await httpClient.GetAsync($"{BaseUrl}?featured={featuredProducts}");

            if (!response.IsSuccessStatusCode) return null!;

            var result = await response.Content.ReadAsStringAsync();
            return [.. DeserializeJsonStringList<Product>(result)];
        }
    }
}
