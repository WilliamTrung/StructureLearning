using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Constants
{
    public class Enum
    {
        public enum ErrorCode
        {
            System,
            IntegrationService,
            ObjectAlreadyExists,
            InvalidObject,
            NullReference,
            ActivatedObject,
            ExpiredObject,
            InactiveObject,
            LimitedObject,
            InvalidInput,
            RequiredObject
        }
    }
}
