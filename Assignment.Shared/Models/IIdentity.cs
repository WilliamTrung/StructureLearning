using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Models
{
    public interface IIdentity
    {
        string Id { get; set; }
        void GenerateId();
    }
}
