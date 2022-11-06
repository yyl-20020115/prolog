using System.Linq;

namespace Prolog.Runtime;

public class ArgumentPrinter : IValueVisitor <string>
{
    public string Visit (Atom atom)
    => atom.Name;

    public string Visit (Variable variable)
    {
        var value = variable.ConcreteValue;

        return variable.Name + ((value == null) ? "" : "=" + value.Accept (this));
    }

    public string Visit(List list) => "[" + string.Join(", ", list.Select(Print)) + "]";

    /// <summary>
    /// Convenience method.
    /// </summary>
    public string Print (IValue value)
    => value.Accept(this);
}