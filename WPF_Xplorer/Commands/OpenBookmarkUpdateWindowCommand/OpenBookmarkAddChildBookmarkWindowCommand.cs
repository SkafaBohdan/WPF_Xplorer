using WPF_Xplorer.View;
using WPF_Xplorer.ViewModels;


namespace WPF_Xplorer.Commands.OpenBookmarkUpdateWindowCommand
{
    public class OpenBookmarkAddChildBookmarkWindowCommand : BaseCommand
    {
        public BookmarkUpdateViewModel bookmarkUpdateViewModel { get; set; }
        static BookmarkAddChildBookmarkWindow bookmarkAddChildWindow  { get; set; }
      

        public OpenBookmarkAddChildBookmarkWindowCommand(BookmarkUpdateViewModel bookmarkUpdateViewModel)
        {
            this.bookmarkUpdateViewModel = bookmarkUpdateViewModel;
            bookmarkAddChildWindow = new BookmarkAddChildBookmarkWindow(this.bookmarkUpdateViewModel);
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(bookmarkUpdateViewModel.ParentNameBookmark);
        }

        public override void Execute(object parameter)
        {
            //TODO: Сделать тесты 
            //TODO: почитать статью за впф
            //TODO: почитать за делегаты и ивенты

            bookmarkAddChildWindow.ShowDialog();
        }
    }
}
