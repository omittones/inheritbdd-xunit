using Xunit;

namespace BDD.Debugger
{
    public class Given_one
    {
        protected readonly int first;

        public Given_one()
        {
            this.first = 1;
        }

        [Fact(Skip = "Because it was defined as such")]
        public void This_test_should_be_skipped()
        {
        }

        [Fact(DisplayName = "This name should appear")]
        public void This_name_should_not_appear()
        {
        }

        [Fact]
        public void Return_one_from_one()
        {
            Assert.True(this.first == 1);
        }

        [Fact]
        public void Return_two_from_one()
        {
            Assert.True(this.first + 1 == 2);
        }

        public virtual void Should_not_show_in_one()
        {
        }

        public virtual void Should_not_show_anywhere()
        {
        }
    }
}
