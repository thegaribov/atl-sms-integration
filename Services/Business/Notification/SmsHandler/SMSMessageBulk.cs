using Core.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Tools.SmsHandler
{
    public class SMSMessage
    {
        public SMSMessage(string phoneNumber, string text)
        {
            this.PhoneNumber = phoneNumber;
            this.Text = text;
        }

        public string PhoneNumber { get; set; }

        public string Text { get; set; }

    }
}
