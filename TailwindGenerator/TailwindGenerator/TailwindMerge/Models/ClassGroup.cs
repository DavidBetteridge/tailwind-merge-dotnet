using System;

namespace TailwindMerge.Models;

internal readonly record struct ClassGroup
{
    internal ClassGroup( string id, string[] items )
        : this( id, null, items, null ) { }

    internal ClassGroup( string id, string className, string[] items )
        : this( id, className, items, null ) { }

    internal ClassGroup( string id, string className, Func<string, bool>[] validators )
        : this( id, className, null, validators ) { }

    public ClassGroup(string Id,
        string? ClassName,
        string[]? Items,
        Func<string, bool>[]? Validators)
    {
        this.Id = Id;
        this.ClassName = ClassName;
        this.Items = Items;
        this.Validators = Validators;
    }

    public string Id { get; }
    public string? ClassName { get; }
    public string[]? Items { get; }
    public Func<string, bool>[]? Validators { get; }

    public void Deconstruct(out string Id, out string? ClassName, out string[]? Items, out Func<string, bool>[]? Validators)
    {
        Id = this.Id;
        ClassName = this.ClassName;
        Items = this.Items;
        Validators = this.Validators;
    }
}
