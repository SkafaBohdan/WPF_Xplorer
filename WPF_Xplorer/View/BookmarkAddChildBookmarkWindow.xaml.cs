using System.Text.RegularExpressions;
using System.Windows;

namespace WPF_Xplorer.View
{
    /// <summary>
    /// Interaction logic for BookmarkAddChildBookmarkWindow.xaml
    /// </summary>
    public partial class BookmarkAddChildBookmarkWindow : Window
    {
        private static readonly Regex onlyNumbers = new Regex("[^0-9.-]+");
        public BookmarkAddChildBookmarkWindow(object dataContex)
        {
            InitializeComponent();

            DataContext = dataContex;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void ChildPage_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !onlyNumbers.IsMatch(text);
        }
    }
}
