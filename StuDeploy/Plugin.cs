using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceDefinitions;
using System.IO;
using System.Reflection;

namespace StuDeploy
{
    // A Plugin that can identify itself as a certain type of plugin, that when identified can be cast to the correct type
    class Plugin : IPlugin
    {
        
        public Plugin(string Path)
        {
            setPlugin(Path);
        }        

        // Helper function to help identify the kind of plugin loaded from the .dll file.
        public static Type GetTypeFromEnum(PluginType T)
        {
            switch (T)
            {
                case PluginType.ComputerActionsPlugin:
                    return typeof(IComputerSourcesPlugin);
                case PluginType.ComputerSourcesPlugin:
                    return typeof(IComputerSourcesPlugin);
                default:
                    return typeof(IPlugin);
            }
        }
        public static string GetTypenameFromEnum(PluginType T)
        {
            switch (T)
            {
                case PluginType.ComputerActionsPlugin:
                    return "ComputerActionsPlugin";
                case PluginType.ComputerSourcesPlugin:
                    return "ComputerSourcesPlugin";
                default:
                    return "Unknown";
            }
        }
        public static char GetTypecharFormEnum(PluginType T)
        {
            switch (T)
            {
                case PluginType.ComputerActionsPlugin:
                    return 'A';
                case PluginType.ComputerSourcesPlugin:
                    return 'S';
                default:
                    return '?';
            }
        }

        // local data members.
        private IPlugin interalPlugin;
        private string myPath;
        private PluginType myType = PluginType.Unknown;
        public string Path { get { return myPath; } }
        public string Filename { get { return new FileInfo(myPath).Name; } }
        public PluginType Type
        {
            get
            {
                if (interalPlugin == null)
                {
                    return PluginType.Unknown;
                }
                return myType;
            }
        }
        public string Name
        {
            get
            {
                if (interalPlugin == null)
                {
                    return "Not a recognized plugin.";
                }
                return interalPlugin.Name;
            }
        }
        public string Version
        {
            get
            {
                if (interalPlugin == null)
                {
                    return "";
                }
                return interalPlugin.Version;
            }
        }
        public string Author
        {
            get
            {
                if (interalPlugin == null)
                {
                    return "";
                }
                return interalPlugin.Author;
            }
        }
        public IPlugin PluginInterface { get { return interalPlugin; } }
        public override string ToString()
        {
            return string.Format("{0}: {1}", GetTypecharFormEnum(myType), myPath);
        }

        // Take the filename, load it as an assembly object, determine if the classes found within have the 'ThePluginAttribute'
        // if so, set the plugin type.
        public bool setPlugin(string PluginFile)
        {
            Assembly asm;
            PluginType pt = PluginType.Unknown;
            Type PluginClass = null;

            if (!File.Exists(PluginFile))
            {
                return false;
            }
            asm = Assembly.LoadFile(PluginFile);

            if (asm != null)
            {
                myPath = PluginFile;
                foreach (Type type in asm.GetTypes())
                {
                   
                    if (type.IsAbstract) continue;
                    
                    //
                    
                    object[] attrs = type.GetCustomAttributes(typeof(ThePluginAttribute), true);
                    //object[] attrs = type.GetCustomAttributes(true);
                    if (attrs.Length > 0)
                    {
                        
                        foreach (ThePluginAttribute pa in attrs)
                        //foreach (
                        {
                            pt = pa.Type;
                        
                            
                        }
                        PluginClass = type;
                        //To support multiple plugins in a single assembly, modify this
                    }
                }
                if (pt == PluginType.Unknown)
                {
                    return false;
                }

                //Get the plugin
               interalPlugin = Activator.CreateInstance(PluginClass) as IPlugin;
                myType = pt;
                return true;
            }
            return false; 
        }

        }
    }


