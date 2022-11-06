using System.Collections.Generic;
using System.Text;

namespace Prolog.Runtime;

public abstract class Goal 
{
    internal Frame GetNextFrame ()
    {
        if (CurrentFrame != null)
        {
            CurrentFrame.ReleaseVariables();
        }

        return frameEnumerator.MoveNext () ? CurrentFrame : null;
    }

    internal Frame CurrentFrame => frameEnumerator?.Current;

    /// <summary>
    /// The frame that contains this goal and other sibling goals.
    /// </summary>
    public Frame Frame {get; set;}

    public Compiled.Goal Definition {get; set;}
    public IValue [] Arguments {get; set;}

    public int Level => this.Frame.Level;

    public override string ToString()
    {
        var builder = new StringBuilder();
        SolutionTreePrinter.Print(this, builder);
        return builder.ToString ();
    }

    IEnumerator <Frame> frameEnumerator;

    internal void Restart()
    {
        if (frameEnumerator != null && CurrentFrame != null)
        {
            CurrentFrame.ReleaseVariables();
        }

        frameEnumerator = GetFrames ()?.GetEnumerator ();
    }

    protected abstract IEnumerable<Frame> GetFrames();
}