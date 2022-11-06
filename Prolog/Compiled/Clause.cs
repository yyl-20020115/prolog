using System;

namespace Prolog.Compiled;

[Serializable]
public class Clause
{
    public Goal Head;

    public Goal[] Body;
}