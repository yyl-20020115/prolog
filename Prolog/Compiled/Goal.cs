using System;

namespace Prolog.Compiled;

[Serializable]
public class Goal
{
    public Predicate Predicate;

    public IArgument[] Arguments;
}