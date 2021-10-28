using Core.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Tools.SmsHandler
{
    public class SMSMessageBulk
    {
        public List<string> PhoneNumbers { get; set; }

        public string Text { get; set; }
    }
}
