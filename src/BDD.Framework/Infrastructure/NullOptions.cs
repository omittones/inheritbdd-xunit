using System;
using Xunit.Abstractions;

namespace BDD.Framework.Infrastructure
{
    [Serializable]
    public class NullOptions : ITestFrameworkExecutionOptions, ITestFrameworkDiscoveryOptions
    {
        public TValue GetValue<TValue>(string name)
        {
            return default(TValue);
        }

        public void SetValue<TValue>(string name, TValue value)
        {
        }
    }
}