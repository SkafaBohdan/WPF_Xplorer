namespace WPF_Xplorer.Services.Interfaces
{
    public interface IBookmarksUpdateService
    {
        int PageCount { get; }
        void AddBookmark(string name, int page);
        void DeleteBookmark(string name);
        void SaveBookmarks(string path);
    }
}
    