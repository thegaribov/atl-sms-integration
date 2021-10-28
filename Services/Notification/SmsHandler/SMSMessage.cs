using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Notification.SmsHandler
{
    public class SMSMessageBulk
    {
        public List<string> PhoneNumbers { get; set; }

        public string Text { get; set; }
    }
}
