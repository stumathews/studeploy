using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using StuDeploy.Properties;
using System.Collections;
using System.IO;
using InterfaceDefinitions;
using ComponentsDefinition;


namespace StuDeploy
{
    public partial class DiscoverComputers : DockContent
    {
        OpenFileDialog browseForPlugin;
        FolderBrowserDialog browseForPluginPath;
        Hashtable listOfPlugins;
        public ConsoleForm console;
        String pluginPath;
      
        public InterfaceBridge bridge;
        public DiscoverComputers()
        {
            InitializeComponent();
            initTreeView();
            loadMyComponents();            
            
        }
        public void startLoadingPlugins()
        {
            loadPlugins();
          
        }
        public void loadInterfaceBridge(ref InterfaceBridge bridge)
        {
            this.bridge = bridge;
            this.bridge.console.writeLnWithDate("Interface bridge initialized.");
            this.console = bridge.console;
            bridge.computerActions.test();
           
        }
        private void loadMyComponents()
        {
            
            console = new ConsoleForm();           
            
            listOfPlugins = new Hashtable();
            
            browseForPlugin = new OpenFileDialog();
            
        }

        private void loadPlugins()
        {
            // Load Plugins in the current directory by default
            getPluginsByPath(System.Environment.CurrentDirectory);
            
        }

        private void setPluginPath(string path)
        {
            
            browseForPluginPath = new FolderBrowserDialog();
            browseForPluginPath.ShowDialog();
            if (Directory.Exists(path))
            {
                pluginPath = browseForPluginPath.SelectedPath;
                console.writeLnWithDate("Getting list of plugins...");
                getPluginsByPath(pluginPath);
                
                
            }
        }

 

        private void getPluginsByPath(string pluginPath)
        {
            if (!Directory.Exists(pluginPath))
            {
                console.writeLnWithDate("Plugin path does not exist.");
                return;
            }




            foreach (string f in Directory.GetFiles(pluginPath))
            {

                getSinglePlugin(f);

            }



        }

        private bool getSinglePlugin(string f)
        {
            FileInfo fi = new FileInfo(f);
            if (fi.Extension.Equals(".dll"))
            {
                // add the fileinfo object to an list.
                //listOfPlugins.Add(new Plugin(f));
                Plugin plugin = new Plugin(f);

                switch (plugin.Type)
                {
                    case PluginType.ComputerSourcesPlugin:
                        // Perform typical ComputerSourcec Plugin duties
                        // Give access the plugin access to the Console.
                        IConsole theConsole = console as IConsole;
                        (plugin.PluginInterface as IComputerSourcesPlugin).registerHostConsole(ref theConsole);                        
                        // Let the plugin set itself up.
                        if ((plugin.PluginInterface as IComputerSourcesPlugin).start() == false)
                        {
                            console.writeLnWithDate("System could not load plugin : " + (plugin.PluginInterface as IComputerSourcesPlugin).Name);
                            return false; // DIE
                        }
                       
                        // Add the plugin as a TreeNode (All Plugins should be added to the TreeNode)

                        if (loadNode((plugin.PluginInterface as IComputerSourcesPlugin).getTreeNode()) == false)
                        {
                            console.writeLnWithDate("Couldn't add node to ComputerSources");
                            return false;
                        }
                        // Tell the plugin to start populating itself
                        (plugin.PluginInterface as IComputerSourcesPlugin).browseForComputers();
                        // Give the plugin access to the copmuter actions
                        //(plugin.PluginInterface as IComputerSourcesPlugin).registerComputerActions( ComputerAc
                        // its all good, store reference to that plugin
                        listOfPlugins.Add((plugin).Name, (plugin.PluginInterface as IComputerSourcesPlugin));
                        return true;
                        break;
                    case PluginType.ComputerActionsPlugin:
                        return false;
                        break;
                    default:
                        return false;
                }




            }
            return false;
        }
        private void initTreeView()
        {
            theTreeView.Nodes.Add("Computers");
            
           
        }

        private void DiscoverComputers_Load(object sender, EventArgs e)
        {
            loadCustomIcon();
            
        }
        public bool loadNode( TreeNode node ){
            
            try
            {
                theTreeView.Nodes[0].Nodes.Add(node);
            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }
        private void loadCustomIcon()
        {
            this.Icon = Icon.FromHandle(((Bitmap)computerSourcesImages.Images["computer"]).GetHicon());
        }

        private void theTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            // We've  got to come up with a solution whereby this event can
            // be channeled/routed to the correct plugin/treenode.
           // MessageBox.Show(e.Node.Text);
            route_node_click(e);
            
        }

        private void route_node_click(TreeViewCancelEventArgs e)
        {
            // Go through each plugin we have, find the one that matches this name
  
            if (listOfPlugins.Contains(e.Node.Text))
            {
                (listOfPlugins[e.Node.Text] as IComputerSourcesPlugin).start();
            }
        }
    }
}
