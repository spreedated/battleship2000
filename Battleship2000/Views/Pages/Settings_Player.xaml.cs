﻿using System.Windows;
using System.Windows.Controls;

namespace Battleship2000.Views.Pages
{
    /// <summary>
    /// Interaction logic for Settings_Player.xaml
    /// </summary>
    public partial class Settings_Player : Page
    {
        public Settings_Player()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TXT_Playername.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }
    }
}
