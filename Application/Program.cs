// LFInteractive LLC. - All Rights Reserved

using Serilog;

namespace ChaseWebComplier.Application;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        // Do shit here
        if (args.Any())
        {
            Environment.CurrentDirectory = Path.GetFullPath(args[0]);
        }

        Log.CloseAndFlush();
    }
}