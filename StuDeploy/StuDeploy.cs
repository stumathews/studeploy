using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using InterfaceDefinitions;
using System.IO;
using System.Collections;
using StuDeploy.Properties;
using ComponentsDefinition;



namespace StuDeploy
{
    public partial class StuDeploy : Form
    {

        DiscoverComputers discoveryComputersForm;
        ConsoleForm console;
        ComputerActionsForm computerActions;
        ListComputersView listComputerView;
        // Interface Bridge
        public InterfaceBridge bridge;
        string pluginPath;
    

        public StuDeploy()
        {
            InitializeComponent();
            initializeMyObjects();
            initializeDockObjects();
            setDefaultLogLevel();

            // Now Actually Start the default behaviour, everything else will be invoked
            // through the interface.

            doDefaultStartBehaviour(); // over and above the initial presentation of the GUI already created within the Designer as this has already been initiated

        }

        private void initializeMyObjects()
        {
            
        }
        public void writeToStatusBar( String str ){
             status.Text = str;
        }
        private void doDefaultStartBehaviour()
        {
            // Report that we are ready.
           status.Text = "Ready.";           

                        
            // Wait for events.
        }


        private void logThis(string p)
        {
            //console.writeLnWithDate(p);
        }

        private void setDefaultLogLevel()
        {
            //defaultLogLevel = LogLevel.DEBUG;
        }

        private void initializeDockObjects()
        {
            console = new ConsoleForm();
            discoveryComputersForm = new DiscoverComputers();
            computerActions = new ComputerActionsForm();
            listComputerView = new ListComputersView();
            // The InterfaceBridge contains the addresses of the interface objects
            // I.E the GUI elements on the main interface.
            // We transfer this object around the application to access these objects when these
            // objects are out of scope in the current executing code - eg while in a plugin to access
            // the min interface like the status bar...

            bridge = new InterfaceBridge(ref discoveryComputersForm, ref computerActions, ref console, this);

            // Transfer the InterfaceBridge to the discoveryComputersForm: it can now access all objects
            // that are contained within the InterfaceBride Object.

            discoveryComputersForm.loadInterfaceBridge(ref bridge); // now discoveryComputersForm has access to all InterfaceObjects.
            discoveryComputersForm.startLoadingPlugins(); // we start this after we load the bridge as the bridge has objects we would need to access
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create all Dock Objects and add them to the main interface.
            // Also these dock objects are intialized with default values.
            prepareDockInterface();

            LoadCustomIcon();
            

           
            

        }

        private void LoadCustomIcon()
        {
            this.Icon = Icon.FromHandle(((Bitmap)images.Images["computer"]).GetHicon());
        }

        

        private void prepareDockInterface()
        {
            // Make sure the dock Panel covers the entire main form.
            theDockPanel.Dock = DockStyle.Fill;
            // Let it be grey in colour
            theDockPanel.BackColor = Color.Beige;
            // Bring it to front?
            theDockPanel.BringToFront();

            // Define Areas of the Panel:

            // Content to fill the Left hand side.

            // This will contain the potential sources of computers that
            // we can discover

            prepareComputerSourcesDock();

            // For each object that we can select, here will be the options/function
            // that can be performed on that computer object.

            prepareComputerActionsDock();

            // This is the console that provides visual feedback about the goings 
            // on in the product.

            prepareConsoleDock();

            // This is the centre page and content of the appliction form.


            prepareCentreStageDock();
        }

        private void prepareCentreStageDock()
        {

            listComputerView.ShowHint = DockState.Document;
            listComputerView.Name = "list Computers";
            listComputerView.TabText = "list Computers";
            listComputerView.Show(theDockPanel);
        }

        private void prepareConsoleDock()
        {
            console.ShowHint = DockState.DockBottom;
            console.Name = "Console";
            console.TabText = "Console";
            console.CloseButtonVisible = false;
            console.Show(theDockPanel);
        }

        private void prepareComputerActionsDock()
        {
            computerActions.ShowHint = DockState.DockRight;
            computerActions.Name = "Computer Actions";
            computerActions.TabText = "Computer Actions";
            computerActions.CloseButtonVisible = false;
            computerActions.Show(theDockPanel);
        }

        private void prepareComputerSourcesDock()
        {
            discoveryComputersForm.ShowHint = DockState.DockLeft;
            discoveryComputersForm.Name = "Computer Sources";
            //discoveryComputersForm.Icon
            discoveryComputersForm.TabText = "Computer Sources";
            discoveryComputersForm.CloseButtonVisible = false;

            // Expose a few things            

            discoveryComputersForm.Show(theDockPanel);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().Show();
        }

        private void loadPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            
        }

        

        enum LogLevel { INFO, WARN, DEBUG };

        private void toolStripButtonLoadPlugin_Click(object sender, EventArgs e)
        {
            // Call the same method we call when user selects file->load plugin
            loadPluginToolStripMenuItem_Click(sender, e);
        }
    }


}
