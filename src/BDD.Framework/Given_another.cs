using Xunit;

namespace BDD.Framework
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
        public void Return_three(int number)
        {
            Assert.True(this.first + this.second == 3);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Return_four(int number)
        {
            Assert.True(this.first + this.second + 1 == 4);
        }
    }
}