using pdftron;
using pdftron.Filters;
using pdftron.PDF;
using pdftron.SDF;
using System;


namespace TestingPDFTron
{
    class Program2
    {
       
        static void Main2(string[] args)
        {
            PDFNet.Initialize("demo:1638797506699:7b62627f0300000000e7c9c9b56709b97ba1634ef4ec416ab001038e68");
            //PDFTest()

            string input_path = "C:/Users/skafa/Desktop/test.pdf";

            #region 2
            //bufer
            MappedFile file = new MappedFile(input_path);
            //SDFDoc file_sdf = new SDFDoc(input_path);

            long file_sz = file.FileSize();
            FilterReader reader = new FilterReader(file);
            byte[] mem = new byte[(int)file_sz];
            long bytes_read = reader.Read(mem);
            PDFDoc file_mem = new PDFDoc(mem, (int)file_sz);
            SDFDoc file_sdf = new SDFDoc(mem, (int)file_sz);

            file_mem.InitSecurityHandler();

            Obj stream = file_sdf.GetTrailer();
            Obj stream2 = file_mem.GetTrailer();

            Filter dec_stm = stream.GetDecodedStream(); // не работает потому что в трейлере нету потока
            Filter dec_stm2 = stream2.GetDecodedStream();// не работает потому что в трейлере нету потока

            //чтобы записать фильтр в файл, просто используйте Filter.WriteToFile():
            //dec_stm.WriteToFile(output_filename, false);

            FilterReader reader1 = new FilterReader(dec_stm);
            FilterReader reader2 = new FilterReader(dec_stm2);

            Console.WriteLine(reader1);
            Console.WriteLine(reader2);
            #endregion

            #region 3
            //	// Relative path to the folder containing test files.
            //	string input_path = "C:/Users/skafa/Desktop/test.pdf";

            //	try
            //	{
            //		Console.WriteLine("-------------------------------------------------");
            //		Console.WriteLine("Пример 1 - извлечение текстовых данных со всех страниц документа..");

            //		// Open the test file
            //		Console.WriteLine("Opening the input pdf...");
            //		using (PDFDoc doc = new PDFDoc(input_path))
            //		using (ElementReader page_reader = new ElementReader())
            //		{
            //			doc.InitSecurityHandler();

            //			PageIterator itr;
            //			for (itr = doc.GetPageIterator(); itr.HasNext(); itr.Next())        //  Read every page
            //			{
            //				page_reader.Begin(itr.Current());
            //				ProcessElements(page_reader);
            //				page_reader.End();
            //			}
            //			Console.WriteLine("Done.");
            //		}
            //	}
            //	catch (PDFNetException e)
            //	{
            //		Console.WriteLine(e.Message);
            //	}
            //	PDFNet.Terminate();
            //}

            #endregion
        }
    }
}



#region 3

//        public static void PDFTest()
//        {
//            string filename = "C:/Users/skafa/Desktop/test.pdf";

//            MappedFile file = new MappedFile(filename);
//            SDFDoc doc_stream = new SDFDoc(filename);


//            long file_sz = file.FileSize();
//            FilterReader file_reader = new FilterReader(file);
//            byte[] mem = new byte[(int)file_sz];
//            long bytes_read = file_reader.Read(mem);
//            SDFDoc doc_mem = new SDFDoc(mem, (int)file_sz);

//            Console.WriteLine();
//            using (TextExtractor txt = new TextExtractor())
//            {
//                if (true)
//                {
//                    using (PDFDoc doc = new PDFDoc(filename))
//                    {
//                        Page page = doc.GetPage(1);
//                        txt.Begin(page);  // Read the page.
//                        String text = txt.GetAsXML(TextExtractor.XMLOutputFlags.e_words_as_elements | TextExtractor.XMLOutputFlags.e_output_bbox | TextExtractor.XMLOutputFlags.e_output_style_info);
//                        Console.WriteLine("\n\n- GetAsXML  --------------------------\n{0}", text);
//                        Console.WriteLine("-----------------------------------------------------------");

//                        // Sample code showing how to use low-level text extraction APIs.
//                        if (true)
//                        {
//                            try
//                            {
//                                LowLevelTextExtractUtils util = new LowLevelTextExtractUtils();


//                                // Example 1. Extract all text content from the document
//                                using (ElementReader reader = new ElementReader())
//                                {
//                                    PageIterator itr = doc.GetPageIterator();
//                                    for (; itr.HasNext(); itr.Next()) //  Read every page
//                                    {
//                                        reader.Begin(itr.Current());
//                                        LowLevelTextExtractUtils.DumpAllText(reader);
//                                        reader.End();
//                                    }

//                                }

//                            }
//                            finally { }
//                            }



//                        if (!doc_mem.InitSecurityHandler())
//                        {
//                            Console.WriteLine("Document authentication error", "PDFViewWPF Error");
//                        }


//                        //пример того, как добраться до корня страницы документа:
//                        Obj trailer = doc_stream.GetTrailer();
//                        DictIterator root_itr = trailer.Get("Root");
//                        Obj root = root_itr.Value();
//                        DictIterator pages_itr = root.Get("Pages");
//                        Obj pages = pages_itr.Value();


//                    }
//                }
//            }
//        }
//    }
//}


//class LowLevelTextExtractUtils
//{
//    // A utility method used to dump all text content in the 
//    // console window.
//    public static void DumpAllText(ElementReader reader)
//    {
//        Element element;
//        while ((element = reader.Next()) != null)
//        {
//            switch (element.GetType())
//            {
//                case Element.Type.e_text_begin:
//                    Console.WriteLine("\n--> Text Block Begin");
//                    break;
//                case Element.Type.e_text_end:
//                    Console.WriteLine("\n--> Text Block End");
//                    break;
//                case Element.Type.e_text:
//                    {
//                        Rect bbox = new Rect();
//                        element.GetBBox(bbox);
//                        // Console.WriteLine("\n--> BBox: {0}, {1}, {2}, {3}", bbox.x1, bbox.y1, bbox.x2, bbox.y2);

//                        String txt = element.GetTextString();
//                        Console.Write(txt);
//                        Console.WriteLine("");
//                        break;
//                    }
//                case Element.Type.e_text_new_line:
//                    {
//                        // Console.WriteLine("\n--> New Line");
//                        break;
//                    }
//                case Element.Type.e_form: // Process form XObjects
//                    {
//                        reader.FormBegin();
//                        DumpAllText(reader);
//                        reader.End();
//                        break;
//                    }
//            }
//        }
//    }
//}

#endregion