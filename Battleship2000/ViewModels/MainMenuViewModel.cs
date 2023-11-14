﻿using Battleship2000.ViewLogic;
using Battleship2000.Views;
using Battleship2000.Views.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Battleship2000.ViewModels
{
    public class MainMenuViewModel : ObservableObject
    {
        public ICommand PlayCommand { get; } = new RelayCommand(() =>
        {
            HelperFunctions.Navigate("playmodeselection");
        });

        public ICommand ExitCommand { get; } = new RelayCommand<Window>((w) =>
        {
            DialogWindow d = new($"Quit?", "Are you sure you want to exit?", DialogWindow.DialogButtons.Yes | DialogWindow.DialogButtons.No)
            {
                Owner = Window.GetWindow(w)
            };
            d.ShowDialog();

            if (d.DialogWindowResult == DialogWindow.DialogResults.Yes)
            {
                w.Close();
            }
        });

        public ICommand SettingsCommand { get; } = new RelayCommand(() =>
        {
            HelperFunctions.Navigate("settings");
        });

#if DEBUG
        public ICommand DebugCommand { get; } = new RelayCommand(() =>
        {
            HelperFunctions.Navigate("shipplacement");
        });
#endif

#if DEBUG
        private Visibility _DebugButtonVisibility = Visibility.Visible;
#else
        private Visibility _DebugButtonVisibility = Visibility.Collapsed;
#endif
        public Visibility DebugButtonVisibility
        {
            get
            {
                return this._DebugButtonVisibility;
            }
            set
            {
                this._DebugButtonVisibility = value;
                base.OnPropertyChanged(nameof(this.DebugButtonVisibility));
            }
        }

        private MainMenu _Instance;
        public MainMenu Instance
        {
            get
            {
                return this._Instance;
            }
            set
            {
                this._Instance = value;
                base.OnPropertyChanged(nameof(this.Instance));
            }
        }

        private Page _CurrentFramePage;
        public Page CurrentFramePage
        {
            get
            {
                return this._CurrentFramePage;
            }
            set
            {
                this._CurrentFramePage = value;
                base.OnPropertyChanged(nameof(this.CurrentFramePage));
            }
        }

        private MainWindow _ParentWindow;
        public MainWindow ParentWindow
        {
            get
            {
                return this._ParentWindow;
            }
            set
            {
                this._ParentWindow = value;
                base.OnPropertyChanged(nameof(this.ParentWindow));
            }
        }
    }
}
