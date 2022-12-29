using Battleship2000.ViewLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public string WindowTitle { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title} v{typeof(MainWindowViewModel).Assembly.GetName().Version}";
        public string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public string ProjectVersion { get; } = $"v{typeof(MainWindowViewModel).Assembly.GetName().Version}";
    }
}
