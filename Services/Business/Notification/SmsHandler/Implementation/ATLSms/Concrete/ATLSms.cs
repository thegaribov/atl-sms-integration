using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Tools.SmsHandler.Abstract;
using System.Xml.Serialization;
using System.IO;
using Core.Constants.Notificaiton.SmsHandler;

namespace Core.Tools.SmsHandler.Implementation.ATLSms.Concreate
{
    public partial class ATLSms : ISMSService, IATLSmsService
    {
        public async Task<SmsOperationResult<bool, string, string>> SendSMS(SMSMessage message)
        {
            return await SendIndividualSMSAsync(message);
        }

        //Item1 - operation result, Item2 - opeartion request xml, 
        public async Task<SmsOperationResult<bool, string, string>> SendIndividualSMSAsync(SMSMessage message)
        {
            string dataXML = GenerateOperationSubmitIndividualXML(message.Text, message.PhoneNumber);

            HttpResponseMessage httpResponse = await SendRequestXMLAsync(HttpMethod.Post, _smsConfiguration.API_URL, dataXML, Encoding.UTF8);
            var content = await httpResponse.Content.ReadAsStringAsync();

            var resultObj = ATLResponseDeserializer<OperationSubmitResponse>(content);

            bool operationResult = _atlSmsError.IsOperationSuccess(resultObj.Head.ResponseCode);

            return new SmsOperationResult<bool, string, string>(operationResult, dataXML, content);
        }

        public async Task<SmsOperationResult<bool, string, string>> SendBulkSMSAsync(SMSMessageBulk smsMessageBulk)
        {
            string dataXML = GenerateOperationSubmitBulkXML(smsMessageBulk.Text, smsMessageBulk.PhoneNumbers);

            HttpResponseMessage httpResponse = await SendRequestXMLAsync(HttpMethod.Post, _smsConfiguration.API_URL, dataXML, Encoding.UTF8);
            var content = await httpResponse.Content.ReadAsStringAsync();

            var resultObj = ATLResponseDeserializer<OperationSubmitResponse>(content);
            bool operationResult = _atlSmsError.IsOperationSuccess(resultObj.Head.ResponseCode);

            return new SmsOperationResult<bool, string, string>(operationResult, dataXML, content);
        }

        public async Task<SmsOperationResult<int, string, string>> GetTotalUnitsAsync()
        {
            string dataXML = GenerateOperationUnitXML();

            HttpResponseMessage httpResponse = await SendRequestXMLAsync(HttpMethod.Post, _smsConfiguration.API_URL, dataXML, Encoding.UTF8);
            var content = await httpResponse.Content.ReadAsStringAsync();

            var resultObj = ATLResponseDeserializer<OperationUnitResponse>(content);

            if (_atlSmsError.IsOperationSuccess(resultObj.Head.ResponseCode))
            {
                int units = resultObj.Body.Units;
                return new SmsOperationResult<int, string, string>(units, dataXML, content); ;
            }
            else
            {
                //This means something went wrong
                return new SmsOperationResult<int, string, string>(-1, dataXML, content);
            }
        }

        public async Task<SmsOperationResult<string, string, string>> GetDetailedReport(string taskId)
        {
            string dataXML = GenerateOperationDetailedReportXML(taskId);

            HttpResponseMessage httpResponse = await SendRequestXMLAsync(HttpMethod.Post, _smsConfiguration.API_URL, dataXML, Encoding.UTF8);
            var content = await httpResponse.Content.ReadAsStringAsync();

            var resultObj = ATLResponseDeserializer<OperationDeliveryReportResponse>(content);

            if (_atlSmsError.IsOperationSuccess(resultObj.Head.ResponseCode))
            {
                StringBuilder reportResult = new StringBuilder();

                foreach (var body in resultObj.Bodies)
                {
                    reportResult.Append($"[{body.PhoneNumber}] - [{body.Message}] - [{_atlSmsReport.GetStatusByCode(body.Status)}]");
                }

                return new SmsOperationResult<string, string, string>(reportResult.ToString(), dataXML, content);
            }

            return new SmsOperationResult<string, string, string>(_atlSmsError.GetErrorMessageByCode(resultObj.Head.ResponseCode), dataXML, content);
        }
    }
}
