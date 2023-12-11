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

            //string[] path = pathLine.Split("/");
            Catalog catalog;

            if (path[0] == "")
            {
                catalog = fileExplorer.RootCatalog;
                path = path[1..];
            }
            else
            {
                catalog = fileExplorer.CurrentScope;
                return null;
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
            return pathLine.Split("/");
        }
    }
}
