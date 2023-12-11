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

            return null;
        }
    }
}
