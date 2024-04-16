using Assignment.Shared.Policies;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Provider.Abstractions
{
    public interface ICoreProvider
    {
        IIdentityProvider IdentityProvider { get; }
        IEnumerable<IDataPolicy> CreatePolicies<T>();
        IMapper Mapper { get; }
#if !DEBUG
        ILogger Logger { get; set; }
#endif
        void LogInformation(string message, object? data = null, [CallerMemberName] string? methodName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0);
    }
}
