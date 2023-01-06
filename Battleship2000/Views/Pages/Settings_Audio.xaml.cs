using Battleship2000.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Settings_Audio.xaml
    /// </summary>
    public partial class Settings_Audio : Page
    {
        public static Settings_Audio Instance { get; private set; }
        public static SettingsAudioViewModel Vm { get; private set; }
        public Settings_Audio()
        {
            InitializeComponent();
            Instance = this;
            Vm = (SettingsAudioViewModel)this.DataContext;
        }
    }
}
