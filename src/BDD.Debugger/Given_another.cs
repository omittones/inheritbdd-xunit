using Xunit;

namespace BDD.Debugger
{
    public class Given_another : Given_one
    {
        protected readonly int second;

        public Given_another()
        {
            this.second = 2;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Return_three_from_another(int number)
        {
            Assert.True(this.first + this.second == 3);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Return_four_from_another(int number)
        {
            Assert.True(this.first + this.second + 1 == 4);
        }

        [Fact]
        public override void Should_not_show_in_one()
        {
            base.Should_not_show_in_one();
        }

        public override void Should_not_show_anywhere()
        {
            base.Should_not_show_anywhere();
        }

        public class And_one_more : Given_another
        {
            [Fact]
            public void Should_be_nested()
            {
            }
        }
    }
}