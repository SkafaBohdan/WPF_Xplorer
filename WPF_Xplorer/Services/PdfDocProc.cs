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

                default:
                    service.AddKidNodes(treeViewItem);
                    break;
            }
        }


    }
}
