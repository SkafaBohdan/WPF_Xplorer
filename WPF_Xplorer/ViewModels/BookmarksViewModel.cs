namespace WPF_Xplorer.ViewModels
{
    public class BookmarksViewModel : NotifyPropertyChanged
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
    }
}
