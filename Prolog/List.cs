using System;
using System.Collections.Generic;

namespace Prolog;

[Serializable]
public class List : IArgument
{
    private readonly IArgument [] elements;

    public List(params IArgument[] elements) => this.elements = elements;

    public IEnumerable<IArgument> Elements => elements;

    T IArgument.Accept<T>(IArgumentVisitor<T> visitor) => visitor.Visit(this);
}