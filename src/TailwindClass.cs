using TailwindMerge.Models;

namespace TailwindMerge;

/// <summary>
/// This class wraps a list of tailwind class names
/// </summary>
/// <param name="classList"></param>
public class TailwindClass(string classList, IEnumerable<ClassInfo> classes)
{
    private IEnumerable<ClassInfo> Classes { get; } = classes;

    /// <summary>
    /// Combines two list of tailwind class names.  Note addition over TailwindClasses
    /// is NOT commutative.  i.e.  A + B != B + A
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static TailwindClass operator +(TailwindClass lhs, TailwindClass rhs)
    {
        var combinedClassNames = FilterConflictingClasses(lhs.Classes, rhs.Classes);

        var classList = string.Join(" ", combinedClassNames.Select(c => c.Name));

        return new TailwindClass(classList, combinedClassNames);
    }

    /// <summary>
    /// Return the classes as a space seperated list
    /// </summary>
    /// <returns></returns>
    public override string ToString() => classList;

    private static List<ClassInfo> FilterConflictingClasses(IEnumerable<ClassInfo> lhs, IEnumerable<ClassInfo> rhs)
    {
        var conflictingClassGroups = new HashSet<string>();
        var combined = new List<ClassInfo>();

        foreach (var c in rhs.Reverse())
        {
            if (!c.IsTailwindClass)
                combined.Add(c);

            var classGroupId = c.ModifierId + c.GroupId;
            if (conflictingClassGroups.Contains(classGroupId))
            {
                continue;
            }

            _ = conflictingClassGroups.Add(classGroupId);

            var classGroups = GetConflictingClassGroupIds(c.GroupId!, c.HasPostfixModifier);
            if (classGroups is { Length: > 0 })
            {
                foreach (var group in classGroups)
                {
                    _ = conflictingClassGroups.Add(c.ModifierId + group);
                }
            }

            combined.Add(c);
        }

        foreach (var c in lhs.Reverse())
        {
            if (!c.IsTailwindClass)
                combined.Add(c);

            var classGroupId = c.ModifierId + c.GroupId;
            if (conflictingClassGroups.Contains(classGroupId))
            {
                continue;
            }

            _ = conflictingClassGroups.Add(classGroupId);

            var classGroups = GetConflictingClassGroupIds(c.GroupId!, c.HasPostfixModifier);
            if (classGroups is { Length: > 0 })
            {
                foreach (var group in classGroups)
                {
                    _ = conflictingClassGroups.Add(c.ModifierId + group);
                }
            }

            combined.Add(c);
        }
        
        return combined;
    }
    
    private static string[]? GetConflictingClassGroupIds( string classGroupId, bool hasPostfixModifier )
    {
        
        var conflicts = TwMergeConfig.ConflictingClassGroupsLookup.GetValueOrDefault( classGroupId );

        if( hasPostfixModifier && 
            TwMergeConfig.ConflictingClassGroupModifiersLookup.TryGetValue(classGroupId, out var value) )
        {
            return [
                .. conflicts,
                .. value
            ];
        }

        return conflicts;
    }
}