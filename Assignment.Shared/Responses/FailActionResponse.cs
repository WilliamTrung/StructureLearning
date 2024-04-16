using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment.Shared.Constants.Enum;

namespace Assignment.Shared.Responses
{
    public class FailActionResponse : ActionResponse
    {
        public string? ErrorMessage { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public int? SubErrorCode { get; set; }
        public string ErrorCodeString => ErrorCode.ToString();
        public string? SubErrorCodeString { get; set; }
    }

    public class FailActionResponse<T> : FailActionResponse, IResultActionResponse<T>
    {
        public T Data { get; set; } = default(T)!;
    }
}
