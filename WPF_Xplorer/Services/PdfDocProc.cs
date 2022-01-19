using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using WPF_Xplorer.Services.Interfaces;
using static WPF_Xplorer.Models.PdfObj;

namespace WPF_Xplorer.Services
{
    public class PdfDocProc : NotifyPropertyChanged, IPdfDocProc
    {
        private readonly IPdfService service;
        private string docPath;
        private string name;


        public PdfDocProc(IPdfService service)
        {
            this.service = service;
        }

        public string DocPath
        {

            get => docPath;
            set
            {
                docPath = value;
                Name = value;
                OnPropertyChanged(nameof(DocPath));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public void OpenFile(string path, ref TreeView treeView)
        {
            service.GetDocumentNode(path, treeView);
            DocPath = path;
        }

        public void RelativeLeaveAdd(ref TreeViewItem treeViewItem)
        {
            var items = treeViewItem.Items;
            if (items.Count > 1 || items.Count == 0)
                return;

            treeViewItem.Items.Clear();

            var type = ((BinderObj)treeViewItem.Tag).PdfObj.Type;
            switch (type)
            {
                case PdfType.Document:
                    service.AddCatalogNode(treeViewItem);
                    service.AddInfoNode(treeViewItem);
                    break;

                case PdfType.Info:
                    service.AddInfoStrings(treeViewItem);
                    break;

                case PdfType.String:
                    break;

                default:
                    service.AddKidNodes(treeViewItem);
                    break;
            }
        }



        //TESTZOONEEE 
        //TODO: test zone
        private ObservableCollection<StringBuilder> gridListItemKey;
        public ObservableCollection<StringBuilder> GridListItemKey
        {
            get
            {
                gridListItemKey = service.GetGridListItemKey();
                return gridListItemKey;
            }
            //set
            //{
            //    gridListItemKey = service.GetGridListItemKey();
            //    OnPropertyChanged(nameof(GridListItemKey));
            //}
        }

        ////

    }
}
