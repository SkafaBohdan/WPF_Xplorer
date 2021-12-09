using pdftron;
using pdftron.Filters;
using pdftron.SDF;
using System;
using pdftron.PDF;
using pdftron.Common;
using System.ComponentModel;
using pdftron.PDF.Struct;
using System.Collections;


namespace TestingPDFTron
{

	//В этом примере исследуются структура и содержимое PDF-документа с тегами и дампы.
	// информация о структуре в окно консоли.
	//
	// В документах PDF с тегами StructTree действует как центральное хранилище информации
	// связаны с логической структурой PDF-документа. Дерево состоит из StructElement-ов
	// и ContentItem-s, которые являются конечными узлами структурного дерева.
	//
	// Образец может быть расширен для доступа и извлечения элементов помеченного содержимого, например
	// как текст и изображения.
	class Program {
		static void PrintIndent(int indent) { Console.WriteLine(); for (int i = 0; i < indent; ++i) Console.Write("  "); }

		// Используется во фрагменте кода 1.
		static void ProcessStructElement(SElement element, int indent)
		{
			if (!element.IsValid())
			{
				return;
			}

			// Распечатайте информацию о типе и заголовке, если таковые имеются.
			PrintIndent(indent++);
			Console.Write("Type: " + element.GetType());
			if (element.HasTitle())
			{
				Console.Write(". Title: " + element.GetTitle());
			}

			int num = element.GetNumKids();
			for (int i = 0; i < num; ++i)
			{
				// Проверьте, является ли ребенок листовым узлом (т.е. это ContentItem).
				if (element.IsContentItem(i))
				{
					ContentItem cont = element.GetAsContentItem(i);
					ContentItem.Type type = cont.GetType();

					Page page = cont.GetPage();

					PrintIndent(indent);
					Console.Write("Элемент содержимого. Часть страницы №" + page.GetIndex());

					PrintIndent(indent);
					switch (type)
					{
						case ContentItem.Type.e_MCID:
						case ContentItem.Type.e_MCR:
							Console.Write("MCID: " + cont.GetMCID());
							break;
						case ContentItem.Type.e_OBJR:
							{
								Console.Write("OBJR ");
								Obj ref_obj = cont.GetRefObj();
								if (ref_obj != null)
									Console.Write("- Ссылочный объект#: " + ref_obj.GetObjNum());
							}
							break;
						default:
							break;
					}
				}
				else
				{  // ребенок - еще один узел StructElement.
					ProcessStructElement(element.GetAsStructElem(i), indent);
				}
			}
		}

		// Используется во фрагменте кода 2.
		static void ProcessElements(ElementReader reader)
		{
			Element element;
			while ((element = reader.Next()) != null)   // Read page contents
			{
				//В этом примере мы обрабатываем только пути и текст, но код может быть
				// расширен для обработки любого типа элемента.
				Element.Type type = element.GetType();
				if (type == Element.Type.e_path || type == Element.Type.e_text || type == Element.Type.e_path)
				{
					switch (type)
					{
						case Element.Type.e_path:               // Путь процесса...
							Console.WriteLine();
							Console.Write("PATH: ");
							break;
						case Element.Type.e_text:               // Текст процесса ...
							Console.WriteLine();
							Console.WriteLine("TEXT: " + element.GetTextString());
							break;
						case Element.Type.e_form:               // Форма процесса XObjects
							Console.WriteLine();
							Console.Write("FORM XObject: ");
							reader.FormBegin(); 
							ProcessElements(reader);
							reader.End(); 
							break;
					}

					//Проверьте, связан ли элемент с каким-либо структурным элементом.
					// Элементы содержимого являются конечными узлами структурного дерева.
					SElement struct_parent = element.GetParentStructElement();
					if (struct_parent.IsValid())
					{
						// Распечатайте тип, название и номер объекта родительского структурного элемента.
						Console.Write(" Type: " + struct_parent.GetType()
							+ ", MCID: " + element.GetStructMCID());
						if (struct_parent.HasTitle())
						{
							Console.Write(". Title: " + struct_parent.GetTitle());
						}
						Console.Write(", Obj#: " + struct_parent.GetSDFObj().GetObjNum());
					}
				}
			}
		}

		private static pdftron.PDFNetLoader pdfNetLoader = pdftron.PDFNetLoader.Instance();


		static void Main3(string[] args)
		{
			PDFNet.Initialize("demo:1638797506699:7b62627f0300000000e7c9c9b56709b97ba1634ef4ec416ab001038e68");
			
			string input_path = "C:/Users/skafa/Desktop/test.pdf";
			string output_path = "C:/Users/skafa/Desktop/";
			string input_path2 = "C:/Users/skafa/Downloads/PDF32000_2008-1-200.pdf";

			try  // Извлекаем логическую структуру из PDF-документа
			{
				using (PDFDoc doc = new PDFDoc(input_path2))
				{
					doc.InitSecurityHandler();

					bool example1 = true;
					bool example2 = true;

					if (example1)
					{
						Console.WriteLine("____________________________________________________________");
						Console.WriteLine("Пример 1 - Обход дерева логической структуры ...");

						STree tree = doc.GetStructTree();
						if (tree.IsValid())
						{
							Console.WriteLine("Документ имеет корень StructTree.");
							for (int i = 0; i < tree.GetNumKids(); ++i)
							{
								// Рекурсивно получить информацию о структуре для всех дочерних элементов.
								ProcessStructElement(tree.GetKid(i), 0);
							}
						}
						else
						{
							Console.WriteLine("Этот документ не содержит никакой логической структуры.");
						}

						Console.WriteLine();
						Console.WriteLine("Done 1.");
					}

					if (example2)
					{
						Console.WriteLine("____________________________________________________________");
						Console.WriteLine("Пример 2 - Получить элементы родительской логической структуры из");
						Console.WriteLine("элементы макета.");

						ElementReader reader = new ElementReader();
						for (PageIterator itr = doc.GetPageIterator(); itr.HasNext(); itr.Next())
						{
							reader.Begin(itr.Current());
							ProcessElements(reader);
							reader.End();
						}
						Console.WriteLine();
						Console.WriteLine("Done 2.");
					}

					//	doc.Save(output_path + "LogicalStructure.pdf", 0);
					Console.WriteLine("Done3");
				}
			}
			catch (PDFNetException e)
			{
				Console.WriteLine(e.Message);
			}
			PDFNet.Terminate();
		}
	}


	
}

