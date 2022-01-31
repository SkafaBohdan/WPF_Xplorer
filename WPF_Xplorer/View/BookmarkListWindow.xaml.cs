using System.Windows;

namespace WPF_Xplorer.View
{
    /// <summary>
    /// Interaction logic for BookmarkList.xaml
    /// </summary>
    public partial class BookmarkListWindow : Window
    {
        public BookmarkListWindow(object dataContext)
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
