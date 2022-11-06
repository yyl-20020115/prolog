using System.Linq;
using System.Text;

namespace Prolog.Runtime;

public class SolutionTreePrinter
{
    public delegate void NodePrinter (StringBuilder builder, ISolutionTreeNode node);

    private readonly NodePrinter nodePrinter;
    private readonly StringBuilder builder = new ();

    private SolutionTreePrinter() => nodePrinter = PrintNode;

    public SolutionTreePrinter (NodePrinter nodePrinter) => this.nodePrinter = nodePrinter;

    public static string SolutionTreeToString(ISolutionTreeNode solution) => new SolutionTreePrinter().Print(solution);

    public string Print (ISolutionTreeNode solution)
    {
        SolutionTreeToString (solution, 0);

        return builder.ToString ();
    }

// ReSharper disable ParameterTypeCanBeEnumerable.Local
    private void SolutionTreeToString (ISolutionTreeNode solution, int level)
// ReSharper restore ParameterTypeCanBeEnumerable.Local
    {
        foreach (var node in solution)
        {
            var s = NodeToString (node);

            if (!string.IsNullOrWhiteSpace (s))
            {
                builder.Append (new string (' ', level * 4));

                builder.AppendLine (s);
            }

            SolutionTreeToString (node, level + 1);
        }
    }

    string NodeToString (ISolutionTreeNode node)
    {
        var stringBuilder = new StringBuilder ();

        nodePrinter (stringBuilder, node);

        return stringBuilder.ToString ();
    }

    static void PrintNode (StringBuilder stringBuilder, ISolutionTreeNode node)
    {
        Print (stringBuilder, node);
    }

    public static void PrintDcgNode (StringBuilder stringBuilder, ISolutionTreeNode node)
    {
        var goal = node.HeadGoal;
        var predicate = goal.Definition.Predicate.Name;
        if (predicate != "concat")
        {
            stringBuilder.Append (predicate);
            stringBuilder.Append ("(");
            stringBuilder.Append (string.Join(", ", goal.Arguments.Where(a => !(a.ConcreteValue is List)).Select (new ArgumentPrinter().Print)));
            stringBuilder.Append (")");
        }
    }

    public static void Print (StringBuilder sb, ISolutionTreeNode frame)
    {
        Print (frame.HeadGoal, sb);
    }

    public static void Print (Goal goal, StringBuilder sb)
    {
        sb.Append (goal.Definition.Predicate.Name);
        sb.Append ("(");
        sb.Append (string.Join(", ", goal.Arguments.Select (new ArgumentPrinter().Print)));
        sb.Append (")");
    }
}
