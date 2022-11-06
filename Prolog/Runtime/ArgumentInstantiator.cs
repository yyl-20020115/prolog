using System.Linq;
using System.Collections.Generic;

namespace Prolog.Runtime;

class ArgumentInstantiator : IArgumentVisitor <IValue>
{
    private readonly Dictionary<string, Variable> variables = new();

    public Dictionary<string, Variable> Variables => variables;

    IValue IArgumentVisitor<IValue>.Visit(Prolog.Atom atom) => new Atom(atom.Name);

    IValue IArgumentVisitor<IValue>.Visit(Prolog.Variable variableDef)
    {
        if (!variables.TryGetValue(variableDef.Name, out Variable variable))
        {
            variable = new Variable(variableDef.Name);

            variables.Add(variableDef.Name, variable);
        }

        return variable;
    }

    IValue IArgumentVisitor<IValue>.Visit(Prolog.List list)
        => new List(list.Elements.Select(e => e.Accept(this)).ToArray());
}