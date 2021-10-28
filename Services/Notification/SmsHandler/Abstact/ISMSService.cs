using Core.Constants.Notificaiton.SmsHandler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Notification.SmsHandler.Abstract
{
    public interface ISMSService
    {
        Task<SmsOperationResult<bool, string, string>> SendSMSAsync(string phoneNumber, string smsText);
        Task<SmsOperationResult<bool, string, string>> SendSMSAsync(SMSMessage message);
    }
}
