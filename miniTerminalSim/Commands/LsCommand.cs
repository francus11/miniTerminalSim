using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniTerminalSim.Commands
{
    internal class LsCommand : CommandAbstract
    {
        public LsCommand(FileExplorer fileExplorer, string[] args) : base(fileExplorer, args) { }

        public override string[] Execute()
        {
            if (args.Length == 0 || args == null)
            {
                string content = "";

                foreach(var item in fileExplorer.CurrentScope.Components)
                {
                    content += $"{item.Name}  ";
                }

                return new string[] { content };
            }

            return null;
        }
    }
}
