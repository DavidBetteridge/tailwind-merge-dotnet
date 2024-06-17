using System.Collections.Generic;

namespace TailwindMerge.Models;

internal readonly record struct ClassModifiersInfo
{
    public ClassModifiersInfo(string BaseClassName,
        bool HasImportantModifier,
        int? PostfixModifierPosition,
        ICollection<string> Modifiers)
    {
        this.BaseClassName = BaseClassName;
        this.HasImportantModifier = HasImportantModifier;
        this.PostfixModifierPosition = PostfixModifierPosition;
        this.Modifiers = Modifiers;
    }

    public string BaseClassName { get; }
    public bool HasImportantModifier { get; }
    public int? PostfixModifierPosition { get; }
    public ICollection<string> Modifiers { get; }

    public void Deconstruct(out string BaseClassName, out bool HasImportantModifier, out int? PostfixModifierPosition, out ICollection<string> Modifiers)
    {
        BaseClassName = this.BaseClassName;
        HasImportantModifier = this.HasImportantModifier;
        PostfixModifierPosition = this.PostfixModifierPosition;
        Modifiers = this.Modifiers;
    }
}
