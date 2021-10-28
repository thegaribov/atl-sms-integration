using Core.Constants.Notificaiton.SmsHandler;
using Core.Tools.SmsHandler;
using Core.Tools.SmsHandler.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISMSService _smsService;
        private readonly IATLSmsService _atlSmsService;

        public SmsController(ISMSService smsService, IATLSmsService atlSmsService)
        {
            _smsService = smsService;
            _atlSmsService = atlSmsService;
        }

        // GET: api/<SmsController>
        [HttpGet]
        public async Task<string> SendSimpleSMS([FromForm] string phoneNumber, [FromForm] string smsText)
        {
            var smsOperationResult = await _smsService.SendSMSAsync(phoneNumber, smsText);

            return smsOperationResult.Request + smsOperationResult.Response + smsOperationResult.Result;
        }

        [HttpGet]
        public async Task<string> SendIndividualSMS([FromForm] string phoneNumber, [FromForm] string smsText)
        {
            var scheduledDate = DateTime.UtcNow;
            var smsMessage = new SMSMessage(phoneNumber, smsText);

            var smsOperationResult = await _atlSmsService.SendIndividualSMSAsync(smsMessage, scheduledDate);

            return smsOperationResult.Request + smsOperationResult.Response + smsOperationResult.Result;
        }

        [HttpGet]
        public async Task<string> SendBulkSMS([FromForm] List<string> phoneNumbers, [FromForm] string smsText)
        {
            var smsOperationResult = await _atlSmsService.SendBulkSMSAsync(smsText, phoneNumbers);

            return smsOperationResult.Request + smsOperationResult.Response + smsOperationResult.Result;
        }

        [HttpGet]
        public async Task<string> GetTotalUnits()
        {
            var smsOperationResult = await _atlSmsService.GetTotalUnitsAsync();

            return smsOperationResult.Request + smsOperationResult.Response + smsOperationResult.Result;
        }


        [HttpGet]
        public async Task<string> GetDetailedReport([FromHeader] string taskId)
        {
            var smsOperationResult = await _atlSmsService.GetDetailedReport(taskId);

            return smsOperationResult.Request + smsOperationResult.Response + smsOperationResult.Result;
        }

    }
}
