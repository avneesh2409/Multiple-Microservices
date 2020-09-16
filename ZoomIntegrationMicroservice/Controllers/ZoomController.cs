using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZoomIntegrationMicroservice.Models;

namespace ZoomIntegrationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoomController : ControllerBase
    {
        private readonly IZoomServices _httpClient;

        public ZoomController(IZoomServices httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpPost]
        [Route("create-user")]
        public async Task<object> CreateZoomUser(CreateRequestPayload payload) {
            var response =await _httpClient.CreateZoomUser(payload);

            return response;
        }
    }
}
