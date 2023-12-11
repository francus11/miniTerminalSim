using miniTerminalSim.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            try
            {
                Catalog catalog1 = null;

                try
                {
                    catalog1 = SearchCatalogFromPath(PathLineToPath(args[0])[..-1]);
                }
                catch (InvalidCastException)
                {
                    throw;
                }
                catch (FileNotFoundException)
                {
                    throw;
                }

                IFileSystemComponent movedObject = catalog1.Select(PathLineToPath(args[0])[-1]);

                Catalog catalog2 = null;

                try
                {
                    catalog2 = SearchCatalogFromPath(PathLineToPath(args[1]));
                }
                catch (InvalidCastException)
                {
                    throw;
                }
                catch (FileNotFoundException)
                {
                    throw;
                }

                if (!catalog2.Add(movedObject))
                {
                    catalog2.Remove(movedObject);
                    catalog2.Add(movedObject);
                }

                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
