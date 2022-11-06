using System.Collections.Generic;

namespace Prolog.Runtime;

public interface ISolutionTreeNode : IEnumerable<ISolutionTreeNode>
{
    Variables Variables {get;}
    Goal HeadGoal { get; set; }

    ISolutionTreeNode this [string goalName] {get;}
}