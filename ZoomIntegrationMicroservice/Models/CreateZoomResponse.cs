using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Models
{
    public class CreateRequestPayload
    {
        public string Action { get; set; }
        public UserInfo User_Info { get; set; }
    }
    public class UserInfo{
        public string Email { get; set; }
        public int Type { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }
    public class UserInfoResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
        public string RoleName { get; set; }
        public string Pmi { get; set; }
        public string UsePmi { get; set; }
        public string PersonalMeetingUrl { get; set; }
        public string TimeZone { get; set; }
        public string Verified { get; set; }
        public string Dept { get; set; }
        public string CreatedAt { get; set; }
        public string LastLoginTime { get; set; }
        public string PicUrl { get; set; }
        public string HostKey { get; set; }
        public string Jid { get; set; }
        public List<string> GroupIds { get; set; }
        public List<string> ImGroupIds { get; set; }
        public string AccountId { get; set; }
        public string Language { get; set; }
        public string PhoneCountry { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
    }
}
