using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim.FileSystem
{
    internal interface IFileSystemComponent : ICloneable, IComparable
    {
        public string Name { get; set; }
        public string Path { get; }
        public Catalog Parent { get; set; }

    }
}
