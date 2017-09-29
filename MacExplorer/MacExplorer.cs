using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using InterfaceDefinitions;
using ComponentsDefinition;
using System.Windows.Forms;
using ZeroconfService;
using System.Collections;
using System.ComponentModel;




namespace MacExplorer
{
    [ThePluginAttribute(PluginType.ComputerSourcesPlugin)]
    public class FindMac : IComputerSourcesPlugin
    {
        IConsole console;
        TreeNode node;
        #region IComputerSourcesPlugin Members

        ArrayList waitingAdd = new ArrayList();
        void IComputerSourcesPlugin.browseForComputers()
        {
            console.writeLnWithDate("Discovering filesharing Macs...");
            NetServiceBrowser nsBrowser = new NetServiceBrowser();
            nsBrowser.DidFindService += new NetServiceBrowser.ServiceFound(nsBrowser_DidFindService);           
            nsBrowser.SearchForService("_afpovertcp._tcp", "");
        }

        
        void nsBrowser_DidFindService(NetServiceBrowser browser, NetService service, bool moreComing)
        {
            
            service.StartMonitoring();
            if (moreComing)
            {
                waitingAdd.Add(service.Name);
            }
            else
            {
                while (waitingAdd.Count > 0)
                {
                    node.Nodes.Add(waitingAdd[0].ToString());
                    console.writeLnWithDate(waitingAdd[0].ToString());
                    waitingAdd.RemoveAt(0);
                }
            }
        }

       

        

        void IComputerSourcesPlugin.registerHostConsole(ref IConsole console)
        {
            this.console = console;
        }

        TreeNode IComputerSourcesPlugin.getTreeNode()
        {
            return node;
        }

        bool IComputerSourcesPlugin.start()
        {
            console.writeLnWithDate("Starting MacExplorer...");
            node = new TreeNode("Mac Explorer");
            return true;
        }

        void IComputerSourcesPlugin.registerComputerActions(IComputerAction actionForm)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPlugin Members

        string IPlugin.Name
        {
            get { return "Browse For Macs Plugin"; }
        }

        string IPlugin.Version
        {
            get { return "0.1"; }
        }

        string IPlugin.Author
        {
            get { return "Stuart Mathews"; }
        }

        #endregion

        
    }
}
