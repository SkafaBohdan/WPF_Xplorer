using System.Windows;

namespace WPF_Xplorer.View
{
    /// <summary>
    /// Interaction logic for BookmarkUpdateWindow.xaml
    /// </summary>
    public partial class BookmarkUpdateWindow : Window
    {
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
    }
}
