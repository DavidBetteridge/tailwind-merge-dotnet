using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TailwindMerge.Common;

internal static partial class Validators
{
    private static HashSet<string> _stringLengths = ["px", "full", "screen"];
    private static HashSet<string> _sizeLabels = ["length", "size", "percentage"];
    private static HashSet<string> _imageLabels = ["image", "url"];

    internal static Func<string, bool> IsLength = ( value ) =>
    {
        return IsNumber!( value ) || _stringLengths.Contains( value ) || FractionRegex().IsMatch( value );
    };

    internal static Func<string, bool> IsArbitraryLength = ( value ) =>
    {
        return GetIsArbitraryValue( value, "length", IsLengthOnly );
    };

    internal static Func<string, bool> IsNumber = ( value ) =>
    {
        return !string.IsNullOrEmpty( value ) && double.TryParse( value, NumberStyles.None, CultureInfo.InvariantCulture, out _ );
    };

    internal static Func<string, bool> IsInteger = ( value ) =>
    {
        return !string.IsNullOrEmpty( value ) && int.TryParse( value, out _ );
    };

    internal static Func<string, bool> IsPercent = ( value ) =>
    {
        return value.Length > 1 && value.EndsWith( "%" ) && IsNumber( value.Substring(0, value.Length-1) );
    };

    internal static Func<string, bool> IsArbitraryNumber = ( value ) =>
    {
        return GetIsArbitraryValue( value, "number", IsNumber );
    };

    internal static Func<string, bool> IsTshirtSize = ( value ) =>
    {
        return TshirtUnitRegex().IsMatch( value );
    };

    internal static Func<string, bool> IsArbitraryValue = ( value ) =>
    {
        return ArbitraryValueRegex().IsMatch( value );
    };

    internal static Func<string, bool> IsArbitrarySize = ( value ) =>
    {
        return GetIsArbitraryValue( value, _sizeLabels, IsNever );
    };

    internal static Func<string, bool> IsArbitraryPosition = ( value ) =>
    {
        return GetIsArbitraryValue( value, "position", IsNever );
    };

    internal static Func<string, bool> IsArbitraryImage = ( value ) =>
    {
        return GetIsArbitraryValue( value, _imageLabels, IsImage );
    };

    internal static Func<string, bool> IsArbitraryShadow = ( value ) =>
    {
        return GetIsArbitraryValue( value, string.Empty, IsShadow );
    };

    internal static Func<string, bool> IsAny = ( _ ) => true;

    private static bool GetIsArbitraryValue( string value, object label, Func<string, bool> testValue )
    {
        var match = ArbitraryValueRegex().Match( value );

        if( match.Success )
        {
            if( !string.IsNullOrEmpty( match.Groups[1].Value ) )
            {
                if( label is string str )
                {
                    return match.Groups[1].Value == str;
                }

                if( label is HashSet<string> set )
                {
                    return set.Contains( match.Groups[1].Value );
                }
            }

            return testValue( match.Groups[2].Value );
        }

        return false;
    }

    private static bool IsLengthOnly( string value )
    {
        return LengthUnitRegex().IsMatch( value ) && !ColorFunctionRegex().IsMatch( value );
    }

    private static bool IsNever( string value )
    {
        return false;
    }

    private static bool IsShadow( string value )
    {
        return ShadowRegex().IsMatch( value );
    }

    private static bool IsImage( string value )
    {
        return ImageRegex().IsMatch( value );
    }

    private static Regex ArbitraryValueRegex() =>
        new(@"^\[(?:([a-z-]+):)?(.+)\]$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private static Regex FractionRegex() => new(@"^\d+\/\d+$", RegexOptions.Compiled);

    private static Regex TshirtUnitRegex() =>
    new(@"^(\d+(\.\d+)?)?(xs|sm|md|lg|xl)$", RegexOptions.Compiled);

    private static Regex LengthUnitRegex() =>
        new(@"\d+(%|px|r?em|[sdl]?v([hwib]|min|max)|pt|pc|in|cm|mm|cap|ch|ex|r?lh|cq(w|h|i|b|min|max))|\b(calc|min|max|clamp)\(.+\)|^0$", RegexOptions.Compiled);

    private static Regex ColorFunctionRegex() =>
        new(@"^(rgba?|hsla?|hwb|(ok)?(lab|lch))\(.+\)$", RegexOptions.Compiled);

    private static Regex ShadowRegex() =>
        new(@"^(inset_)?-?((\d+)?\.?(\d+)[a-z]+|0)_-?((\d+)?\.?(\d+)[a-z]+|0)", RegexOptions.Compiled);

    private static Regex ImageRegex() =>
        new(@"^(url|image|image-set|cross-fade|element|(repeating-)?(linear|radial|conic)-gradient)\(.+\)$", RegexOptions.Compiled);
}
