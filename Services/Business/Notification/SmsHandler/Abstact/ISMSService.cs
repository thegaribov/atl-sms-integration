using Core.Constants.Notificaiton.SmsHandler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tools.SmsHandler.Abstract
{
    public interface ISMSService
    {
        Task<SmsOperationResult<bool, string, string>> SendSMS(SMSMessage message);
    }
}
