using System.Text;

namespace Prolog.Tests;

public class Tracer
{
    readonly StringBuilder builder = new ();

    public string Trace => builder.ToString();

    public void Engine_Unified(Runtime.Goal goal)
    {
        PrintGoal (goal, "Unified: ");
    }

    public void Engine_Failed(Runtime.Goal goal)
    {
        PrintGoal (goal, "Failed:  ");
    }

    public void Engine_NewGoal(Runtime.Goal goal)
    {
        PrintGoal (goal, "NewGoal: ");
    }

    private void PrintGoal (Runtime.Goal goal, string type)
    {
        builder.Append (type);
        builder.Append (new string (' ', 4 * goal.Level));
        Runtime.SolutionTreePrinter.Print (goal, builder);
        builder.AppendLine();
    }
}