using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Models
{
    public class CreateRequestPayload
    {
        public string action { get; set; }
        public UserInfo user_info { get; set; }
        //public int UserId { get; set; }
        //public string Body { get; set; }
        //public string Title { get; set; }
    }
    public class AccessTokenResponse
    {
        public int id { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public ulong expires_in { get; set; }
        public string scope { get; set; }
    }
    public class CodeResponse {
        public int id { get; set; }
        public string state { get; set; }
        public string code { get; set; }
    }
    public class CreateResponse
    {
    public string id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public int type { get; set; }
    public string error { get; set; }
    }
    public class UserResponse {
        public List<UserInformation> users { get; set; }
    }
    public class UserInformation
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public int type { get; set; }
        public ulong pmi { get; set; }
        public string timezone { get; set; }
        public int verified { get; set; }
        public DateTime created_at { get; set; }
        public DateTime last_login_time { get; set; }
        public string pic_url { get; set; }
        public string language { get; set; }
        public string phone_number { get; set; }
        public string status { get; set; }
    }
    public class UserInfo{
        public string email { get; set; }
        public int type { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
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
    public class RequestMeeting {
        public int duration { get; set; }
        public string password { get; set; }
        public int type { get; set; }
        public string timezone { get; set; }
        public string agenda { get; set; }
        public string start_time { get; set; }
        public string topic { get; set; }
    }
    public class TokenClass
    {
    public string accesstoken { get; set; }
    public int duration { get; set; }
    public string password { get; set; }
    public int type { get; set; }
    public string timezone { get; set; }
    public string agenda { get; set; }
    public string start_time { get; set; }
    public string topic { get; set; }
}
    public class CreateMeetingResponse
    {
        public string uuid { get; set; }
        public ulong id { get; set; }
        public string host_id { get; set; }
        public string host_email { get; set; }
        public string topic { get; set; }
        public int type { get; set; }
        public string status { get; set; }
        public string timezone { get; set; }
        public string agenda { get; set; }
        public string created_at { get; set; }
        public string start_url { get; set; }
        public string join_url { get; set; }
        public string password { get; set; }
        //public List<MeetingTrackingField> tracking_fields { get; set; }
        public string h323_password { get; set; }
        public string pstn_password { get; set; }
        public string encrypted_password { get; set; }
        //public MeetingRecurrence recurrence { get; set; }
        //public MeetingSettings settings { get; set; }

    }
    public class MeetingSettings {
        public bool host_video { get; set; }
        public bool participant_video { get; set; }
        public bool cn_meeting { get; set; }
        public bool in_meeting { get; set; }
        public bool join_before_host { get; set; }
        public bool mute_upon_entry { get; set; }
        public bool watermark { get; set; }
        public bool use_pmi { get; set; }
        public int approval_type { get; set; }
        public int registration_type { get; set; }
        public string audio { get; set; }
        public string auto_recording { get; set; }
        public bool enforce_login { get; set; }
        public string enforce_login_domains { get; set; }
        public bool alternative_hosts { get; set; }
        public bool close_registration { get; set; }
        public bool registrants_confirmation_email { get; set; }
        public bool waiting_room { get; set; }
        public bool request_permission_to_unmute_participants { get; set; }
        public bool registrants_email_notification { get; set; }
        public bool meeting_authentication { get; set; }
        public List<string> global_dial_in_countries { get; set; }

    }
    public class MeetingTrackingField {
        public string field { get; set; }
        public string value { get; set; }
    }
    public class MeetingRecurrence
    {
        public int type { get; set; }
        public int repeat_interval { get; set; }
        public string weekly_days { get; set; }
        public int monthly_day { get; set; }
        public int monthly_week { get; set; }
        public int monthly_week_day { get; set; }
        public int end_times { get; set; }
        public DateTime end_date_time { get; set; }
    }
}
