using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Requests
{
    public class BaseGetAllRequest
    {
        public int? PageSize { get; set; }
        public int PageIndex { get; set; }
        public string? Filter { get; set; }
        public string? SortField { get; set; }
        public bool? Asc { get; set; }
    }
}
