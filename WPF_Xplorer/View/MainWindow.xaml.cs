using System.Windows;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(object dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }
    }
}
