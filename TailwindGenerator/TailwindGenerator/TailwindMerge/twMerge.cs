using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using TailwindMerge.Common;
using TailwindMerge.Models;

namespace TailwindMerge;

/// <summary>
/// A utility for merging conflicting Tailwind CSS classes.
/// </summary>
public class TwMerge
{
    private readonly TwMergeConfig _config;
    private readonly TwMergeContext _context;

    /// <summary>
    /// Initializes a new instance of <see cref="TwMerge" />.
    /// </summary>
    public TwMerge()
    {
        _config = TwMergeConfig.Default();
        _context = new TwMergeContext( _config );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="classList"></param>
    /// <returns></returns>
    public IEnumerable<ClassInfo> SplitClassList(string classList)
    {
        var classes = SeparatorRegex()
            .Split( classList.Trim() )
            .Select( GetClassInfo );
        
        return classes;
    }
    
    private ClassInfo GetClassInfo(string className )
    {
        (var baseClassName,
            var hasImportantModifier,
            var postfixModifierPosition,
            var modifiers) = _context.SplitModifiers( className );

        var hasPostfixModifier = postfixModifierPosition.HasValue;

        var classGroupId = _context.GetClassGroupId( hasPostfixModifier
            ? baseClassName.Substring(0,postfixModifierPosition!.Value)
            : baseClassName );

        if( string.IsNullOrEmpty( classGroupId ) )
        {
            if( !hasPostfixModifier )
            {
                return new ClassInfo( className, isTailwindClass: false );
            }

            classGroupId = _context.GetClassGroupId( baseClassName );

            if( string.IsNullOrEmpty( classGroupId ) )
            {
                return new ClassInfo( className, isTailwindClass: false );
            }

            hasPostfixModifier = false;
        }

        var variantModifier = string.Join( ":", _context.SortModifiers( modifiers ) );
        var modifierId = hasImportantModifier
            ? variantModifier + Constants.ImportantModifier
            : variantModifier;

        return new ClassInfo(
            className,
            classGroupId,
            modifierId,
            IsTailwindClass: true,
            hasPostfixModifier
        );
    }

    private static Regex SeparatorRegex() => new(@"\s+", RegexOptions.Compiled);
}
