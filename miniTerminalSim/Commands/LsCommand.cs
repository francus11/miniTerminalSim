using miniTerminalSim.FileSystem;

namespace miniTerminalSim.Commands
{
    internal class LsCommand : CommandAbstract
    {
        public LsCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

        public override string[] Execute()
        {
            Catalog catalog = null;

            if (args.Length == 0 || args == null)
            {
                catalog = fileExplorer.CurrentScope;
            }
            else
            {
                try
                {
                    catalog = SearchCatalogFromPath(PathLineToPath(args[0]));
                }
                catch
                {
                    string text = $"ls: {args[0]}: No such file or directory";
                    return new string[] { text };
                }
            }

            string content = "";

            foreach (var item in catalog.Components)
            {
                content += $"{item.Name}  ";
            }

            return new string[] { content };

            return null;
        }
    }
}
