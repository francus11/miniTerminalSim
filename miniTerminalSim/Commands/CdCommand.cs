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
            if (args == null || args.Length == 0)
            {
                return null;
            }

            if (args.Length > 1)
            {
                string text = "Too many arguments";
                return new string[] { text };
            }

            string[] path = args[0].Split("/");
            Catalog catalog;

            try
            {
                catalog = SearchCatalogFromPath(path);
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
