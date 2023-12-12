using miniTerminalSim.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim.Commands
{
    internal class CpCommand : CommandAbstract
    {
        public CpCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

        public override string[] Execute()
        {
            if (args.Length < 2)
            {
                string text = "cp: missing file operand";
                return new string[] { text };
            }

            Catalog catalog1 = null;
            IFileSystemComponent copiedObject = null;


            try
            {
                catalog1 = SearchCatalogFromPath(PathLineToPath(args[0])[..^1]);
                copiedObject = (IFileSystemComponent)catalog1.Select(PathLineToPath(args[0])[^1]).Clone();
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
                catalog2 = SearchCatalogFromPath(PathLineToPath(args[1])[..^1]);
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

            if (!catalog2.Add(copiedObject))
            {
                catalog2.Remove(copiedObject);
                catalog2.Add(copiedObject);
                copiedObject.Parent = catalog2;
                copiedObject.Name = PathLineToPath(args[1])[-1];
            }

            return null;
        }
    }
}
