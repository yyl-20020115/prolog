﻿using System;
using System.Collections.Generic;

namespace Prolog.Runtime;

public interface IDebugEvent
{
    void Accept (IDebugEventSink sink);
}

public class Solution : IDebugEvent
{
    readonly ISolutionTreeNode tree;

    public Solution(ISolutionTreeNode tree) => this.tree = tree;

    public ISolutionTreeNode Tree => tree;

    public void Accept(IDebugEventSink sink) => sink.Visit(this);
}

public class Enter : IDebugEvent
{
    readonly ISolutionTreeNode node;

    public Enter(ISolutionTreeNode node) => this.node = node;

    public ISolutionTreeNode Node => node;

    public void Accept(IDebugEventSink sink) => sink.Visit(this);
}

public class Leave : IDebugEvent
{
    public Leave(ISolutionTreeNode node) => this.Node = node;

    public ISolutionTreeNode Node { get; }

    public void Accept(IDebugEventSink sink) => sink.Visit(this);
}

public class Engine : IDebugEventSink
{
    public event Action <Goal> Unified;
    public event Action <Goal> Failed;
    public event Action Start;

    public IEnumerable <ISolutionTreeNode> Solve (Compiled.Goal [] goalDefs)
    {
        solution = null;

        Start?.Invoke();

        var engine = new EngineInternals ();

        foreach (var @event in engine.Solve (goalDefs))
        {
            @event.Accept (this);

            if (solution != null)
            {
                yield return solution.Tree;

                solution = null;
            }
        }
    }

    public IEnumerable<ISolutionTreeNode> Run(Compiled.Program program) => Solve(program.GetStartupGoal());

    Solution solution;

    private void RaiseUnified (Goal goal)
        => Unified?.Invoke(goal);

    private void RaiseFailed (Goal goal)
        => Failed?.Invoke(goal);

    void IDebugEventSink.Visit(Solution newSolution) => this.solution = newSolution;

    void IDebugEventSink.Visit(Enter enter) => RaiseUnified(enter.Node.HeadGoal);

    void IDebugEventSink.Visit(Leave leave) => RaiseFailed(leave.Node.HeadGoal);
}