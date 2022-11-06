namespace Prolog.Runtime;

public interface IDebugEventSink
{
    void Visit(Solution solution);

    void Visit(Enter enter);

    void Visit(Leave leave);
}
