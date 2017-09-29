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
using ComponentsDefinition;

namespace StuDeploy
{
    public partial class ConsoleForm : DockContent, IConsole
    {
        public ConsoleForm()
        {
            InitializeComponent();
            initConsole();
        }
        
        private void initConsole()
        {
            writeLnWithDate("Console Starting.");
            Output.ReadOnly = true;
            this.CloseButtonVisible = false;
        }
        public void writeLnWithDate(String line)
        {
            //Output.Text += line + System.Environment.NewLine;
            
            Output.AppendText(DateTime.Now+"\t"+ line + System.Environment.NewLine);
           
        }
        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            loadCustomIcon();
            
            
        }

        private void loadCustomIcon()
        {
            this.Icon = Icon.FromHandle(((Bitmap)consoleImages.Images["console"]).GetHicon());
        }

       
    }
}
