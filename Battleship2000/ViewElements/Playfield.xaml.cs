using Battleship2000.ViewLogic;
using Serilog;
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

namespace Battleship2000.ViewElements
{
    /// <summary>
    /// Interaction logic for Playfield.xaml
    /// </summary>
    public partial class Playfield : UserControl
    {
        public ICommand FieldClickCommand { get; } = new RelayCommand((c) =>
        {
            Button cc = (Button)c;
            cc.Background = Brushes.Red;
            Log.Debug($"[Playfield] Field {cc.Name} pressed");
        });

        public Playfield()
        {
            InitializeComponent();
            this.DataContext = this;

            string alpha = "ABCDEFGHIJ";

            StackPanel[] stcks = new StackPanel[]
            {
                this.StckPnl_L1,
                this.StckPnl_L2,
                this.StckPnl_L3,
                this.StckPnl_L4,
                this.StckPnl_L5,
                this.StckPnl_L6,
                this.StckPnl_L7,
                this.StckPnl_L8,
                this.StckPnl_L9,
                this.StckPnl_L10
            };

            for (int i = 0; i < 10; i++)
            {
                for (int ii = 0; ii < 10; ii++)
                {
                    stcks[i].Children.Add(new Button()
                    {
                        Name = $"Field_{alpha[ii]}_{i+1}",
                        Margin = new Thickness((ii == 0 ? 16 : 0), 0 ,4.8, 0),
                        Style = (Style)Application.Current.Resources["ButtonField"],
                        Command = this.FieldClickCommand
                    });
                    stcks[i].Children.OfType<Button>().Last().CommandParameter = stcks[i].Children.OfType<Button>().Last();
                }
            }

        }
    }
}
