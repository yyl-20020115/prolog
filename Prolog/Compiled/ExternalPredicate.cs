using System;

namespace Prolog.Compiled;

[Serializable]
public class ExternalPredicate : Predicate
{
    public ExternalPredicate(string name) => this.Name = name;

    [NonSerialized]
    public ExternalPredicateDefinition Callback;

    public override T Accept<T>(IPredicateVisitor<T> visitor) => visitor.Visit(this);
}