using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment.Shared.Requests
{
    public class BaseRequest
    {
        public virtual string? Id { get; set; }
    }
}
