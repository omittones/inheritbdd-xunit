using System;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework.Infrastructure
{
    public class ExtendedTheory : XunitTheoryTestCase, IXunitTestCase
    {
        [Obsolete("Called by the deserializer", true)]
        public ExtendedTheory()
        {
        }

        public ExtendedTheory(IMessageSink diagnosticMessageSink, IXunitTestCase inner) : base(
            diagnosticMessageSink,
            TestMethodDisplay.ClassAndMethod,
            inner.TestMethod)
        {
            this.TestMethodArguments = inner.TestMethodArguments;
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.DisplayName = ExtendedFact.TransformName(this, this.MethodGenericTypes);
        }
    }
}