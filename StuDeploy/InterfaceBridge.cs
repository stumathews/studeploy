using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StuDeploy
{
    public class InterfaceBridge
    {
        public DiscoverComputers discoveryComputers;
        public ComputerActionsForm computerActions;
        public ConsoleForm console;
        public StuDeploy mainInterface;

        public InterfaceBridge(ref DiscoverComputers dc, ref ComputerActionsForm ca, ref ConsoleForm console,StuDeploy main)
        {
            this.discoveryComputers = dc;
            this.computerActions = ca;
            this.console = console;
            mainInterface = main;
        }
        public void writeToStatusBar(String str)
        {
            
        }
    }
}
