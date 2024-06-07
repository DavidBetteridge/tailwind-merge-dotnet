namespace TailwindMerge;

/// <summary>
/// This class wraps a list of tailwind class names
/// </summary>
/// <param name="classList"></param>
public class TailwindClass(string classList)
{
    /// <summary>
    /// Combines two list of tailwind class names.  Note addition over TailwindClasses
    /// is NOT commutative.  i.e.  A + B != B + A
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static TailwindClass operator +(TailwindClass lhs, TailwindClass rhs)
    {
        // Quick and dirty solution
        var twMerge = new TwMerge();
        return new TailwindClass(twMerge.Merge(lhs.ToString(), rhs.ToString()));
    }

    /// <summary>
    /// Return the classes as a space seperated list
    /// </summary>
    /// <returns></returns>
    public override string ToString() => classList;
}