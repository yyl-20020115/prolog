using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prolog.Runtime;

public class List : IEnumerable <IValue>, IConcreteValue
{
    private readonly IValue [] elements;

    public List(IValue[] elements) => this.elements = elements;

    T IValue.Accept<T>(IValueVisitor<T> visitor) => visitor.Visit(this);

    public IEnumerator<IValue> GetEnumerator() => elements.Select(x => x).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool IsAtLeastAsLongAs(List list1) => elements.Length >= list1.elements.Length;

    public List SkipLengthOf(List other) => new List(this.Skip(other.elements.Length).ToArray());

    public bool IsSameLength (List list)
    => elements.Length == list.elements.Length;

    IConcreteValue IValue.ConcreteValue => this;

    T IConcreteValue.Accept<T>(IConcreteValueVisitor<T> visitor) => visitor.Visit(this);

    bool IValue.Accept(IValueUnifier visitor) => visitor.Visit(this);
}