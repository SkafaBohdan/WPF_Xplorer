﻿using pdftron.PDF;
using pdftron.SDF;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.Services
{
    public class PdfTreeProc : IPdfTreeProc
    {
        private readonly IPdfObjProc pdfObjectProc;
        private ObservableCollection<StringBuilder> gridListItemKey = new ObservableCollection<StringBuilder>();
        private ObservableCollection<StringBuilder> gridListItemType = new ObservableCollection<StringBuilder>();
        private ObservableCollection<StringBuilder> gridListItemValue = new ObservableCollection<StringBuilder>();
       
        public PdfTreeProc(IPdfObjProc pdfObjectProc)
        {
            this.pdfObjectProc = pdfObjectProc;
        }


        public TreeViewItem GetCatalogNode(Obj root, Obj catalogObj, string name)
        {
            var pdfObj = pdfObjectProc.InderectObj(catalogObj, name);

            return new TreeViewItem
            {
                Header = pdfObj.DisplayKey,
                Tag = new BinderObj(pdfObj, root)
            };
        }

        public IEnumerable<TreeViewItem> GetKidNodes(IEnumerable<KeyValuePair<string, Obj>> dictionary)
        {
            var child = dictionary.Select(item => GetKid(item.Key, item.Value)).Where(child => child.Tag is BinderObj);
       
            return child;
        }

        public TreeViewItem GetDocumentNode(string path)
        {
            var pdfObject = pdfObjectProc.DocObj(path);

            return new TreeViewItem
            {
                Header = path,
                Tag = new BinderObj(pdfObject, null)
            };
        }

        public TreeViewItem GetInfoNode(Obj infoObj, PDFDocInfo docInfo)
        {
            var pdfObject = pdfObjectProc.InderectObj(infoObj, "Info");
            pdfObject.Type = Models.PdfObj.PdfType.Info;

            return new TreeViewItem
            {
                Header = pdfObject.DisplayKeyAndValue,
                Tag = new BinderObj(pdfObject, infoObj)
                {
                    InfoDoc = docInfo
                }
            };
        }

        public IEnumerable<TreeViewItem> GetInfoNodes(PDFDocInfo info)
        {
            foreach (var (key, value) in pdfObjectProc.GetInfoObj(info))
            {
                if (string.IsNullOrEmpty(value)) continue;

                var pdfString = pdfObjectProc.StringObj(key, value);

                yield return new TreeViewItem
                {
                    Header = pdfString.DisplayKeyAndValue,
                    Tag = new BinderObj(pdfString, null)
                };
            }
        }

        public TreeViewItem GetStream(string name, IStreamService streamService, Obj value)
        {
            var pdfStream = pdfObjectProc.StreamObj(value, streamService, name);
            GetKidNodesKeyFromGridView(pdfStream);

            return new TreeViewItem
            {
                Header = pdfStream.DisplayKeyAndValue,
                Tag = new BinderObj(pdfStream, null)
            };
        }


        private TreeViewItem GetKid(string key, Obj value)
        {
            var pdfObject = pdfObjectProc.StructObjectBranchOnType(value, key, out var hasChild);

            GetKidNodesKeyFromGridView(pdfObject);

            var treeViewItem = new TreeViewItem
            {
                Header = pdfObject.DisplayKeyAndValue,
                Tag = new BinderObj(pdfObject, value)
            };

            if (hasChild)
            {
                treeViewItem.Items.Add(null);
            }

            return treeViewItem;
        }


        public ObservableCollection<StringBuilder> GetGridListItemKey()
        {
            return gridListItemKey;
        }
        public ObservableCollection<StringBuilder> GetGridListItemType()
        {
            return gridListItemType;
        }
        public ObservableCollection<StringBuilder> GetGridListItemValue()
        {
            return gridListItemValue;
        }
        private void GetKidNodesKeyFromGridView(Models.PdfObj obj)
        {
            StringBuilder childGridKey = new StringBuilder();
            StringBuilder childGridType = new StringBuilder();
            StringBuilder childGridValue = new StringBuilder();


            gridListItemKey.Add(childGridKey.Append(obj.Key));
            gridListItemType.Add(childGridType.Append(obj.DisplayType));
            gridListItemValue.Add(childGridValue.Append(obj.DisplayValue));
        }
    }
}
