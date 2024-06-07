namespace TailwindMerge.Tests;

public partial class ColorsClasses2Tests
{
    [Fact]
    public void Merge_Colors_MergesCorrectly()
    {
        var actual = Example1_Lhs() + Example1_Rhs();
        Assert.Equal( "hover:inline bg-hotpink text-white", actual.ToString() );
    }
    
    [GeneratedTailwindClass( "hover:inline bg-grey-5" )]
    private static partial TailwindClass Example1_Lhs();
    
    [GeneratedTailwindClass( "bg-hotpink text-white" )]
    private static partial TailwindClass Example1_Rhs();
}

internal class GeneratedTailwindClassAttribute : Attribute
{
    public GeneratedTailwindClassAttribute(string classList)
    {
    }
}

public partial class ColorsClasses2Tests
{
    private static partial TailwindClass Example1_Lhs() => new("hover:inline bg-grey-5");
    
    private static partial TailwindClass Example1_Rhs() => new("bg-hotpink text-white");
}

public class TailwindClass(string classList)
{
    public static TailwindClass operator +(TailwindClass a, TailwindClass b)
    {
        var twMerge = new TwMerge();
        return new TailwindClass(twMerge.Merge(a.ToString(), b.ToString()));
    }

    public override string ToString() => classList;
}
