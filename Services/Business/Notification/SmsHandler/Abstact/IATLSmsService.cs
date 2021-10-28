using Core.Constants.Notificaiton.SmsHandler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tools.SmsHandler.Abstract
{
    public interface IATLSmsService
    {
        Task<SmsOperationResult<bool, string, string>> SendIndividualSMSAsync(string phoneNumber, string smsText);
        Task<SmsOperationResult<bool, string, string>> SendIndividualSMSAsync(string phoneNumber, string smsText, DateTime schedule);
        Task<SmsOperationResult<bool, string, string>> SendIndividualSMSAsync(SMSMessage message, DateTime? schedule = null);

        Task<SmsOperationResult<bool, string, string>> SendBulkSMSAsync(string smsText, params string[] phoneNumbers);
        Task<SmsOperationResult<bool, string, string>> SendBulkSMSAsync(string smsText, List<string> phoneNumbers);
        Task<SmsOperationResult<bool, string, string>> SendBulkSMSAsync(SMSMessageBulk smsMessageBulk, DateTime? schedule = null);

        Task<SmsOperationResult<int, string, string>> GetTotalUnitsAsync();
        Task<SmsOperationResult<string, string, string>> GetDetailedReport(string taskId);
    }
}
