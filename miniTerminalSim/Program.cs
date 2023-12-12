using miniTerminalSim.Commands;
using miniTerminalSim.FileSystem;

namespace miniTerminalSim
{
    internal class Program
    {
        static private FileExplorer fileExplorer;

        static private Dictionary<string, Func<FileExplorer, string[], CommandAbstract>> commands = new Dictionary<string, Func<FileExplorer, string[], CommandAbstract>>()
        {
            { "cd", (explorer, arguments) => new CdCommand(explorer, arguments) },
            { "cp", (explorer, arguments) => new CpCommand(explorer, arguments) },
            { "mv", (explorer, arguments) => new MvCommand(explorer, arguments) },
            { "ls", (explorer, arguments) => new LsCommand(explorer, arguments) },
            { "mkdir", (explorer, arguments) => new MkdirCommand(explorer, arguments) },
            { "touch", (explorer, arguments) => new TouchCommand(explorer, arguments) },
            { "tree", (explorer, arguments) => new TreeCommand(explorer, arguments) }
        };


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
            /*if (command == "cd")
            {
                var run = new CdCommand(fileExplorer, args);
                return run.Execute();
            }*/

            CommandAbstract execCommand = null;
            try
            {
                execCommand = commands[command](fileExplorer, args);
            }
            catch
            {
                return null;
            }

            return execCommand.Execute();

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

            var rootFile = new FileMock("rootFile.bin");
            root.Add(rootFile);

            var bin = new Catalog("bin");
            root.Add(bin);

            var home = new Catalog("home");
            var user1 = new Catalog("user1");
            home.Add(user1);
            root.Add(home);

            var textFile = new FileMock("text.txt");
            var textFile2 = new FileMock("text2.txt");
            var textFile3 = new FileMock("text3.txt");
            user1.Add(textFile);
            user1.Add(textFile2);
            user1.Add(textFile3);

            FileExplorer fileExplorer = new FileExplorer(root);
            fileExplorer.CurrentScope = home;

            return fileExplorer;
        }
    }
}