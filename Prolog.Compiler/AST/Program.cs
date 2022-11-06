namespace Prolog.AST;

public class Program
{
    public Program ()
    {
        DcgClauses = new DcgClause[0];
        Clauses = new Clause[0];
    }

    public Program (Clause [] clauses, ExternalPredicateDeclaration [] externalPredicateDeclarations = null) : this ()
    {
        this.Clauses = clauses;
        this.ExternalPredicates = externalPredicateDeclarations;
    }

    public Clause[] Clauses;

    public ExternalPredicateDeclaration [] ExternalPredicates;

    public DcgClause [] DcgClauses;
}