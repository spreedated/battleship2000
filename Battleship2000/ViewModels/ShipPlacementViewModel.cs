using Battleship2000.Models;
using Battleship2000.ViewElements;
using Battleship2000.ViewLogic;
using Battleship2000.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class ShipPlacementViewModel : ViewModelBase
    {
        public ICommand DisconnectCommand { get; } = new RelayCommand(async (c) =>
        {
            HostServer.Vm.ResetButtonStates();
            await HostServer.Vm.NetworkServerStop();
            MainWindowViewModel.Navigate("mainmenu");
        });

        public string Playername
        {
            get
            {
                return HostServer.Vm.NetworkServer?.ConnectedClient?.Playername;
            }
        }

        private bool _ButtonDisconnectEnabled = true;
        public bool ButtonDisconnectEnabled
        {
            get
            {
                return this._ButtonDisconnectEnabled;
            }
            set
            {
                this._ButtonDisconnectEnabled = value;
                base.OnPropertyChanged(nameof(this.ButtonDisconnectEnabled));
            }
        }

        private bool _ButtonReadyEnabled;
        public bool ButtonReadyEnabled
        {
            get
            {
                return this._ButtonReadyEnabled;
            }
            set
            {
                this._ButtonReadyEnabled = value;
                base.OnPropertyChanged(nameof(this.ButtonReadyEnabled));
            }
        }

        private Visibility _CarrierArrowVisibility = Visibility.Hidden;
        public Visibility CarrierArrowVisibility
        {
            get
            {
                return this._CarrierArrowVisibility;
            }
            set
            {
                this._CarrierArrowVisibility = value;
                base.OnPropertyChanged(nameof(this.CarrierArrowVisibility));
            }
        }

        private Visibility _BattleshipArrowVisibility = Visibility.Hidden;
        public Visibility BattleshipArrowVisibility
        {
            get
            {
                return this._BattleshipArrowVisibility;
            }
            set
            {
                this._BattleshipArrowVisibility = value;
                base.OnPropertyChanged(nameof(this.BattleshipArrowVisibility));
            }
        }

        private Visibility _CruiserArrowVisibility = Visibility.Hidden;
        public Visibility CruiserArrowVisibility
        {
            get
            {
                return this._CruiserArrowVisibility;
            }
            set
            {
                this._CruiserArrowVisibility = value;
                base.OnPropertyChanged(nameof(this.CruiserArrowVisibility));
            }
        }

        private Visibility _SubmarineArrowVisibility = Visibility.Hidden;
        public Visibility SubmarineArrowVisibility
        {
            get
            {
                return this._SubmarineArrowVisibility;
            }
            set
            {
                this._SubmarineArrowVisibility = value;
                base.OnPropertyChanged(nameof(this.SubmarineArrowVisibility));
            }
        }

        private Visibility _DestroyerArrowVisibility = Visibility.Hidden;
        public Visibility DestroyerArrowVisibility
        {
            get
            {
                return this._DestroyerArrowVisibility;
            }
            set
            {
                this._DestroyerArrowVisibility = value;
                base.OnPropertyChanged(nameof(this.DestroyerArrowVisibility));
            }
        }

        private bool _CarrierEnabled = true;
        public bool CarrierEnabled
        {
            get
            {
                return this._CarrierEnabled;
            }
            set
            {
                this._CarrierEnabled = value;
                base.OnPropertyChanged(nameof(this.CarrierEnabled));
            }
        }

        private bool _BattleshipEnabled = true;
        public bool BattleshipEnabled
        {
            get
            {
                return this._BattleshipEnabled;
            }
            set
            {
                this._BattleshipEnabled = value;
                base.OnPropertyChanged(nameof(this.BattleshipEnabled));
            }
        }

        private bool _CruiserEnabled = true;
        public bool CruiserEnabled
        {
            get
            {
                return this._CruiserEnabled;
            }
            set
            {
                this._CruiserEnabled = value;
                base.OnPropertyChanged(nameof(this.CruiserEnabled));
            }
        }

        private bool _SubmarineEnabled = true;
        public bool SubmarineEnabled
        {
            get
            {
                return this._SubmarineEnabled;
            }
            set
            {
                this._SubmarineEnabled = value;
                base.OnPropertyChanged(nameof(this.SubmarineEnabled));
            }
        }

        private bool _DestroyerEnabled = true;
        public bool DestroyerEnabled
        {
            get
            {
                return this._DestroyerEnabled;
            }
            set
            {
                this._DestroyerEnabled = value;
                base.OnPropertyChanged(nameof(this.DestroyerEnabled));
            }
        }

        public Ship SelectedShip { get; private set; }

        public enum ShipSelections
        {
            None = 0,
            All,
            Carrier,
            Battleship,
            Cruiser,
            Submarine,
            Destroyer
        }

        public ShipSelections ShipSelectionCurrent { get; private set; }

        public ICommand CarrierSetCommand { get; } = new RelayCommand((c) =>
        {
            if (ShipPlacement.Vm.ShipSelectionCurrent == ShipSelections.Carrier)
            {
                ShipSelectorButtonHandler(ShipSelections.All);
                ShipPlacement.Vm.SelectedShip = null;
                return;
            }

            ShipSelectorButtonHandler(ShipSelections.Carrier);
            ShipPlacement.Vm.SelectedShip = new Carrier();
        });

        public ICommand BattlshipSetCommand { get; } = new RelayCommand((c) =>
        {
            if (ShipPlacement.Vm.BattleshipArrowVisibility == Visibility.Visible)
            {
                ShipSelectorButtonHandler(ShipSelections.All);
                ShipPlacement.Vm.SelectedShip = null;
                return;
            }
            ShipSelectorButtonHandler(ShipSelections.Battleship);
            ShipPlacement.Vm.SelectedShip = new Battleship();
        });

        public ICommand CruiserSetCommand { get; } = new RelayCommand((c) =>
        {
            if (ShipPlacement.Vm.CruiserArrowVisibility == Visibility.Visible)
            {
                ShipSelectorButtonHandler(ShipSelections.All);
                ShipPlacement.Vm.SelectedShip = null;
                return;
            }
            ShipSelectorButtonHandler(ShipSelections.Cruiser);
            ShipPlacement.Vm.SelectedShip = new Cruiser();
        });

        public ICommand SubmarineSetCommand { get; } = new RelayCommand((c) =>
        {
            if (ShipPlacement.Vm.SubmarineArrowVisibility == Visibility.Visible)
            {
                ShipSelectorButtonHandler(ShipSelections.All);
                ShipPlacement.Vm.SelectedShip = null;
                return;
            }
            ShipSelectorButtonHandler(ShipSelections.Submarine);
            ShipPlacement.Vm.SelectedShip = new Submarine();
        });

        public ICommand DestroyerSetCommand { get; } = new RelayCommand((c) =>
        {
            if (ShipPlacement.Vm.DestroyerArrowVisibility == Visibility.Visible)
            {
                ShipSelectorButtonHandler(ShipSelections.All);
                ShipPlacement.Vm.SelectedShip = null;
                return;
            }
            ShipSelectorButtonHandler(ShipSelections.Destroyer);
            ShipPlacement.Vm.SelectedShip = new Destroyer();
        });

        private static void ShipSelectorButtonHandler(ShipSelections shipSelection)
        {
            ShipPlacement.Vm.ShipSelectionCurrent = shipSelection;

            IEnumerable<PropertyInfo> allEnables = typeof(ShipPlacementViewModel).GetProperties().Where(p => p.PropertyType == typeof(bool) && Enum.GetNames(typeof(ShipSelections)).Any(x => p.Name.Contains(x)));
            foreach (PropertyInfo p in allEnables)
            {
                p.SetValue(ShipPlacement.Vm, false);
            }

            IEnumerable<PropertyInfo> allVisiblesArrows = typeof(ShipPlacementViewModel).GetProperties().Where(p => p.PropertyType == typeof(Visibility) && Enum.GetNames(typeof(ShipSelections)).Any(x => p.Name.Contains(x)));
            foreach (PropertyInfo p in allVisiblesArrows)
            {
                p.SetValue(ShipPlacement.Vm, Visibility.Hidden);
            }

            if (shipSelection == ShipSelections.All)
            {
                foreach (PropertyInfo p in allEnables)
                {
                    p.SetValue(ShipPlacement.Vm, true);
                }
                return;
            }

            PropertyInfo specificPropertyEnable = typeof(ShipPlacementViewModel).GetProperties().FirstOrDefault(p => p.Name.Contains(shipSelection.ToString()) && p.PropertyType == typeof(bool));
            PropertyInfo specificPropertyArrowVisible = typeof(ShipPlacementViewModel).GetProperties().FirstOrDefault(p => p.Name.Contains(shipSelection.ToString()) && p.PropertyType == typeof(Visibility));

            if (specificPropertyArrowVisible != null && specificPropertyEnable != null)
            {
                specificPropertyEnable.SetValue(ShipPlacement.Vm, true);
                specificPropertyArrowVisible.SetValue(ShipPlacement.Vm, Visibility.Visible);
            }
        }

        public ICommand OrientationChangeCommand { get; } = new RelayCommand((c) =>
        {
            if (ShipPlacement.Vm.RotationArrow == SelectionArrow.Rotations.Right)
            {
                ShipPlacement.Vm.RotationArrow = SelectionArrow.Rotations.Down;
                return;
            }

            ShipPlacement.Vm.RotationArrow = SelectionArrow.Rotations.Right;
        });

        private SelectionArrow.Rotations _RotationArrow = SelectionArrow.Rotations.Right;
        public SelectionArrow.Rotations RotationArrow
        {
            get
            {
                return this._RotationArrow;
            }
            set
            {
                this._RotationArrow = value;
                base.OnPropertyChanged(nameof(this.RotationArrow));
            }
        }

    }
}
