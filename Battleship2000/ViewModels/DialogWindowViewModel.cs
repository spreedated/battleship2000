using Battleship2000.Logic;
using Battleship2000.ViewLogic;
using Battleship2000.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Battleship2000.ViewModels
{
    public class DialogWindowViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; } = new RelayCommand((c) =>
        {
            ((DialogWindow)c).DialogWindowResult = DialogWindow.DialogResults.Cancel;
            ((Window)c).Close();
        });

        private ICommand _YesCommand = new RelayCommand((c) =>
        {
            ((DialogWindow)c).DialogWindowResult = DialogWindow.DialogResults.Yes;
            ((Window)c).Close();
        });

        public ICommand YesCommand
        {
            get
            {
                return this._YesCommand;
            }
            set
            {
                this._YesCommand = value;
                base.OnPropertyChanged(nameof(this.YesCommand));
            }
        }

        private ICommand _NoCommand = new RelayCommand((c) =>
        {
            ((DialogWindow)c).DialogWindowResult = DialogWindow.DialogResults.No;
            ((Window)c).Close();
        });
        public ICommand NoCommand
        {
            get
            {
                return this._NoCommand;
            }
            set
            {
                this._NoCommand = value;
                base.OnPropertyChanged(nameof(this.NoCommand));
            }
        }

        private ICommand _OkayCommand = new RelayCommand((c) =>
        {
            ((DialogWindow)c).DialogWindowResult = DialogWindow.DialogResults.Okay;
            ((Window)c).Close();
        });
        public ICommand OkayCommand
        {
            get
            {
                return this._OkayCommand;
            }
            set
            {
                this._OkayCommand = value;
                base.OnPropertyChanged(nameof(this.OkayCommand));
            }
        }

        public Visibility BackgroundVis
        {
            get
            {
                return ObjectStorage.BackgroundVis;
            }
        }

        private DialogWindow _Instance;
        public DialogWindow Instance
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

        public ImageSource BackgroundImage
        {
            get
            {
                return ObjectStorage.BackgroundImage;
            }
        }

        private string _HeadlineText;
        public string HeadlineText
        {
            get
            {
                return this._HeadlineText;
            }
            set
            {
                this._HeadlineText = value;
                base.OnPropertyChanged(nameof(this.HeadlineText));
            }
        }

        private string _ContentText;
        public string ContentText
        {
            get
            {
                return this._ContentText;
            }
            set
            {
                this._ContentText = value;
                base.OnPropertyChanged(nameof(this.ContentText));
            }
        }

        private Visibility _ButtonYesVisibility = Visibility.Collapsed;
        public Visibility ButtonYesVisibility
        {
            get
            {
                return this._ButtonYesVisibility;
            }
            set
            {
                this._ButtonYesVisibility = value;
                base.OnPropertyChanged(nameof(this.ButtonYesVisibility));
            }
        }

        private Visibility _ButtonNoVisibility = Visibility.Collapsed;
        public Visibility ButtonNoVisibility
        {
            get
            {
                return this._ButtonNoVisibility;
            }
            set
            {
                this._ButtonNoVisibility = value;
                base.OnPropertyChanged(nameof(this.ButtonNoVisibility));
            }
        }

        private Visibility _ButtonCancelVisibility = Visibility.Collapsed;
        public Visibility ButtonCancelVisibility
        {
            get
            {
                return this._ButtonCancelVisibility;
            }
            set
            {
                this._ButtonCancelVisibility = value;
                base.OnPropertyChanged(nameof(this.ButtonCancelVisibility));
            }
        }

        private Visibility _ButtonOkayVisibility = Visibility.Collapsed;
        public Visibility ButtonOkayVisibility
        {
            get
            {
                return this._ButtonOkayVisibility;
            }
            set
            {
                this._ButtonOkayVisibility = value;
                base.OnPropertyChanged(nameof(this.ButtonOkayVisibility));
            }
        }
    }
}
