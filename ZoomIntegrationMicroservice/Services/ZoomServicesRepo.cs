using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZoomIntegrationMicroservice.Models;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ZoomIntegrationMicroservice.Services
{
    public class ZoomServicesRepo : IZoomServices
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;

        public ZoomServicesRepo(IHttpClientFactory httpClient,IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<object> CreateZoomUser(CreateRequestPayload payload)
        {
            //var request = new HttpRequestMessage(HttpMethod.Post,"" );
            //request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Content-Type", "application/json");
            //request.Headers.Add("Authorization", $"Bearer {_config["ZoomCredentials:ZOOM_JWT_TOKEN"]}");
            var client = _httpClient.CreateClient("zoom");
            //var userPayload = new StringContent(
            //                        JsonSerializer.Serialize(payload),
            //                        Encoding.UTF8,
            //                        "application/json");

            //using var response = await client.PostAsync("/", userPayload);
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            var userItemJson = new StringContent(
                        JsonSerializer.Serialize(payload, jsonSerializerOptions),
                        Encoding.UTF8);

            using var response =  await client.PostAsync("/users", userItemJson);
            response.EnsureSuccessStatusCode();
            //var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                var Branches = JsonSerializer.Serialize(responseStream);
                return Branches;
            }
            else
            {
                return null;
            }
        }
    }
}
