using System;
using System.Collections.Generic;
using System.Text;

namespace MadPay24.Common.ErrorAndMessage
{
    public class ReturnMessage
    {
        public bool Status { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        
        
    }
}
