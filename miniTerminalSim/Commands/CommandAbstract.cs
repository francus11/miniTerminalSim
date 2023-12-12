using miniTerminalSim.FileSystem;
using System.Xml.XPath;

namespace miniTerminalSim.Commands
{
    internal abstract class CommandAbstract
    {
        protected FileExplorer fileExplorer;
        protected string[] args;

        public CommandAbstract(FileExplorer fileExplorer, string[] args)
        {
            this.fileExplorer = fileExplorer;
            this.args = args;
        }

        public abstract string[] Execute();

        protected Catalog SearchCatalogFromPath(string[] path)
        {
            if (path.Length == 1 && path[0] == "/")
            {
                return fileExplorer.RootCatalog;
            }
            Catalog catalog;

            if (path.Length == 0)
            {
                return fileExplorer.RootCatalog;
            }

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
                    if (pathItem == "..")
                    {
                        catalog = catalog.Parent;
                    }
                    else
                    {
                        catalog = (Catalog)catalog.Select(pathItem);
                    }
                }
            }
            catch (InvalidCastException)
            {
                throw;
            }
            catch(FileNotFoundException)
            {
                throw;
            }

            return catalog;
        }

        protected string[] PathLineToPath(string pathLine)
        {
            if (pathLine == "/")
            {
                return new string[] { "/" };
            }
            return pathLine.Split("/");
        }
    }
}
