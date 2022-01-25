using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Xplorer.ViewModels
{
    public class BookmarksViewModel : INotifyPropertyChanged
    {
        private string textBookmarks { get; set; }
        public string TextBookmarks
        {
            get => textBookmarks;
            set
            {
                textBookmarks = value;
                OnPropertyChanged(nameof(TextBookmarks));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
