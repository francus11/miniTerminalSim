using System.IO;

namespace miniTerminalSim.FileSystem
{
    internal class Catalog : IFileSystemComponent
    {
        private List<IFileSystemComponent> _components = new List<IFileSystemComponent>();

        public string Name { get; set; }

        public List<IFileSystemComponent> Components { get { return new List<IFileSystemComponent>(_components); } }

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

            _components.Add(item);
            item.Parent = this;
            _components.Sort();
            return true;
        }

        public bool Remove(IFileSystemComponent component)
        {
            try
            {
                _components.Remove(Select(component.Name));
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            
        }

        public IFileSystemComponent Select(string name)
        {
            try
            {
                return _components.Where(c => c.Name == name).First();
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }

        public bool Contains(IFileSystemComponent component)
        {
            return _components.Contains(component);
        }

        public bool Contains(string name)
        {
            return _components.Any(component => component.Name == name);
        }

        public object Clone()
        {
            Catalog catalog = new Catalog(Name);

            foreach (var item in _components)
            {
                catalog.Add((IFileSystemComponent)item.Clone());
            }

            return catalog;
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
