using pdftron.PDF;
using pdftron.SDF;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace WPF_Xplorer.Models
{
    public class PdfObj : NotifyPropertyChanged
    {
        private string key;
        private PdfType type;
        private object value;
        public enum PdfType
        {
            Document,
            Bool,
            String,
            Stream,
            Number,
            Dictionary,
            Indirect,
            Array,
            Name,
            Info,
            Null
        }

        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        public PdfType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(nameof(value));
            }
        }

        public object Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public virtual string DisplayKey
        {
            get
            {
                if (Type is PdfType.Document || Type is PdfType.Stream)
                    return key;
                else
                    return $"/{key}";
            }
        }
        public virtual string DisplayType
        {
            get
            {
                if (Type is PdfType.Info)
                    return PdfType.Indirect.ToString();
                else
                    return Type.ToString();
            }
        }

        public virtual string DisplayValue
        {
            get
            {
                return value?.ToString();
            }
        }
        public virtual string DisplayKeyAndValue
        {
            get
            {
                return DisplayKey + " " + Value;
            }
        }

        public string StreamValue
        {
            get
            {
                if (!(Type is PdfType.Stream))
                    return string.Empty;

                byte[] arr;

                var streamValue = (PdfStream)this;
                using (var fileStream = new FileStream(streamValue.Path, FileMode.Open))
                {
                    arr = new byte[fileStream.Length];
                    fileStream.Read(arr, 0, arr.Length);
                }
                return Encoding.Default.GetString(arr);
            }
        }


        // TEST ZONE
        ObservableCollection<StringBuilder> gridListItemKey = new ObservableCollection<StringBuilder>();
        ObservableCollection<StringBuilder> gridListItemValue = new ObservableCollection<StringBuilder>();
        ObservableCollection<StringBuilder> gridListItemType = new ObservableCollection<StringBuilder>();


        public ObservableCollection<StringBuilder> GridListItemKey
        {
            get
            {
                gridListItemKey.Clear();
                gridListItemKey.Add(DisplayStructKey());
                return gridListItemKey;
            }
        }
        public ObservableCollection<StringBuilder> GridListItemType
        {
            get
            {
                gridListItemType.Clear();
                gridListItemType.Add(DisplayStructType());
                return gridListItemType;
            }
        }
        public ObservableCollection<StringBuilder> GridListItemValue
        {
            get
            {
                gridListItemValue.Clear();
                gridListItemValue.Add(DisplayStructValue());
                return gridListItemValue;
            }
        }


        StringBuilder DisplayStructKey()
        {
            string filename = "C:/Users/skafa/Desktop/test.pdf";
            PDFDoc doc = new PDFDoc(filename);
            var root = doc.GetRoot();
            var KeyStruct = Key == "Catalog" ? "Root" : Key;
            var obj = root.Get(KeyStruct).Value();
            var hasKid = false;


            StringBuilder stringList = new StringBuilder();
            if (obj.IsIndirect())
            {
                hasKid = true;
            }

            var branch = obj.GetDictIterator();
            CreateStructTree(branch);
            return stringList;
            void CreateStructTree(DictIterator branch)
            {
                var root_dict = branch;
                for (var itr = root_dict; itr.HasNext(); itr.Next())
                {
                    var choose = itr.Value().GetType();
                    switch (choose)
                    {
                        case Obj.ObjType.e_name:
                            stringList.Append(itr.Key().GetName() + "\n");
                            break;
                        case Obj.ObjType.e_dict:
                            hasKid = true;
                            stringList.Append(itr.Key().GetName() + "\n");
                            if (itr.Value().IsIndirect())
                            {
                                stringList.Append(itr.Value().GetObjNum() + " " + itr.Value().GetGenNum() + "R" + "\n");
                                break;
                            }
                            //else
                            //    for (var itr2 = itr.Value().GetDictIterator(); itr2.HasNext(); itr2.Next())
                            //    {
                            //        stringList.Append(itr2.Key().GetName() + "\n");
                            //        if (itr2.Value().GetType() == Obj.ObjType.e_dict)
                            //            CreateStructTree(itr2);
                            //    }
                            break;
                        case Obj.ObjType.e_stream:
                            stringList.Append(itr.Key().GetName() + "\n");
                            break;
                        case Obj.ObjType.e_string:
                            stringList.Append(itr.Key().GetName() + "\n");
                            break;
                        case Obj.ObjType.e_bool:
                            stringList.Append(itr.Key().GetName() + "\n");
                            break;
                        case Obj.ObjType.e_array:
                            hasKid = true;
                            for (int i = 0; i < itr.Value().Size(); ++i)
                            {
                                var arrayElement = itr.Value().GetAt(i);
                                if (arrayElement.GetType() == Obj.ObjType.e_number)
                                {
                                    stringList.Append(itr.Key().GetName() + "\n");
                                    break;
                                }
                                else
                                {
                                    CreateStructTree(arrayElement.GetDictIterator());
                                }
                            }
                            break;
                        case Obj.ObjType.e_number:
                            stringList.Append(itr.Key().GetName() + "\n");
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        StringBuilder DisplayStructType()
        {
            string filename = "C:/Users/skafa/Desktop/test.pdf";
            PDFDoc doc = new PDFDoc(filename);
            var root = doc.GetRoot();
            var KeyStruct = Key == "Catalog" ? "Root" : Key;
            var obj = root.Get(KeyStruct).Value();
            var hasKid = false;

            StringBuilder stringList = new StringBuilder();
            if (obj.IsIndirect())
            {
                hasKid = true;
                // stringList.Append(KeyStruct + "\n");
            }

            var branch = obj.GetDictIterator();
            CreateStructTree(branch);
            return stringList;
            void CreateStructTree(DictIterator branch)
            {
                var root_dict = branch;
                for (var itr = root_dict; itr.HasNext(); itr.Next())
                {
                    var choose = itr.Value().GetType();
                    stringList.Append(itr.Value().GetType() + "\n");

                    //    switch (choose)
                    //    {
                    //        case Obj.ObjType.e_name:
                    //            stringList.Append(itr.Key().GetName() + "\n");
                    //            break;
                    //        case Obj.ObjType.e_dict:
                    //            hasKid = true;
                    //            stringList.Append(itr.Key().GetName() + "\n");
                    //            if (itr.Value().IsIndirect())
                    //            {
                    //                stringList.Append(itr.Value().GetObjNum() + " " + itr.Value().GetGenNum() + "R" + "\n");
                    //                break;
                    //            }
                    //            //else
                    //            //    for (var itr2 = itr.Value().GetDictIterator(); itr2.HasNext(); itr2.Next())
                    //            //    {
                    //            //        stringList.Append(itr2.Key().GetName() + "\n");
                    //            //        if (itr2.Value().GetType() == Obj.ObjType.e_dict)
                    //            //            CreateStructTree(itr2);
                    //            //    }
                    //            break;
                    //        case Obj.ObjType.e_stream:
                    //            stringList.Append(itr.Key().GetName() + " " + itr.Value().GetObjNum() + itr.Value().GetGenNum() + "R" + "\n");
                    //            break;
                    //        case Obj.ObjType.e_string:
                    //            stringList.Append(itr.Key().GetName() + "\n");
                    //            break;
                    //        case Obj.ObjType.e_bool:
                    //            stringList.Append(itr.Key().GetName() + "\n");
                    //            break;
                    //        case Obj.ObjType.e_array:
                    //            hasKid = true;
                    //            for (int i = 0; i < itr.Value().Size(); ++i)
                    //            {
                    //                var arrayElement = itr.Value().GetAt(i);
                    //                if (arrayElement.GetType() == Obj.ObjType.e_number)
                    //                {
                    //                    stringList.Append(itr.Key().GetName() + " " + itr.Value().GetAt(i).GetNumber() + "\n");
                    //                    break;
                    //                }
                    //                else
                    //                {
                    //                    CreateStructTree(arrayElement.GetDictIterator());
                    //                    break;
                    //                }
                    //            }
                    //            break;
                    //        case Obj.ObjType.e_number:
                    //            stringList.Append(itr.Key().GetName() + "\n");
                    //            break;

                    //        default:
                    //            break;
                    //    }
                    //}
                }

            }
        }

        StringBuilder DisplayStructValue()
        {
            string filename = "C:/Users/skafa/Desktop/test.pdf";
            PDFDoc doc = new PDFDoc(filename);
            var root = doc.GetRoot();
            var KeyStruct = Key == "Catalog" ? "Root" : Key;
            var obj = root.Get(KeyStruct).Value();
            var hasKid = false;


            StringBuilder stringList = new StringBuilder();

            if (obj.IsIndirect())
            {
                hasKid = true;
            }

            var branch = obj.GetDictIterator();
            CreateStructTree(branch);
            return stringList;
            void CreateStructTree(DictIterator branch)
            {
                var root_dict = branch;
                for (var itr = root_dict; itr.HasNext(); itr.Next())
                {
                    var choose = itr.Value().GetType();
                    switch (choose)
                    {
                        case Obj.ObjType.e_name:
                            stringList.Append(itr.Value().GetName() + "\n");
                            break;
                        case Obj.ObjType.e_dict:
                            hasKid = true;
                            stringList.Append(itr.Key().GetName() + "\n");
                            if (itr.Value().IsIndirect())
                            {
                                stringList.Append(itr.Value().GetObjNum() + " " + itr.Value().GetGenNum() + "R" + "\n");
                                break;
                            }
                            else
                                for (var itr2 = itr.Value().GetDictIterator(); itr2.HasNext(); itr2.Next())
                                {
                                    if (itr2.Value().GetType() == Obj.ObjType.e_dict)
                                        CreateStructTree(itr2);
                                }
                            break;
                        case Obj.ObjType.e_stream:
                            stringList.Append(itr.Value().GetObjNum() + itr.Value().GetGenNum() + "R" + "\n");
                            break;
                        case Obj.ObjType.e_string:
                            stringList.Append(itr.Value().GetAsPDFText() + "\n");
                            break;
                        case Obj.ObjType.e_bool:
                            stringList.Append(itr.Value().GetBool() + "\n");
                            break;
                        case Obj.ObjType.e_array:
                            hasKid = true;
                            for (int i = 0; i < itr.Value().Size(); ++i)
                            {
                                var arrayElement = itr.Value().GetAt(i);
                                if (arrayElement.GetType() == Obj.ObjType.e_number)
                                {
                                    stringList.Append(itr.Value().GetAt(i).GetNumber() + "\n");
                                    break;
                                }
                                else
                                {
                                    CreateStructTree(arrayElement.GetDictIterator());
                                }
                            }
                            break;
                        case Obj.ObjType.e_number:
                            stringList.Append(itr.Value().GetNumber() + "\n");
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
