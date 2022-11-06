namespace Prolog.Runtime;

public class Atom : IConcreteValue
{
    public Atom(string name) => this.Name = name;

    public string Name {get; private set; }

    T IValue.Accept<T>(IValueVisitor<T> visitor) => visitor.Visit(this);

    IConcreteValue IValue.ConcreteValue => this;

    T IConcreteValue.Accept<T>(IConcreteValueVisitor<T> visitor) => visitor.Visit(this);

    bool IValue.Accept(IValueUnifier visitor) => visitor.Visit(this);
}