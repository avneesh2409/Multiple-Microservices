using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZoomIntegrationMicroservice.Models
{
    public class EmailModel
    {
        public List<string> To
        {
            get; set;
        }
        public string Subject
        {
            get; set;
        }
        public string Text
        {   
            get; set;
        }
        public string File { get; set; }
        public string Html { get; set; }
        public bool IsHtml { get; set; }
        public bool IsFile { get; set; }
        public bool IsText { get; set; }
    }
}
