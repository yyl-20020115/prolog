using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Prolog.Runtime;

public class Variables : IEnumerable <KeyValuePair <string, IConcreteValue>>
{
    private readonly Dictionary <string, Variable> variables;

    internal Variables(Dictionary<string, Variable> variables) => this.variables = variables;

    public IConcreteValue this[string variableName] => variables.TryGetValue(variableName, out Variable variable) ? variable.ConcreteValue : null;

    public bool Contains(string variableName) => this.variables.ContainsKey(variableName);

    public IEnumerator<KeyValuePair<string, IConcreteValue>> GetEnumerator() => variables.ToDictionary(
            variable => variable.Key,
            variable => variable.Value.ConcreteValue)
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int Count => this.variables.Count;
}