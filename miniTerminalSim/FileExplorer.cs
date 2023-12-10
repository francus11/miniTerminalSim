using miniTerminalSim.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim
{
    internal class FileExplorer
    {
        public Catalog RootCatalog { get; private set; }

        // May occur errors with CurrentScope not being in tree of RootCatalog
        public Catalog CurrentScope { get; set; }

        public FileExplorer(Catalog rootCatalog)
        {
            RootCatalog = rootCatalog;
            CurrentScope = rootCatalog;
        }
    }
}
