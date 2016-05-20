using Xunit;

namespace InheritBDD.xUnit.Nuget
{
    public class Given_framework_installed_via_nuget
    {
        [Fact]
        public void Verify_it_works_ok()
        {
            Assert.Equal(true, true);
        }
    }
}