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
using System.Net.Http.Headers;

namespace ZoomIntegrationMicroservice.Services
{
    public class ZoomServicesRepo : IZoomServices
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _config;
        private readonly IAccessToken _tokens;

        public ZoomServicesRepo(IHttpClientFactory httpClient,IConfiguration config,IAccessToken tokens)
        {
            _httpClient = httpClient;
            _config = config;
            _tokens = tokens;
        }
        public async Task<object> GetZoomUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.zoom.us/v2/users");
            var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",_config["ZoomCredentials:ZOOM_JWT_TOKEN"]);

           var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
               var responseStream = await response.Content.ReadAsStreamAsync();
                var Branches = JsonSerializer.DeserializeAsync<UserResponse>(responseStream);
                return Branches;
            }
            else
            {
                return null;
            }
        }
        public async Task<object> CreateZoomUser(CreateRequestPayload payload)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.zoom.us/v2/users");
            request.Content = new  StringContent(JsonSerializer.Serialize(payload));
            request.Content.Headers.ContentType =  new MediaTypeWithQualityHeaderValue("application/json");

            var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config["ZoomCredentials:ZOOM_JWT_TOKEN"]);


            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var Branches = JsonSerializer.DeserializeAsync<CreateResponse>(responseStream);
                return Branches;
            }
            else
            {
                return null;
            }

        }

        public async Task<object> CreateZoomMeeting(TokenClass payload) {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.zoom.us/v2/users/me/meetings");
            request.Content = new StringContent(JsonSerializer.Serialize(new RequestMeeting{
                agenda = payload.agenda,
                start_time=payload.start_time,
                timezone=payload.timezone,
                topic=payload.topic,
                type=payload.type,
                duration=payload.duration,
                password=payload.password
            }));
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", payload.accesstoken);


            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var Branches = JsonSerializer.DeserializeAsync<CreateMeetingResponse>(responseStream);
                return Branches;
            }
            else
            {
                return null;
            }
        }
    public async Task<object> GetAuthorizeToken(string code,string state) {
            string grant_type = "authorization_code";
            //string redirect_uri = "https://localhost:44377/api/zoom/redirect";
            string redirect_uri = "http://localhost:3000/zoom";
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://zoom.us/oauth/token?grant_type={grant_type}&code={code}&redirect_uri={redirect_uri}");
            var client = _httpClient.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", CreateAuthToken());
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var Branches = JsonSerializer.DeserializeAsync<AccessTokenResponse>(responseStream);
                bool result = _tokens.AddToken(Branches.Result);
                if (result)
                {
                    return Branches.Result;
                }
                else {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        private string CreateAuthToken() {
            string txt = _config["ZoomCredentials:ZOOM_OAUTH_CLIENT_ID"] +":"+ _config["ZoomCredentials:ZOOM_OAUTH_CLIENT_SECRET"];
            string decodedTxt = Convert.ToBase64String(Encoding.UTF8.GetBytes(txt));
            //string decodedTxt2 = System.Text.Encoding.Unicode.GetString(decodedBytes);
            return decodedTxt;
        }
    }
}
