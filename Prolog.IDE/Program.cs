using System;
using System.IO;
using System.Windows.Forms;
using Prolog.Runtime;

namespace Prolog.IDE;

static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string [] args)
    {
        if(args.Length == 0 || !File.Exists(args[0]))
        {
            MessageBox.Show("Unable to open file!");
            return;
        }
        Compiled.Program program = GetProgram (args[0]);

        var externalPredicates = new [] {
            Concat.GetConcat (), Lexer.GetLexer (
            new StringReader ("test"))};

        program.SetExternalPredicateCallbacks (externalPredicates);

        var engine = new EngineInternals ();

        var events = engine.Run (program);

        Application.EnableVisualStyles ();            
        Application.SetCompatibleTextRenderingDefault (false);
        Application.Run (new MainForm(events));
    }

    private static Compiled.Program GetProgram(string fileName) 
        => new Compiler().Compile(fileName, new[]
        {
            Concat.GetConcat().Key,
            Lexer.GetExternalPredicateDeclaration ()
        });
}
