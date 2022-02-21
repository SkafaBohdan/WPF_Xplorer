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
                LastIndexOf(value);
                OnPropertyChanged(nameof(Name));
            }
        }
        private void LastIndexOf(string value)
        {
            if (value != null)
            {
                var last = value.LastIndexOf("\\");
                name = value.Substring(last + 1);
            }
            else
            {
                name = value;
            }
        }

        public void OpenFile(string path, TreeView treeView)
        {
            service.GetDocumentNode(path, treeView, out bool boolPath);
            if (boolPath)
            {
                DocPath = path;
            }
            else
                DocPath = null;
        }


        public void RelativeLeaveAdd(TreeViewItem treeViewItem)
        {
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


        private ObservableCollection<StringBuilder> gridListItemKey;
        public ObservableCollection<StringBuilder> GridListItemKey
        {
            get
            {
                gridListItemKey = service.GetGridListItemKey();
                return gridListItemKey;
            }
          
        }
        private ObservableCollection<StringBuilder> gridListItemType;

        public ObservableCollection<StringBuilder> GridListItemType
        {
            get
            {
                gridListItemType = service.GetGridListItemType();
                return gridListItemType;
            }
        }
        private ObservableCollection<StringBuilder> gridListItemValue;
        public ObservableCollection<StringBuilder> GridListItemValue
        {
            get
            {
                gridListItemValue = service.GetGridListItemValue();
                return gridListItemValue;
            }
            
        }

        public StringBuilder PrintBookmarks()
        {
            return service.PrintBookmarks();
        }
    }
}
