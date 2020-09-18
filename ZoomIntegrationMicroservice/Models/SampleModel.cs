using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Models
{
    public class SampleModel
    {
        public List<UserTestInfo> result { get; set; }
    }
    public class UserTestInfo {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }

    }
}
