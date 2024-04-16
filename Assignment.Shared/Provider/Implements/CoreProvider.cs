using Assignment.Shared.Extensions;
using Assignment.Shared.Models;
using Assignment.Shared.Policies;
using Assignment.Shared.Provider.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment.Shared.Provider.Implements
{
    public class CoreProvider : ICoreProvider
    {
        public IIdentityProvider IdentityProvider { get; } = null!;

        public IMapper Mapper { get; } = null!;
        public CoreProvider(IIdentityProvider identityProvider,
#if !DEBUG
            ILogger logger,
#endif
          IMapper mapper)
        {
            Mapper = mapper;
            IdentityProvider = identityProvider;
#if !DEBUG
            Logger = logger;
#endif
        }
        public IEnumerable<IDataPolicy> CreatePolicies<T>()
        {
            Type entityType = typeof(T);
            List<IDataPolicy> policies = new List<IDataPolicy>();
            if (typeof(IBaseEntity).IsAssignableFrom(entityType))
            {
                var policy = Activator.CreateInstance(typeof(BaseEntityPolicy<>).MakeGenericType(entityType), this) as IDataPolicy;
                if (policy != null)
                {
                    policies.Add(policy);
                }
            }
            if (typeof(IActiveEntity).IsAssignableFrom(entityType))
            {
                var policy = Activator.CreateInstance(typeof(ActiveEntityPolicy<>).MakeGenericType(entityType)) as IDataPolicy;
                if (policy != null)
                {
                    policies.Add(policy);
                }
            }
            if (typeof(IIdentity).IsAssignableFrom(entityType))
            {
                var policy = Activator.CreateInstance(typeof(IdentityPolicy<>).MakeGenericType(entityType)) as IDataPolicy;
                if (policy != null)
                {
                    policies.Add(policy);
                }
            }
            return policies;
        }
        public void LogInformation(string message, object? data = null, [CallerMemberName] string? methodName = null, [CallerFilePath] string? filePath = null, [CallerLineNumber] int lineNumber = 0)
        {
            var logInfo = new List<string>()
            {
                $"file: {Path.GetFileNameWithoutExtension(filePath)}",
                $"method: {methodName}",
                $"line: {lineNumber}",
                $"message: {message}"
            };

            if (data is not null)
            {
#if !DEBUG
                logInfo.Add("data: {data}");
#else
                logInfo.Add($"data: {data.TrySerializeObject()}");
#endif
            }

            var logMessage = string.Join(", ", logInfo);
#if !DEBUG
            Logger.Information(logMessage, data?.TrySerializeObject());
#else
            Debug.WriteLine(logMessage);
#endif
        }
    }
}
