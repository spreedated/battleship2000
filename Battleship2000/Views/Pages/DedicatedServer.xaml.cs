﻿using Battleship2000.ViewModels;
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
    /// Interaction logic for DedicatedServer.xaml
    /// </summary>
    public partial class DedicatedServer : Page
    {
        public static DedicatedServer Instance { get; private set; } = null;
        public static DedicatedServerViewModel Vm { get; private set; } = null;
        public DedicatedServer()
        {
            InitializeComponent();
            Instance = this;
            Vm = (DedicatedServerViewModel)this.DataContext;
        }
    }
}