﻿using TailwindMerge.Common;

namespace TailwindMerge.Models;

internal record ClassNameNode
{
    internal string? ClassGroupId { get; set; }
    internal List<ClassValidator>? Validators { get; set; }
    internal Dictionary<string, ClassNameNode>? Children { get; set; }

    internal ClassNameNode Insert( string className, string classGroupId )
    {
        var current = this;

        foreach( var part in className.Split( Constants.ClassNameSeparator, StringSplitOptions.RemoveEmptyEntries ) )
        {
            current.Children ??= [];

            if( !current.Children.TryGetValue( part, out var node ) )
            {
                node = new ClassNameNode();
                current.Children[part] = node;
            }

            current = node;
        }

        current.ClassGroupId = classGroupId;
        return current;
    }
}
