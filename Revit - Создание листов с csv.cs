using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB.Architecture;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Microsoft.Office.Interop.Excel;


namespace RevitProgramm.Плагины
{ 
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

	public class Пример : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// Получаем доступ к интерфейсу и документу
			UIApplication uiApp = commandData.Application;
			UIDocument uidoc = uiApp.ActiveUIDocument;
			Document doc = uidoc.Document;
			Autodesk.Revit.ApplicationServices.Application app = uiApp.Application;

			//Создаем диалоговое окно для выбора файла
			string fileName;
			System.Windows.Forms.OpenFileDialog openDlg = new System.Windows.Forms.OpenFileDialog();
			openDlg.Title = "Select a file";
			openDlg.Filter = "Comma Separated Values (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

			openDlg.RestoreDirectory = true;

			System.Windows.Forms.DialogResult result = openDlg.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK)
			{
				fileName = openDlg.FileName;
				System.IO.StreamReader sr = new System.IO.StreamReader(fileName, Encoding.Default);


				FilteredElementCollector collector = new FilteredElementCollector(doc);
				collector.OfCategory(BuiltInCategory.OST_TitleBloc​ks);
				collector.WhereElementIsElementType();

				string csvLine = null;
				Transaction t = new Transaction(doc, "Create Sheets");
				t.Start();

				//Цикл проверяет ячейки построчно до первой пустой строки
				while ((csvLine = sr.ReadLine()) != null)
				{
					char[] separator = new char[] { ',' };
					string[] values = csvLine.Split(separator, StringSplitOptions.None);

					foreach (var word in values)
					{
						if (word.IndexOf("#") == 0) // или if (word.Contains("#"))
						{
							continue;
						}
						else
						{
							csvLine = word;
							ViewSheet sheet = ViewSheet.Create(doc, ElementId.InvalidElementId);
							string[] wordlist = word.Split(';');
							sheet.Name = wordlist[1];
							sheet.SheetNumber = wordlist[3] + "л." + wordlist[0];
							Autodesk.Revit.DB.Parameter section = sheet.LookupParameter("INF.L.РазделПроекта");
							section.Set(wordlist[2] + wordlist[3]);
							Autodesk.Revit.DB.Parameter sheetnumber = sheet.LookupParameter("INF.L.НомерЛиста");
							sheetnumber.Set(wordlist[0]);
							Autodesk.Revit.DB.Parameter projectstage = sheet.LookupParameter("INF.L.Стадия");
							projectstage.Set(wordlist[4]);
						}

					}
				}
				t.Commit();
			}
			return Autodesk.Revit.UI.Result.Succeeded;
		}
	}
}