# InheritBDD for  xUnit

Allow inheriting from existing test classes to reuse initialization code, without repeating all the tests. No need for special attribute decorators or magic method names.

## Example:

```cs
using InheritBDD.xUnit;
using Xunit;

[assembly: TestFramework(Framework.TypeName, Framework.AssemblyName)]

public class Given_one
{
    protected readonly int first;

    public Given_one()
    {
        this.first = 1;
    }
    
    [Fact]
    public void First_is_one()
    {
        Assert.True(this.first == 1);
    }

    [Fact]
    public virtual void First_plus_one_is_two()
    {
        Assert.True(this.first + 1 == 2);
    }
}

public class Given_two : Given_one
{
    protected readonly int second;

    public Given_two() : base()
    {
        this.second = 1;
    }
    
    [Fact]
    public void First_plus_second_is_three()
    {
        Assert.True(this.first + this.second == 3);
    }
}
```
