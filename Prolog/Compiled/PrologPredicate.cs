using System;
using System.Collections.Generic;

namespace Prolog.Compiled;

[Serializable]
public class PrologPredicate : Predicate
{
    public IEnumerable<Clause> Clauses;

    public override T Accept<T>(IPredicateVisitor<T> visitor) => visitor.Visit(this);
}