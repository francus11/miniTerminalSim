using miniTerminalSim.FileSystem;

namespace miniTerminalSim.Commands
{
    internal class MkdirCommand : CommandAbstract
    {
        public MkdirCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

        public override string[] Execute()
        {
            if (args == null || args.Length == 0)
            {
                string text = "mkdir: missing operand";
                return new string[] { text };
            }
            Catalog parentCatalog;
            string[] path = PathLineToPath(args[0][..^1]);
            if (path[..^1] == null || path[..^1].Length == 0)
            {
                parentCatalog = fileExplorer.CurrentScope;
            }
            else
            {

                parentCatalog = SearchCatalogFromPath(PathLineToPath(args[0])[..^1]);
            }

            Catalog newCatalog = new Catalog(PathLineToPath(args[0])[^1]);
            newCatalog.Parent = parentCatalog;
            if (!parentCatalog.Add(newCatalog))
            {
                string text = "mkdir: directory already exists";
                return new string[] { text };
            }

            return null;
        }
    }
}
