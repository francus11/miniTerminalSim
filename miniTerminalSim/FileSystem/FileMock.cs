using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim.FileSystem
{
    internal class FileMock : IFileSystemComponent
    {
        public string[] Content { get; set; }
        public string Name { get; set; }

        public string Path => throw new NotImplementedException();

        public Catalog Parent { get; set; }

        public FileMock(string name)
        {
            Name = name;
        }

        public FileMock(string name, string[] content)
        {
            Name = name;
            Content = content;
        }

        public object Clone()
        {
            FileMock file = new FileMock(Name);
            file.Content = Content;
            return file;
        }

        public int CompareTo(object? obj)
        {
            return Name.CompareTo(((IFileSystemComponent)obj).Name);
        }
    }
}
