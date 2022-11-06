using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prolog.Runtime;

public class Frame : ISolutionTreeNode
{
    private readonly Goal [] goals;
    private BoundVariableSet boundVariables;
    private readonly Variables variables;

    public Frame (Goal [] goals, Goal parent, BoundVariableSet boundVariables, Dictionary <string, Variable> variables)
    {
        this.goals = goals;
        this.boundVariables = boundVariables;
        this.HeadGoal = parent;

        this.variables = new Variables(variables);

        foreach (var goal in goals)
        {
            goal.Frame = this;
        }
    }

    public Goal CurrentGoal => this.GoalsProven < this.goals.Length ? this.goals[this.GoalsProven] : null;

    public int GoalsProven;

    public Frame Parent => HeadGoal == null ? null : HeadGoal.Frame;

    public IEnumerable <Frame> ParentFrames
    {
        get
        {
            var frame = Parent;

            while (frame != null)
            {
                yield return frame;

                frame = frame.Parent;
            }
        }
    }

    public Goal HeadGoal
    {
        get; set;
    }

    public Goal[] Goals => goals;

    public void ReleaseVariables()
    {
        boundVariables.Release ();
        boundVariables = null; // to prevent ReleaseVariables from being called twice.
    }

    public int Level => ParentFrames.Count();

    Variables ISolutionTreeNode.Variables => variables;

    ISolutionTreeNode ISolutionTreeNode.this [string goalName]
    {
        get
        {
            var matches = goals.OfType<PrologGoal>().Where(g => g.Predicate.Name == goalName).ToArray();

            return matches.Length switch
            {
                1 => matches.Single().CurrentFrame,
                0 => null,
                _ => throw new Exception(),
            };
        }
    }

    public IEnumerator<ISolutionTreeNode> GetEnumerator() => this.goals.Select(g => g.CurrentFrame).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public override string ToString()
    {
        var builder = new StringBuilder ();

        SolutionTreePrinter.Print (builder, this);

        return builder.ToString ();
    }
}