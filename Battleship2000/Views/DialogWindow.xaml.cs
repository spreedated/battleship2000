using Battleship2000.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Battleship2000.Views
{
    public partial class DialogWindow : Window
    {
        [Flags]
        public enum DialogButtons
        {
            None = 0,
            Yes = 1,
            No = 2,
            Cancel = 4,
            Okay = 8
        }

        public enum DialogResults
        {
            None = 0,
            Yes,
            No,
            Cancel,
            Okay
        }

        public DialogButtons Dialogstyle { get; private set; }
        public DialogResults DialogWindowResult { get; set; }

        private readonly DialogWindowViewModel Vm;

        public DialogWindow()
        {
            this.InitializeComponent();
        }

        public DialogWindow(string headlinetext, string contenttext, DialogButtons dialogstyle = DialogButtons.Cancel) : this()
        {
            this.Vm = ((DialogWindowViewModel)this.DataContext);

            this.Vm.HeadlineText = headlinetext;
            this.Vm.ContentText = contenttext;
            this.Vm.Instance = this;
            this.Dialogstyle = dialogstyle;
            this.ApplyDialogstyle();

            this.Height = 180 + (28 * (contenttext.Count(x => x == '\n')));
        }

        private void ApplyDialogstyle()
        {
            if (this.Dialogstyle.HasFlag(DialogButtons.Yes))
            {
                this.Vm.ButtonYesVisibility = Visibility.Visible;
            }
            if (this.Dialogstyle.HasFlag(DialogButtons.No))
            {
                this.Vm.ButtonNoVisibility = Visibility.Visible;
            }
            if (this.Dialogstyle.HasFlag(DialogButtons.Cancel))
            {
                this.Vm.ButtonCancelVisibility = Visibility.Visible;
            }
            if (this.Dialogstyle.HasFlag(DialogButtons.Okay))
            {
                this.Vm.ButtonOkayVisibility = Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
