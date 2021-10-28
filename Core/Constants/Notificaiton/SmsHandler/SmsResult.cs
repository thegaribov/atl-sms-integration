using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants.Notificaiton.SmsHandler
{
    public class SmsOperationResult<TResult, TRequest, TResponse>
    {
        public SmsOperationResult(TResult result, TRequest request, TResponse response)
        {
            Result = result;
            Request = request;
            Response = response;
        }

        public TResult Result { get; set; }
        public TRequest Request { get; set; }
        public TResponse Response { get; set; }
    }
}
