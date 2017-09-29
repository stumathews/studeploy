using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComponentsDefinition;


namespace InterfaceDefinitions
{
    
    // All Plugins are the same in the following ways:
    public interface IPlugin
    {
      
        string Name { get; }
        string Version { get; }
        string Author { get; }

    };
    
    // When writing a ComputerSourcesPlugn, these agree to the following conventions
    // defined by this interface, IComputerSourcesPlugin

    public interface IComputerSourcesPlugin : IPlugin
    {
        
        void browseForComputers();
        void registerHostConsole(ref IConsole console);
    
        TreeNode getTreeNode();
        bool start();
        void registerComputerActions(IComputerAction actionForm );
        
        
    };

    public enum PluginType
    {
        ComputerSourcesPlugin,
        ComputerActionsPlugin,
        Unknown
    };

    [AttributeUsage(AttributeTargets.Class,Inherited = true)]
    public sealed class ThePluginAttribute: Attribute
    {
        private PluginType _Type;
        public ThePluginAttribute(PluginType T) { _Type = T; }
        public PluginType Type { get {return _Type; } }
    }
}