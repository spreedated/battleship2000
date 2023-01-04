using System.Collections.Generic;
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
