using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework.Infrastructure
{
    public class NullSourceInformationProvider : LongLivedMarshalByRefObject, ISourceInformationProvider
    {
        public ISourceInformation GetSourceInformation(ITestCase testCase)
        {
            return new SourceInformation();
        }

        public void Dispose()
        {
        }
    }
}