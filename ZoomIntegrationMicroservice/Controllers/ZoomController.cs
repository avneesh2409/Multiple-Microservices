using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using ZoomIntegrationMicroservice.Models;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using ZoomIntegrationMicroservice.Helper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IO;
using System.Text;

namespace ZoomIntegrationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ZoomController : ControllerBase
    {
        static readonly char[] padding = { '=' };
        private readonly IZoomServices _httpClient;
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;
        private readonly IAccessToken _access;

        public ZoomController(IZoomServices httpClient, IHttpClientFactory client, IConfiguration config,IAccessToken access)
        {
            _httpClient = httpClient;
            _client = client;
            _config = config;
            _access = access;
        }

        #region Zoom Integrarion
        [HttpGet]
        [Route("get-user")]
        public async Task<object> GetZoomUser() {
            var response = await _httpClient.GetZoomUser();
            return response;
        }
        [HttpPost]
        [Route("create-user")]
        public async Task<object> CreateZoomUser(CreateRequestPayload payload)
        {
            var response = await _httpClient.CreateZoomUser(payload);

            return response;
        }
        [HttpGet]
        [Route("get-sign/{meetingNumber}/{role}")]
        public JsonResult GenerateSingnatureForZoom(string meetingNumber, string role)
        {
            string apiKey = _config["ZoomCredentials:ZOOM_API_KEY"];
            string apiSecret = _config["ZoomCredentials:ZOOM_SECRET_KEY"];
            string ts = (ToTimestamp(DateTime.UtcNow.ToUniversalTime()) - 30000).ToString();
            string token = GenerateToken(apiKey, apiSecret, meetingNumber, ts, role);
            return new JsonResult(new { signature = token });
        }
        private static long ToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000;
            return epoch;
        }
        private static string GenerateToken(string apiKey, string apiSecret, string meetingNumber, string ts, string role)
        {
            string message = String.Format("{0}{1}{2}{3}", apiKey, meetingNumber, ts, role);
            apiSecret = apiSecret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(apiSecret);
            byte[] messageBytesTest = encoding.GetBytes(message);
            string msgHashPreHmac = System.Convert.ToBase64String(messageBytesTest);
            byte[] messageBytes = encoding.GetBytes(msgHashPreHmac);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string msgHash = System.Convert.ToBase64String(hashmessage);
                string token = String.Format("{0}.{1}.{2}.{3}.{4}", apiKey, meetingNumber, ts, role, msgHash);
                var tokenBytes = System.Text.Encoding.UTF8.GetBytes(token);
                return System.Convert.ToBase64String(tokenBytes).TrimEnd(padding);
            }
        }
        [HttpGet]
        [Route("access-token")]
        public async Task<object> GetAccessToken(string code,string state)
        {
            //string code = _access.GetCodeToken(state).code;
            var response = await _httpClient.GetAuthorizeToken(code, state);
            return response;
        }

        [HttpGet]
        [Route("authorize")]
        public ObjectResult AuthorizeUser(string state)
        {
            string response_type = "code";
            string client_id = _config["ZoomCredentials:ZOOM_OAUTH_CLIENT_ID"];
            //string redirect_uri = "https://localhost:44377/api/zoom/redirect";
            string redirect_uri = "http://localhost:3000/zoom";
            var request = $"https://zoom.us/oauth/authorize?response_type={response_type}&client_id={client_id}&state={state}&redirect_uri={redirect_uri}";
            return new ObjectResult(new { url = request });
        }
        
        [HttpPost]
        [Route("create-meeting")]
        public async Task<object> CreateMeeting(TokenClass payload) {
            var response = await _httpClient.CreateZoomMeeting(payload);
            return response;
        }
        #endregion

        #region JWTAuthentication
        [HttpPost]
        [AllowAnonymous]
        [Route("/test")]
        public JsonResult GetResourceApi(TestModel user) {
            string token = GenerateJwtToken(user);
            return new JsonResult(new {token });
        }
        [HttpGet]
        [Route("/auth")]
        public JsonResult GetResource()
        {
            return new JsonResult(new { status = true, message = "only authenticated user can only see this" });
        }

        private string GenerateJwtToken(TestModel user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Issuer"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(1),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion

        #region CCAvenuePaymentGateway
        [HttpPost]
        public ActionResult Payment()
        {

            //var queryParameter = new CCACrypto();

            //CCACrypto is the dll you get when you download the ASP.NET 3.5 integration kit from 
            //ccavenue account.

            // return View("CcAvenue", new CcAvenueViewModel(queryParameter.Encrypt
            //(BuildCcAvenueRequestParameters(), _config["CCAvenue:CcAvenueWorkingKey"]),
            //_config["CCAvenue:CcAvenueAccessCode"]),
            // _config["CCAvenue:CcAvenueCheckoutUrl"]));
            return Ok("hello");
        }
        [HttpPost]
        [Route("payment-successful")]
        public ActionResult PaymentSuccessful(string encResp)
        {
            /*Do not change the encResp.If you change it you will not get any response.
            Decode the encResp*/
            //var decryption = new CCACrypto();
            //var decryptedParameters = decryption.Decrypt(encResp,
            //_config["CCAvenue:CcAvenueWorkingKey"]);
            return null;
            /*split the decryptedParameters by & and then by = and save your values*/
        }
        [HttpPost]
        [Route("payment-cancelled")]
        public ActionResult PaymentCanceled(string encResp)
        {
            /*Do not change the encResp.If you change it you will not get any response.
            Decode the encResp*/
            //var decryption = new CCACrypto();
            //var decryptedParameters = decryption.Decrypt(encResp,
            //_config["CCAvenue:CcAvenueWorkingKey"]);
            return null;
            /*split the decryptedParameters by & and then by = and save your values*/
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("dict")]
        public string BuildCcAvenueRequestParameters(string orderId, string amount)
        {
            var queryParameters = new Dictionary<string, string>
                                {
                                {"order_id", orderId},
                                {"merchant_id", _config["CCAvenue:CcAvenueMerchantId"]},
                                {"amount", amount},
                                {"currency","INR" },
                                {"tid",orderId },
                                {"redirect_url","/payment-successful" },
                                {"cancel_url","/payment-cancelled"},
                                {"request_type","JSON" },
                                {"response_type","JSON" },
                                {"version","1.1" }
}.Select(item => string.Format("{0}={1}", item.Key, item.Value));
            return string.Join("&", queryParameters);
        }

        #endregion

        #region FileUploadFunction
        [HttpPost]
        [AllowAnonymous]
        [Route("file-upload")]
        public async Task<JsonResult> FileUpload(List<IFormFile> files) {

            if (files.Count > 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
                Dictionary<string,string> filesPath = new Dictionary<string,string>();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                foreach (IFormFile file in files)
                {
                    string filePath = Path.Combine(path,Guid.NewGuid().ToString() + '_' + file.FileName);
                    filesPath.Add(file.FileName,"/upload/"+ Guid.NewGuid().ToString() + '_' + file.FileName);
                    FileStream fs = System.IO.File.Create(filePath);
                    await file.CopyToAsync(fs);
                }
                return new JsonResult(filesPath);
            }
            else
            {
                return new JsonResult(new { status = false, message = "unable to upload" });
            }               
        }
        #endregion
    }
}
