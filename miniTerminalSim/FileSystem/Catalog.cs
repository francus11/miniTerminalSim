using System.IO;

namespace miniTerminalSim.FileSystem
{
    internal class Catalog : IFileSystemComponent
    {
        List<IFileSystemComponent> components = new List<IFileSystemComponent>();

        public string Name { get; set; }

        public string Path
        {
            get
            {
                /*List<string> path = new List<string> { Name };
                while (true)
                {
                    var parentName  = Parent;
                    if (Parent == this)
                    {
                        break;
                    }

                    path.Add(Parent.Name);

                }

                path.Reverse();*/

                string path = UntrimmedPath();
                if (path.StartsWith("//"))
                {
                    path = "/" + path.Substring(2);
                }

                return path;

            }
        }

        public Catalog Parent { get; set; }

        public Catalog(string name)
        {
            Name = name;
        }

        public bool Add(IFileSystemComponent item)
        {
            if (Contains(item.Name))
            {
                return false;
            }

            components.Add(item);
            item.Parent = this;
            components.Sort();
            return true;
        }

        public bool Remove(IFileSystemComponent component)
        {
            if (components.Any(component => component.Name == component.Name))
            {
                return false;
            }

            components.Add(component);
            return true;
        }

        public IFileSystemComponent Select(string name)
        {
            try
            {
                return components.Where(c => c.Name == name).First();
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }

        public bool Contains(IFileSystemComponent component)
        {
            return components.Contains(component);
        }

        public bool Contains(string name)
        {
            return components.Any(component => component.Name == name);
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object? obj)
        {
            return Name.CompareTo(((IFileSystemComponent)obj).Name);
        }

        private string UntrimmedPath()
        {
            if (Parent == this)
            {
                return $"/{Name}";
            }
            else
            {
                return $"{Parent.UntrimmedPath()}/{Name}";
            }
        }
    }
}
