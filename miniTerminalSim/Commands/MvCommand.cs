using miniTerminalSim.FileSystem;

namespace miniTerminalSim.Commands
{
    internal class MvCommand : CommandAbstract
    {
        public MvCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

        public override string[] Execute()
        {
            if (args.Length < 2)
            {
                string text = "mv: missing file operand";
                return new string[] { text };
            }

            Catalog catalog1 = null;
            IFileSystemComponent movedObject = null;

            try
            {
                string[] convertedPath = PathLineToPath(args[0]);
                convertedPath = convertedPath[..^1];
                catalog1 = SearchCatalogFromPath(convertedPath);
                movedObject = catalog1.Select(PathLineToPath(args[0])[^1]);
                catalog1.Remove(movedObject);
            }
            catch (InvalidCastException)
            {
                string text = "mv: can't find file or directory";
                return new string[] { text };
            }
            catch (FileNotFoundException)
            {
                string text = "mv: can't find file or directory";
                return new string[] { text };
            }


            Catalog catalog2 = null;

            try
            {
                catalog2 = SearchCatalogFromPath(PathLineToPath(args[1]));
            }
            catch (InvalidCastException)
            {
                string text = "mv: can't find file or directory";
                return new string[] { text };
            }
            catch (FileNotFoundException)
            {
                string text = "mv: can't find file or directory";
                return new string[] { text };
            }

            if (!catalog2.Add(movedObject))
            {
                catalog2.Remove(movedObject);
                catalog2.Add(movedObject);
                movedObject.Parent = catalog2;
            }

            return null;
        }
    }
}
