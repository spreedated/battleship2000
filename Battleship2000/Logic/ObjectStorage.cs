using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Logic
{
    internal static class ObjectStorage
    {
        internal readonly static List<Page> pages = new();
        internal readonly static List<Window> windows = new();
    }
}
