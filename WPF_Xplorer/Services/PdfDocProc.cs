using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            private set
            {
                var normalizedPath = value.Replace('/', '\\');
                var lastIndex = normalizedPath.LastIndexOf('\\');

                name = lastIndex <= 0 ? value : value[(lastIndex + 1)..];

                OnPropertyChanged(nameof(Name));
            }
        }

        public void OpenFile(string path, ref TreeView treeView)
        {
            service.GetDocumentNode(path, treeView);
            DocPath = path;
        }

        public void AddRelativeLeaves(ref TreeViewItem treeViewItem)
        {
            //var items = treeViewItem.Items;
            //if (items[0] != null || items.Count > 1) return;

            treeViewItem.Items.Clear(); //Delete dummy item

            var type = ((ObjBinder)treeViewItem.Tag).PdfObj.Type;
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
                    service.AddChildNodes(treeViewItem);
                    break;
            }
        }


    }
}
