using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceDefinitions;
using System.Windows.Forms;
using StuDeploy;
using ComponentsDefinition;
using System.IO;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;
using System.Drawing;



namespace BrowseActiveDirectoryPlugin
{
    public class ADNode : TreeNode
    {
        public bool have_init_problem;
        public String lastError;
        public ADNode()
        {
            // default Statup action
           
            Initialize();
            try
            {
                AdImages = new ImageList();
                AdImages.Images.Add("ad",Image.FromFile("image.png"));
                AdImages.Images.Add("programgroup",Image.FromFile("image.png"));
                AdImages.Images.Add("openfolder",Image.FromFile("image.png"));
                AdImages.Images.Add("closedfolder",Image.FromFile("image.png"));
                AdImages.Images.Add("pc",Image.FromFile("image.png"));

                    
                ourListView = new ListView();
                this._AdRootDSE = new DirectoryEntry("LDAP://rootDSE");
                this._AdRoot = new DirectoryEntry("LDAP://" + (string)this._AdRootDSE.Properties["defaultNamingContext"].Value);
                /*
                foreach(string property in this._AdRoot.Properties.PropertyNames)
                {
                    MessageBox.Show(property + " = " + this._AdRoot.Properties[property].Value);
                }*/

                //Text = (string)this._AdRootDSE.Properties["defaultNamingContext"].Value;
                Text = Domain.GetComputerDomain().Name;
                Name = Text;
                ImageIndex = AdImages.Images.IndexOfKey("ad");
                SelectedImageIndex = AdImages.Images.IndexOfKey("ad");

                Tag = this._AdRoot;
                // Now Populate the socks off this TreeNode
                populate_node(_AdRoot, this);
            }
            catch( Exception ex )
            {
                // unload this plugin.
                have_init_problem = true;
                lastError = ex.Message;
                
                
            }



        }

        public void populate_node(DirectoryEntry entry, TreeNode node)
        {
            ourListView.Items.Clear();
            /*
            if (ourListView.Columns.ContainsKey("Name") == false)
            {
                ourListView.Columns.Add("Name", "Name");
                
            }
            */
            foreach (DirectoryEntry child in entry.Children)
            {
                TreeNode tmpNode = null;
                ListViewItem tmpItem = null;

                switch (child.SchemaClassName)
                {
                    case "organizationalUnit":
                        tmpNode = new TreeNode((string)child.Properties["name"].Value, AdImages.Images.IndexOfKey("programgroup"), AdImages.Images.IndexOfKey("openfolder"));
                        tmpNode.Name = (string)child.Properties["name"].Value;
                        //tmpNode.Name = null;
                        tmpItem = new ListViewItem(new string[] {
                                                                                                                                                (string)child.Properties["name"].Value,
                                                                                                                                                child.SchemaClassName,
                                                                                                                                                (string)child.Properties["description"].Value

                                                                                                                                        }, 
                                                                                                                                        AdImages.Images.IndexOfKey("programgroup"));
                        break;
                    case "container":
                        tmpNode = new TreeNode((string)child.Properties["name"].Value, AdImages.Images.IndexOfKey("closedfolder"), AdImages.Images.IndexOfKey("openfolder"));
                        tmpNode.Name = (string)child.Properties["name"].Value;
                        tmpItem = new ListViewItem(new string[] {
                                                                                                                                                (string)child.Properties["name"].Value,
                                                                                                                                                child.SchemaClassName,
                                                                                                                                                (string)child.Properties["description"].Value

                                                                                                                                        }, AdImages.Images.IndexOfKey("closedfolder"));
                        break;
                    case "computer":
                        // use it now but later Dont add it as node - use the tmpItem later to add it to list view
                        /*
                        tmpNode = new TreeNode((string)child.Properties["name"].Value, AdImages.Images.IndexOfKey("pc"), AdImages.Images.IndexOfKey("pc"));
                        tmpNode.Name = (string)child.Properties["name"].Value;
                         */
                        tmpNode = null; // dont add it as a node
                        tmpItem = new ListViewItem(new string[] { (string)child.Properties["name"].Value, "", "", "" }, AdImages.Images.IndexOfKey("pc"));
                        tmpItem.Name = (string)child.Properties["name"].Value; // temp

                        break;
                    case "user":
                        /* // lets hide users
                        tmpNode = new TreeNode((string)child.Properties["name"].Value, AdImages.Images.IndexOfKey("user"), AdImages.Images.IndexOfKey("user"));
                        tmpNode.Name = (string)child.Properties["name"].Value;
                        tmpItem = new ListViewItem(new string[] {
                                                                                                                                                (string)child.Properties["name"].Value,
                                                                                                                                                child.SchemaClassName,
                                                                                                                                                (string)child.Properties["description"].Value

                                                                                                                                        });
                         */
                        break;
                    case "group":
                        /* // lets hide groups
                        tmpNode = new TreeNode((string)child.Properties["name"].Value, AdImages.Images.IndexOfKey("group"), AdImages.Images.IndexOfKey("group"));
                        tmpNode.Name = (string)child.Properties["name"].Value;
                        tmpItem = new ListViewItem(new string[] {
                                                                                                                                                (string)child.Properties["name"].Value,
                                                                                                                                                child.SchemaClassName,
                                                                                                                                                (string)child.Properties["description"].Value

                                                                                                                                        } );
                         */
                        break;
                    default:
                        // Lets not show other things as a node
                        /*
                        tmpNode = new TreeNode((string)child.Properties["name"].Value, AdImages.Images.IndexOfKey("item"), AdImages.Images.IndexOfKey("item"));
                        tmpNode.Name = (string)child.Properties["name"].Value;
                        tmpItem = new ListViewItem(new string[] {
                                                                                                                                                (string)child.Properties["name"].Value,
                                                                                                                                                child.SchemaClassName,
                                                                                                                                                (string)child.Properties["description"].Value

                                                                                                                                        });
                         */
                        break;
                }
                // save the directory entry reference in the tag
                if (tmpNode != null)
                {
                    tmpNode.Tag = child;
                    //Nodes.Add(tmpNode);

                    if (node.Nodes.ContainsKey(tmpNode.Name) == false)
                    {

                        node.Nodes.Add(tmpNode);
                    }

                }
                if (tmpItem != null)
                {

                    if (ourListView.Items.ContainsKey(tmpItem.Name) == false)
                    {
                        if (child.SchemaClassName == "computer")
                        {
                            tmpItem.SubItems.Add(""); // computer status
                            tmpItem.SubItems.Add(""); // version
                            ourListView.Items.Add(tmpItem);
                        }
                    }


                }

            }

        }

