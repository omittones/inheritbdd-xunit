using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework
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