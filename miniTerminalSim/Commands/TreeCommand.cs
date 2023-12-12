using miniTerminalSim.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim.Commands
{
    internal class TreeCommand : CommandAbstract
    {
        public TreeCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

        public override string[] Execute()
        {
            Catalog catalog = null;
            if (args == null || args.Length == 0)
            {
                catalog = fileExplorer.CurrentScope;
            }
            else
            {
                try
                {
                    catalog = SearchCatalogFromPath(PathLineToPath(args[0]));
                }
                catch (InvalidCastException)
                {
                    string text = "tree: not a directory";
                    return new string[] { text };
                }
                catch (FileNotFoundException)
                {
                    string text = "tree: can't find file or directory";
                    return new string[] { text };
                }
            }
            return ListTree(catalog, 0);
        }

        private string[] ListTree(Catalog catalog, int deep)
        {
            List<string> tree = new List<string>();
            foreach (IFileSystemComponent component in catalog.Components)
            {
                string line = RepeatString("│  ", deep);
                tree.Add(line + $"├──{component.Name}");
                if (component is Catalog)
                {
                    tree.AddRange(ListTree(component as Catalog, deep + 1));
                }
            }

            return tree.ToArray();
        }

        private string RepeatString(string toInsert, int n)
        {
            StringBuilder result = new StringBuilder("");

            for (int i = 0; i < n; i++)
            {
                result.Append(toInsert);
            }

            return result.ToString();
        }
    }
}
