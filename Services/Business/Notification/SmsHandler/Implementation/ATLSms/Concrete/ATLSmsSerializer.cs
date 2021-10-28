using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Tools.SmsHandler.Abstract;
using System.Xml.Serialization;
using Core.Tools.SmsHandler.Implementation.ATLSms.Abstract;

namespace Core.Tools.SmsHandler.Implementation.ATLSms.Concreate
{
    public partial class ATLSms : ISMSService
    {

        #region Operation unit

        [XmlRoot(ElementName = "response")]
        public class OperationUnitResponse : IATLSmsSerializer
        {
            [XmlElement("head")]
            public OperationUnitHead Head { get; set; }

            [XmlElement("body")]
            public OperationUnitBody Body { get; set; }
        }

        public class OperationUnitHead
        {
            [XmlElement("responsecode")]
            public string ResponseCode { get; set; }
        }

        public class OperationUnitBody
        {
            [XmlElement("units")]
            public int Units { get; set; }
        }

        #endregion

        #region Delivery report

        [XmlRoot(ElementName = "response")]
        public class OperationDeliveryReportResponse : IATLSmsSerializer
        {
            [XmlElement("head")]
            public OperationDeliveryReportHead Head { get; set; }

            [XmlElement("body")]
            public List<OperationDeliveryReportBody> Bodies { get; set; }
        }

        public class OperationDeliveryReportHead
        {
            [XmlElement("responsecode")]
            public string ResponseCode { get; set; }
        }

        public class OperationDeliveryReportBody
        {
            [XmlElement("msisdn")]
            public string PhoneNumber { get; set; }

            [XmlElement("message")]
            public string Message { get; set; }

            [XmlElement("status")]
            public string Status { get; set; }
        }

        #endregion

        #region Submit

        [XmlRoot(ElementName = "response")]
        public class OperationSubmitResponse : IATLSmsSerializer
        {
            [XmlElement("head")]
            public OperationSubmitHead Head { get; set; }

            [XmlElement("body")]
            public OperationSubmitBody Body { get; set; }
        }

        public class OperationSubmitHead
        {
            [XmlElement("responsecode")]
            public string ResponseCode { get; set; }
        }

        public class OperationSubmitBody
        {
            [XmlElement("taskid")]
            public string TaskId { get; set; }
        }

        #endregion
    }
}
