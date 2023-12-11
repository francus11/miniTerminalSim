using miniTerminalSim.FileSystem;

namespace miniTerminalSim.Commands
{
    internal class CdCommand : CommandAbstract
    {
        public CdCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args)
        {

        }

        public override string[] Execute()
        {
            if (args == null)
            {
                return null;
            }

            if (args.Length > 1)
            {
                string text = "Too many arguments";
                return new string[] { text };
            }

            if (args[0] == "..")
            {
                fileExplorer.CurrentScope = fileExplorer.CurrentScope.Parent;
            }

            string[] path = args[0].Split("/");
            Catalog catalog;

            if (path[0] == "")
            {
                catalog = fileExplorer.RootCatalog;
                path = path[1..];
            }
            else
            {
                catalog = fileExplorer.CurrentScope;
            }

            try
            {
                foreach (string pathItem in path)
                {

                    catalog = (Catalog)catalog.Select(pathItem);
                }
            }
            catch (FileNotFoundException)
            {
                string text = $"cd: {args[0]}: No such file or directory";
                return new string[] { text };
            }
            catch(InvalidCastException)
            {
                string text = $"cd: {args[0]}: Not a directory";
                return new string[] { text };
            }

            fileExplorer.CurrentScope = catalog;

            return null;
        }
    }
}
