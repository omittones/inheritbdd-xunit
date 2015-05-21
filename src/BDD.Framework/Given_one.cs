using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BDD.Framework
{
    public class Given_one
    {
        protected readonly int first;

        public Given_one()
        {
            this.first = 1;
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
