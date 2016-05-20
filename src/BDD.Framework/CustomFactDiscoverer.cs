using System;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BDD.Framework
{
    public class CustomFactDiscoverer : XunitTestCase, IXunitTestCase
    {
        private string nameOverride;

        [Obsolete("Called by the deserializer", true)]
        public CustomFactDiscoverer()
        {
        }

        public CustomFactDiscoverer(IMessageSink diagnosticMessageSink, IXunitTestCase inner, IAttributeInfo fact)
            : base(diagnosticMessageSink,
                TestMethodDisplay.ClassAndMethod,
                inner.TestMethod,
                inner.TestMethodArguments)
        {
            this.nameOverride = fact.GetNamedArgument<string>("DisplayName");
        }

        public override void Serialize(IXunitSerializationInfo data)
        {
            base.Serialize(data);
            data.AddValue("nameOverride", this.nameOverride);
        }

        public override void Deserialize(IXunitSerializationInfo data)
        {
            base.Deserialize(data);
            this.nameOverride = data.GetValue<string>("nameOverride");
        }

        protected override void Initialize()
        {
            base.Initialize();

            if (string.IsNullOrEmpty(this.nameOverride))
                this.DisplayName = TransformName(this.TestMethod, this.TestMethodArguments, this.MethodGenericTypes);
            else
                this.DisplayName = this.nameOverride;
        }

        private static string FirstLowercase(string name)
        {
            if (name.Length > 1)
                return name.Substring(0, 1).ToLower() + name.Substring(1);
            else
                return name.ToLower();
        }

        private static string[] ExtractTypeNames(ITestMethod method)
        {
            var prefix = method.TestClass.Class.Name;
            prefix = (prefix ?? "").Split('.').Last();
            return prefix.Split('+');
        }

        private static string TransformName(
            ITestMethod method,
            object[] methodArgs,
            ITypeInfo[] genericTypes)
        {
            var names = ExtractTypeNames(method);
            var prefix = names.First();
            for (int i = 1; i < names.Length; i++)
                prefix += " " + FirstLowercase(names[i]);

            var postfix = FirstLowercase(method.Method.Name);

            var fullName = $"{prefix}, {postfix}".Replace('_', ' ');

            if (methodArgs != null && methodArgs.Any())
            {
                postfix = method.Method.GetDisplayNameWithArguments("", methodArgs, genericTypes);
                fullName += " " + postfix;
            }

            return fullName;
        }
    }
}