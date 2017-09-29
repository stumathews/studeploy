using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ComponentsDefinition;



namespace StuDeploy
{
    public partial class ComputerActionsForm : DockContent, IComputerAction
    {
        public ComputerActionsForm()
        {
            InitializeComponent();
        }
        public void addAction(String action)
        {
            
            Button button = new Button();
            button.Text = action;
            this.Controls.Add(button);
            this.Controls.Add(button);
        }
        private void ComputerActionsForm_Load(object sender, EventArgs e)
        {
            loadCustomIcon();
            
            
        }
        public void test(){
            
            theTreeView.Nodes.Add("Test");
        }
        private void loadCustomIcon()
        {
            this.Icon = Icon.FromHandle(((Bitmap)computerActionsImages.Images["action"]).GetHicon());
        }
    }
}