        private void Initialize()
        {
            have_init_problem = false;
        }

        // members
        DirectoryEntry _AdRootDSE;
        DirectoryEntry _AdRoot;
        ImageList AdImages;
        ListView ourListView;
    }

    [ThePluginAttribute(PluginType.ComputerSourcesPlugin)]
    public class BrowseActiveDirectoryPlugin : IComputerSourcesPlugin
    {
        // We want to be able to use the Programs Console
        IConsole console;
        ADNode node;
        String currentADDomain;
        public void registerComputerActions(IComputerAction actionForm) { }
        public BrowseActiveDirectoryPlugin()
        {
            // First thing we do is initialize our object that we are goinf to be using...
            node = new ADNode();
            
        }
        public void registerComputerActions() { }
        public TreeNode getTreeNode()
        {
            
            return node;
        }
        #region IComputerSourcesPlugin Members

        public void browseForComputers()
        {
            // Ok lets look for the current domain
            try
            {
                currentADDomain = Domain.GetCurrentDomain().Name;
                console.writeLnWithDate("Currently joined to " + currentADDomain + " AD Domain.");
                // See if we have ay child domains...
                if (Domain.GetComputerDomain().Children.Count > 0)
                {
                    console.writeLnWithDate("Found the following child domains:");
                    foreach (Domain domain in Domain.GetComputerDomain().Children)
                    {
                        console.writeLnWithDate(domain.Name);
                    }
                }
            }
            catch (ActiveDirectoryOperationException adex)
            {
                console.writeLnWithDate(adex.Message);
            }
              

        }

        public bool start()
        {
            // OK, setting up myself...
            if (node.have_init_problem == true)
            {
                console.writeLnWithDate("Problem loading this plugin: "+this.Name);
                console.writeLnWithDate(node.lastError);                
                return false;
            }
            else
            {
                console.writeLnWithDate("Successfully loaded this plugin: "+this.Name);
                return true;
            }
            
        }

        #endregion

        
        #region IPlugin Members

        public string Name
        {
            get { return "BrowseADPlugin"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public string Author
        {
            get { return "Stuart Mathews"; }
        }

        #endregion

        #region BrowseActiveDirecotryPlugin Members

        

        #endregion

        #region IComputerSourcesPlugin Members



        #endregion

        #region IComputerSourcesPlugin Members


        public void registerHostConsole(ref IConsole console)
        {
            this.console = console;            
        }

        #endregion
    }
}
