using miniTerminalSim.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim.Commands
{
    internal class MoreCommand : CommandAbstract
    {
        private int linesToDisplay = 20;
        public MoreCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

        public override string[] Execute()
        {
            if (args.Length == 0 || args == null)
            {
                string text = "more: missing arguments";
                return new string[] { text };
            }
            else if (args.Length == 1)
            {
                Catalog catalog1 = null;
                FileMock file = null;
                try
                {
                    catalog1 = SearchCatalogFromPath(PathLineToPath(args[0])[..^1]);
                    file = (FileMock)catalog1.Select(PathLineToPath(args[0])[^1]);
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

                return file.Content;
            }

            return null;
        }
    }
}
