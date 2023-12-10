using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
