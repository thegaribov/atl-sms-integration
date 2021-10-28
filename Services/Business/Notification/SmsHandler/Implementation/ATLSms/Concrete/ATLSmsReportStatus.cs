using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Tools.SmsHandler.Implementation.ATLSms.Concreate
{
    public class ATLSmsReportStatus
    {
        private Dictionary<string, string> Statuses { get; set; }

        public ATLSmsReportStatus()
        {
            Statuses = new Dictionary<string, string>
            {
                { "1", "Message is queued"},
                { "2", "Message was successfully delivered"},
                { "3", "Message delivery failed"},
                { "4", "Message was removed from list"},
                { "5", "System error"},
            };
        }

        public string GetStatusByCode(string code)
        {
            return Statuses.GetValueOrDefault(code);
        }
    }
}
