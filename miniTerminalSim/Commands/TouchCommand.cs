using miniTerminalSim.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim.Commands
{
    internal class TouchCommand : CommandAbstract
    {
        public TouchCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

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

            FileMock newFile = new FileMock(PathLineToPath(args[0])[^1]);
            newFile.Parent = parentCatalog;
            if (!parentCatalog.Add(newFile))
            {
                string text = "mkdir: directory already exists";
                return new string[] { text };
            }

            return null;
        }
    }
}
