using System;

namespace TailwindMerge.Models;

internal readonly record struct ClassValidator
{
    public ClassValidator(string ClassGroupId, Func<string, bool> Validator)
    {
        this.ClassGroupId = ClassGroupId;
        this.Validator = Validator;
    }

    public string ClassGroupId { get; }
    public Func<string, bool> Validator { get; }

    public void Deconstruct(out string ClassGroupId, out Func<string, bool> Validator)
    {
        ClassGroupId = this.ClassGroupId;
        Validator = this.Validator;
    }
}