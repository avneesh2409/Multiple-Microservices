using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Models
{
    public interface IZoomServices
    {
        Task<object> CreateZoomUser(CreateRequestPayload payload);
        Task<object> GetZoomUser();
        Task<object> GetAuthorizeToken(string code, string state);
        Task<object> CreateZoomMeeting(TokenClass payload);

    }
}
