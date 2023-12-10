using miniTerminalSim.Commands;
using miniTerminalSim.FileSystem;
using System.Data;

namespace miniTerminalSim
{
    internal class Program
    {
        static private FileExplorer fileExplorer;


        static void Main(string[] args)
        {
            fileExplorer = MockFileExplorer();

            while (true)
            {
                string line = GetCommandLine();
                string command;
                string[] arguments;
                SeparateCommandLine(line, out command, out arguments);
                string[] output = InterpretCommandLine(command, arguments);
                if (output != null)
                {
                    foreach (string arg in output)
                    {
                        Console.WriteLine(arg);
                    }
                }
                
            }
        }

        static string Path
        {
            get
            {
                return fileExplorer.CurrentScope.Path;
            }
        }

        static string GetCommandLine()
        {
            Console.Write($"{Path}$ ");
            return Console.ReadLine();
        }

        static string[] InterpretCommandLine(string command, string[] args)
        {
            if (command == "cd")
            {
                var run = new CdCommand(fileExplorer, args);
                return run.Execute();
            }

            return null;
        }

        static void PrintLine(string line)
        {
            
        }

        //TODO Add cases to handle quotes
        static void SeparateCommandLine(string line, out string command, out string[] args)
        {
            string[] parts = line.Split(' ');
            command = parts[0];
            args = parts[1..];
        }

        static FileExplorer MockFileExplorer()
        {
            Catalog root = new Catalog("");
            root.Parent = root;

            var bin = new Catalog("bin");
            root.Add(bin);

            var home = new Catalog("home");
            var user1 = new Catalog("user1");
            home.Add(user1);
            root.Add(home);

            var textFile = new FileMock("text.txt");
            user1.Add(textFile);

            FileExplorer fileExplorer = new FileExplorer(root);
            fileExplorer.CurrentScope = home;

            return fileExplorer;
        }
    }
}