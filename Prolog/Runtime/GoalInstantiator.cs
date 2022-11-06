namespace Prolog.Runtime;

class GoalInstantiator : Compiled.IPredicateVisitor<Goal>
{
    Goal Compiled.IPredicateVisitor<Goal>.Visit(Compiled.PrologPredicate predicate) => new PrologGoal(predicate);

    Goal Compiled.IPredicateVisitor<Goal>.Visit(Compiled.ExternalPredicate predicate) => new ExternalGoal(predicate.Callback);
}