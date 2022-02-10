using System.Windows;

namespace WPF_Xplorer.View
{
    /// <summary>
    /// Interaction logic for BookmarkAddChildBookmarkWindow.xaml
    /// </summary>
    public partial class BookmarkAddChildBookmarkWindow : Window
    {
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
    }
}
