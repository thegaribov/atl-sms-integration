using Core.Constants.Notificaiton.SmsHandler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tools.SmsHandler.Abstract
{
    public interface IATLSmsService
    {
        Task<SmsOperationResult<bool, string, string>> SendIndividualSMSAsync(SMSMessage message);
        Task<SmsOperationResult<bool, string, string>> SendBulkSMSAsync(SMSMessageBulk smsMessageBulk);
        Task<SmsOperationResult<int, string, string>> GetTotalUnitsAsync();
        Task<SmsOperationResult<string, string, string>> GetDetailedReport(string taskId);
    }
}
