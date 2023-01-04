﻿using Battleship2000.ViewLogic;
using System.Linq;
using System.Reflection;

namespace Battleship2000.ViewModels
{
    public class PreloadViewModel : ViewModelBase
    {
        public string ProjectName { get; } = $"{((AssemblyTitleAttribute)typeof(MainWindowViewModel).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First()).Title}";
        public string ProjectVersion { get; } = $"v{typeof(MainWindowViewModel).Assembly.GetName().Version}";

        private double _ProgressbarValue = 0.0d;
        public double ProgressbarValue
        {
            get
            {
                return _ProgressbarValue;
            }
            set
            {
                _ProgressbarValue = value;
                base.OnPropertyChanged(nameof(ProgressbarValue));
            }
        }

        private string _LoadingText = "Loading ...";
        public string LoadingText
        {
            get
            {
                return _LoadingText;
            }
            set
            {
                _LoadingText = value;
                base.OnPropertyChanged(nameof(LoadingText));
            }
        }
    }
}