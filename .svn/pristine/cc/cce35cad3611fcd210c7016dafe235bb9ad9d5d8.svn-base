using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InterfaceDefinitions;
using ComponentsDefinition;
using System.Windows.Forms;

namespace ComputerNamesPlugin
{
    [ThePluginAttribute(PluginType.ComputerSourcesPlugin)]
    public class ComputerNamesPlugin : IComputerSourcesPlugin
    {
        TreeNode treeNode;
        public ComputerNamesPlugin()
        {

            treeNode = new TreeNode(this.Name);
            
          
        }
        
        public void registerComputerActions(IComputerAction actionForm) { }
        IConsole console;
        #region IComputerSourcesPlugin Members

        public void browseForComputers()
        {
            
        }

        public void registerHostConsole(ref ComponentsDefinition.IConsole console)
        {
            this.console = console;
        }

        public System.Windows.Forms.TreeNode getTreeNode()
        {
            //throw new NotImplementedException();
            return treeNode;
        }

        public bool start()
        {
            //throw new NotImplementedException();
           this.console.writeLnWithDate("Starting "+Name);
            
            return true;
        }

        #endregion

        #region IPlugin Members

        public string Name
        {
            get { return "Computer Names Explorer"; }
        }

        public string Version
        {
            get { throw new NotImplementedException(); }
        }

        public string Author
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
