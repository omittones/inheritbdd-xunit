# InheritBDD for  xUnit

Allow inheriting from existing test classes to reuse initialization code, without repeating all the tests. No need for special attribute decorators or magic method names.
When inheriting from existing class with some Facts, by default, xUnit will treat the methods of that base class as new tests, essentialy repeating them.
This plugin disables repeating tests from inherited class (unless explicitly overriden). It also constructs test names from class and mehod names.

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
    
    [Fact] //will show in test window as 'Given one, first is one.'
    public void First_is_one()
    {
        Assert.True(this.first == 1);
    }

    [Fact] //will show in test window as 'Given one, first plus one is two.'
    public void First_plus_one_is_two()
    {
        Assert.True(this.first + 1 == 2);
    }
    
    [Fact] //will show as 'Given one, override works.'
    public virtual void Override_works()
    {
        Assert.True(true);
    }
}

public class Given_two : Given_one
{
    protected readonly int second;

    public Given_two() : base()
    {
        this.second = 1;
    }

    //inherited 'Given one, first is one.' will not be repeated for this test class
    //inherited 'Given one, first plus one is two.' will not be repeated for this test class
    
    [Fact] //will show in test window as 'Given two, first plus second is three.'
    public void First_plus_second_is_three()
    {
        Assert.True(this.first + this.second == 3);
    }
    
    [Fact] //will show as 'Given two, override works.'
    public override void Override_works()
    {
        base.Override_works();
    }
}
```
