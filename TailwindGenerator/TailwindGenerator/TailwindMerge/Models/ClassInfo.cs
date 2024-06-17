namespace TailwindMerge.Models;

public readonly record struct ClassInfo
{

    internal ClassInfo( string name, bool isTailwindClass )
        : this( name, null, null, isTailwindClass, false ) { }

    public ClassInfo(string Name,
        string? GroupId,
        string? ModifierId,
        bool IsTailwindClass,
        bool HasPostfixModifier)
    {
        this.Name = Name;
        this.GroupId = GroupId;
        this.ModifierId = ModifierId;
        this.IsTailwindClass = IsTailwindClass;
        this.HasPostfixModifier = HasPostfixModifier;
    }

    public string Name { get; }
    public string? GroupId { get; }
    public string? ModifierId { get; }
    public bool IsTailwindClass { get; }
    public bool HasPostfixModifier { get; }

    public void Deconstruct(out string Name, out string? GroupId, out string? ModifierId, out bool IsTailwindClass, out bool HasPostfixModifier)
    {
        Name = this.Name;
        GroupId = this.GroupId;
        ModifierId = this.ModifierId;
        IsTailwindClass = this.IsTailwindClass;
        HasPostfixModifier = this.HasPostfixModifier;
    }
}
