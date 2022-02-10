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

        public override void Execute(object parameter)
        {
            //TODO: Сделать кнопку сохранить(сохранить как уже есть). 
            //TODO: Сделать тесты 
            //TODO: ++ивент, сделать делегат в котором буду вызывать фкункцию определениея пейдж каунта, а изначально он будет инициирован в прайвете. В гете будет только его получение.
            bookmarkAddChildWindow.ShowDialog();
        }
    }
}
