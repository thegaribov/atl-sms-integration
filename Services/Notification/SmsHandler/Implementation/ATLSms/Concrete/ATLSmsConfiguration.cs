using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Notification.SmsHandler.Implementation.ATLSms.Concreate
{
    public class ATLSmsConfiguration
    {
        public string API_URL { get; set; }
        public string API_LOGIN { get; set; }
        public string API_SECRET { get; set; }
        public string Sender { get; set; }

    }
}
