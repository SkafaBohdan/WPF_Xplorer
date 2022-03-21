using System.Text.RegularExpressions;
using System.Windows;

namespace WPF_Xplorer.View
{
    /// <summary>
    /// Interaction logic for BookmarkUpdateWindow.xaml
    /// </summary>
    public partial class BookmarkUpdateWindow : Window
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9.-]+");

        public BookmarkUpdateWindow(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void Page_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !onlyNumbers.IsMatch(text);
        }
    }
}
