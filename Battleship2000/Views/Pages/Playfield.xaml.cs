using Serilog;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Playfield.xaml
    /// </summary>
    public partial class Playfield : Page
    {
        public Playfield()
        {
            InitializeComponent();
            Log.Verbose("[Playfield] Page loaded");
        }
    }
}
