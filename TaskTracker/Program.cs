using TaskTracker.Services;

namespace TaskTracker;

public sealed class Program
{
    private static void Main(string[] args)
    {
        ConsoleProvider consoleProvider = new ConsoleProvider();
        consoleProvider.RunProgram();
    }
}