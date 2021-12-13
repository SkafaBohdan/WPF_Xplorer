using pdftron;
using pdftron.Filters;
using pdftron.PDF;
using pdftron.PDF.Struct;
using pdftron.SDF;
using System;

namespace TestingPDFTron
{
    //public enum ObjType
    //{
    //    e_null = 0,
    //    e_bool = 1,
    //    e_number = 2,
    //    e_name = 3,
    //    e_string = 4,
    //    e_dict = 5,
    //    e_array = 6,
    //    e_stream = 7
    //}


    class Program3_test_main
    {
        static string input_path = "C:/Users/skafa/Desktop/test.pdf";
        static string input_path2 = "C:/Users/skafa/Downloads/PDF32000_2008-1-200.pdf";

        static void Main(string[] args)
        {
            PDFNet.Initialize("demo:1638797506699:7b62627f0300000000e7c9c9b56709b97ba1634ef4ec416ab001038e68");

            Pdf_Struct();
        }


        static void Pdf_Struct()
        {
            using (MappedFile file = new MappedFile(input_path))
            {
                FilterReader reader = new FilterReader(file);
                var fileSize = file.FileSize();
                byte[] mem = new byte[fileSize];
                reader.Read(mem);
                
                //получение рута и его имени "root"
                PDFDoc pDFDoc = new PDFDoc(mem, fileSize);
                pDFDoc.InitSecurityHandler();

                var root = pDFDoc.GetRoot();
                var root_name = root.Find("Type").Value().GetName();

                // DisplayInfo(pDFDoc);
              
                // вывод свойств сatalog
                var root_dict = root.GetDictIterator();
                DisplayStructKeyAndValueTreePdf(root);

                Console.WriteLine("-----------------");
                //for (var itr = root_dict; itr.HasNext(); itr.Next())
                //{
                //    var choose = itr.Value().GetType();
                //    Console.WriteLine("-----------------");
                //    Console.WriteLine(itr.Value().GetType());
                //    switch (choose)
                //    {
                //        case Obj.ObjType.e_name:
                //            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetName());
                //            break;
                //        case Obj.ObjType.e_dict:
                //            Console.WriteLine(itr.Key().GetName() + " -->");
                //            if (itr.Value().IsIndirect())
                //            {
                //                Console.WriteLine(" | " + itr.Value().GetObjNum() + " " + itr.Value().GetGenNum() + " R");
                //            }
                //            else
                //                for (var itr2 = itr.Value().GetDictIterator(); itr2.HasNext(); itr2.Next())
                //                {
                //                    Console.WriteLine(itr2.Key().GetName());
                //                }
                //            break;
                //        case Obj.ObjType.e_stream:
                //            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetObjNum() + " " + itr.Value().GetGenNum() + " R");
                //            break;
                //        case Obj.ObjType.e_string:
                //            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetAsPDFText());
                //            break;
                //        case Obj.ObjType.e_bool:
                //            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetBool());
                //            break;
                //        case Obj.ObjType.e_array:
                //            Console.WriteLine(itr.Key().GetName() + " | ");
                //            for (var itr2 = itr.Value().GetDictIterator(); itr2.HasNext(); itr2.Next())
                //            {
                //                Console.WriteLine(itr2.Value().GetName());
                //            }
                //            break;
                //        case Obj.ObjType.e_number:
                //            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetNumber());
                //            break;
                //    }
                //}


                //вывод на дисплей информации //Catalog 7 0 R  /  Info 4 0 R  / 4 0 Object
                //Console.WriteLine(root_name + " " + root.GetObjNum() + " " + root.GetGenNum() + " R");
                //Console.WriteLine(info_name + " " + info_dict.GetObjNum() + " " + info_dict.GetGenNum() + " R");
                //Console.WriteLine(info_dict.GetObjNum() + " " + info_dict.GetGenNum() + " " + info.GetType().BaseType.Name);
            }
        }

        static void DisplayInfo(PDFDoc pDFDoc)
        {
            //получение info и его имени "info
            var trailer = pDFDoc.GetTrailer();
            var info = trailer.Get("Info");
            var info_dict = trailer.Get("Info").Value();
            var info_name = info.Key().GetName();

            //done
            Console.WriteLine("-----------------");
            Console.WriteLine(info.Key().GetName() + " " + info_dict.GetObjNum() + " " + info_dict.GetGenNum() + " R");
            for (var itr = info_dict.GetDictIterator(); itr.HasNext(); itr.Next())
            {
                Console.WriteLine(itr.Key().GetName() + " " + itr.Value().GetType() + " " + itr.Value().GetAsPDFText());
            }
        }

        static void DisplayStructKeyAndValueTreePdf(Obj obj)
        {
            var root = obj.GetDictIterator();
            StructTree(root);
            void StructTree(DictIterator branch)
            {
                // вывод свойств сatalog
                var root_dict = branch;
                Console.WriteLine("-----------------");
                for (var itr = root_dict; itr.HasNext(); itr.Next())
                {
                    var choose = itr.Value().GetType();
                    Console.WriteLine("-----------------");
                    Console.WriteLine(itr.Value().GetType());
                    switch (choose)
                    {
                        case Obj.ObjType.e_name:
                            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetName());
                            break;
                        case Obj.ObjType.e_dict:
                            Console.WriteLine(itr.Key().GetName() + " -->");
                            if (itr.Value().IsIndirect())
                            {
                                Console.WriteLine(" | " + itr.Value().GetObjNum() + " " + itr.Value().GetGenNum() + " R");
                            }
                            else
                                for (var itr2 = itr.Value().GetDictIterator(); itr2.HasNext(); itr2.Next())
                                {
                                    Console.WriteLine(itr2.Key().GetName());
                                    if (itr2.Value().GetType() == Obj.ObjType.e_dict)
                                        StructTree(itr2);
                                }
                            break;
                        case Obj.ObjType.e_stream:
                            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetObjNum() + " " + itr.Value().GetGenNum() + " R");
                            break;
                        case Obj.ObjType.e_string:
                            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetAsPDFText());
                            break;
                        case Obj.ObjType.e_bool:
                            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetBool());
                            break;
                        case Obj.ObjType.e_array:
                            for(int i = 0; i < itr.Value().Size(); ++i)
                            {
                                var el_array = itr.Value().GetAt(i);
                                Console.WriteLine(el_array);
                            }


                            //Console.WriteLine(itr.Key().GetName() + " | ");
                            //for (var itr2 = itr; itr2.HasNext(); itr2.Next()) // предположительно не работает 
                            //{
                            //    Console.WriteLine(itr2.Value().GetName());
                            //}
                            break;
                        case Obj.ObjType.e_number:
                            Console.WriteLine(itr.Key().GetName() + " | " + itr.Value().GetNumber());
                            break;
                    }
                    if (choose == Obj.ObjType.e_dict && itr.HasNext())
                    {
                        for (var itr2 = itr.Value().GetDictIterator(); itr2.HasNext(); itr2.Next())
                        {
                            StructTree(itr2);
                        }
                    }
                }
                
            }
        }

    }
}
