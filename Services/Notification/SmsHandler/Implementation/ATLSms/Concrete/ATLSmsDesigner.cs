using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Core.Services.RandomKeyGenerators;
using Services.Notification.SmsHandler.Abstract;
using Services.Notification.SmsHandler.Implementation.ATLSms.Abstract;

namespace Services.Notification.SmsHandler.Implementation.ATLSms.Concreate
{
    public partial class ATLSms : ISMSService
    {
        private readonly ATLSmsConfiguration _smsConfiguration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ATLSmsError _atlSmsError;
        private readonly ATLSmsReportStatus _atlSmsReport;

        public ATLSms(ATLSmsConfiguration smsConfiguration, IHttpClientFactory clientFactory)
        {
            _smsConfiguration = smsConfiguration;
            _clientFactory = clientFactory;
            _atlSmsError = new ATLSmsError();
            _atlSmsReport = new ATLSmsReportStatus();
        }

        private string GenerateOperationUnitXML()
        {
            string OPERATION = "units";
            string LOGIN = _smsConfiguration.API_LOGIN;
            string PASSWORD = _smsConfiguration.API_SECRET;

            string dataXML =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<request>" +
                    "<head>" +
                        $"<operation>{OPERATION}</operation>" +
                        $"<login>{LOGIN}</login>" +
                        $"<password>{PASSWORD}</password>" +
                    "</head>" +
                "</request>".Trim();

            return dataXML;
        }

        private string GenerateOperationDetailedReportXML(string taskId)
        {
            string OPERATION = "detailedreport";
            string LOGIN = _smsConfiguration.API_LOGIN;
            string PASSWORD = _smsConfiguration.API_SECRET;

            string dataXML =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<request>" +
                    "<head>" +
                        $"<operation>{OPERATION}</operation>" +
                        $"<login>{LOGIN}</login>" +
                        $"<password>{PASSWORD}</password>" +
                        $"<taskid>{taskId}</taskid>" +
                    "</head>" +
                "</request>".Trim();

            return dataXML;
        }


        private string GenerateOperationSubmitIndividualXML(List<string> messages, List<string> phoneNumbers, DateTime? schedule = null)
        {
            string GetScheduleDate(DateTime? schedule)
            {
                return schedule.HasValue ? schedule.Value.ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string GetBody(List<string> messages, List<string> phoneNumbers)
            {
                if (messages.Count != phoneNumbers.Count)
                {
                    throw new Exception($"Messages count [{messages.Count}] and Phone numbers count are not same [{phoneNumbers.Count}]");
                }

                StringBuilder body = new StringBuilder();

                for (int i = 0; i < phoneNumbers.Count; i++)
                {
                    body.Append($"<body><msisdn>{phoneNumbers[i]}</msisdn><message>{messages[i]}</message></body>");
                }

                return body.ToString();
            }

            string OPERATION = "submit";
            string TITLE = _smsConfiguration.Sender;
            string LOGIN = _smsConfiguration.API_LOGIN;
            string PASSWORD = _smsConfiguration.API_SECRET;
            string SCHEDULED = GetScheduleDate(schedule);
            string ISBULK = "false";
            string CONTROLE_ID = KeyGenerator.GetUniqueKey(20, false); //This part will be updated so uow will be integared

            string dataXML =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<request>" +
                    "<head>" +
                        $"<operation>{OPERATION}</operation>" +
                        $"<login>{LOGIN}</login>" +
                        $"<password>{PASSWORD}</password>" +
                        $"<title>{TITLE}</title>" +
                        $"<scheduled>{SCHEDULED}</scheduled>" +
                        $"<isbulk>{ISBULK}</isbulk>" +
                        $"<controlid>{CONTROLE_ID}</controlid>" +
                        "<unicode>true</unicode>" +
                    "</head>" +
                    $"{GetBody(messages, phoneNumbers)}" +
                "</request>".Trim();

            return dataXML;
        }

        private string GenerateOperationSubmitIndividualXML(string message, string phoneNumber, DateTime? schedule = null)
        {
            var messages = new List<string> { message };
            var phoneNumbers = new List<string> { phoneNumber };

            return GenerateOperationSubmitIndividualXML(messages, phoneNumbers, schedule);
        }


        private string GenerateOperationSubmitBulkXML(string bulkMessage, List<string> phoneNumbers, DateTime? schedule = null)
        {
            string GetScheduleDate(DateTime? schedule)
            {
                return schedule.HasValue ? schedule.Value.ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            string GetBody(List<string> phoneNumbers)
            {
                StringBuilder body = new StringBuilder();

                foreach (var phoneNumber in phoneNumbers)
                {
                    body.Append($"<body><msisdn>{phoneNumber}</msisdn></body>");
                }

                return body.ToString();
            }

            string OPERATION = "submit";
            string TITLE = _smsConfiguration.Sender;
            string LOGIN = _smsConfiguration.API_LOGIN;
            string PASSWORD = _smsConfiguration.API_SECRET;
            string SCHEDULED = GetScheduleDate(schedule);
            string ISBULK = "true";
            string CONTROLE_ID = KeyGenerator.GetUniqueKey(20, false); //This part will be updated so uow will be integared

            string dataXML =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<request>" +
                    "<head>" +
                        $"<operation>{OPERATION}</operation>" +
                        $"<login>{LOGIN}</login>" +
                        $"<password>{PASSWORD}</password>" +
                        $"<title>{TITLE}</title>" +
                        $"<bulkmessage>{bulkMessage}</bulkmessage>" +
                        $"<scheduled>{SCHEDULED}</scheduled>" +
                        $"<isbulk>{ISBULK}</isbulk>" +
                        $"<controlid>{CONTROLE_ID}</controlid>" +
                        "<unicode>true</unicode>" +
                    "</head>" +
                    $"{GetBody(phoneNumbers)}" +
                "</request>".Trim();

            return dataXML;
        }
    
    
        public T ATLResponseDeserializer<T>(string content)
            where T : IATLSmsSerializer
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result;

            using (TextReader reader = new StringReader(content))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }

        private async Task<HttpResponseMessage> SendRequestXMLAsync(HttpMethod method, string endpoint, string dataXML, Encoding encoding)
        {
            var request = new HttpRequestMessage(method, endpoint);
            request.Headers.Add("Accept", "application/xml");
            request.Content = new StringContent(dataXML, encoding, "application/xml");

            var client = _clientFactory.CreateClient();
            return await client.SendAsync(request);
        }
    }
}
