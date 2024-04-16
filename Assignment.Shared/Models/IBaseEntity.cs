using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Models
{
    public interface IBaseEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? LastUpdatedAt { get; set; }
        string CreatedBy { get; set; }
        string? LastUpdatedBy { get; set; }
    }
}
