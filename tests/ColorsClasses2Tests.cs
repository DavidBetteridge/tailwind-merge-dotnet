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
    
    [GeneratedTailwindClass( "bg-hotpink text-white" )]
    private static partial TailwindClass Example2_Rhs();
}