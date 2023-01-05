using Battleship2000.Logic;
using Battleship2000.Models;
using Battleship2000.ViewLogic;
using Battleship2000.Views;
using Battleship2000.Views.Pages;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2000.ViewModels
{
    public class SettingsAppearanceViewModel : ViewModelBase
    {
        public SettingsAppearanceViewModel()
        {
            Task.Factory.StartNew(async () =>
            {
                while (Settings_Appearance.Vm == null || Settings_Appearance.Vm.Backgrounds == null)
                {
                    await Task.Delay(50);
                }

                Settings_Appearance.Instance.Dispatcher.Invoke(() =>
                {
                    Settings_Appearance.Vm.Backgrounds.Add(new() { Name = "Oldschool", Filename = "battleship1-1280x736.png" });
                    Settings_Appearance.Vm.Backgrounds.Add(new() { Name = "Blue", Filename = "blue.png" });
                    if (Settings_Appearance.Vm.Backgrounds.Any(x => x.Name == ObjectStorage.Config.Appearance.Background))
                    {
                        Settings_Appearance.Instance.CMB_Background.SelectedItem = Settings_Appearance.Vm.Backgrounds.First(x => x.Name == ObjectStorage.Config.Appearance.Background);
                    }
                });
            });
        }

        public ObservableCollection<Background> Backgrounds { get; set; } = new();

        private Background _SelectedBackground;
        public Background SelectedBackground
        {
            get
            {
                return this._SelectedBackground;
            }
            set
            {
                this._SelectedBackground = value;
                ObjectStorage.Config.Appearance.Background = this._SelectedBackground.Name;
                base.OnPropertyChanged(nameof(SelectedBackground));
                MainWindow.Instance.RefreshBackground();
            }
        }
    }
}
