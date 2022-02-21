using pdftron.PDF;
using System.Windows.Controls;

namespace WPF_Xplorer.Services.Interfaces
{
    public interface IBookmarksUpdateService
    {
        int PageCount { get; }
        void AddBookmark(string name, int page);
        void DeleteBookmark(Bookmark bookmarkObj);
        void SaveBookmarks(string path);
        void AddChildBookmark(Bookmark parentBookmark, string name, int page);
        void InitPageCount();
     
        void GetBookmarksTreeViewItem(TreeView treeViewItem);
    }
}
    