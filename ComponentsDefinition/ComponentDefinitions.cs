using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComponentsDefinition
{
    public interface IConsole
    {
        void writeLnWithDate(String line);
    }
    public interface IComputerAction
    {
        void addAction(String action);
    }
}
