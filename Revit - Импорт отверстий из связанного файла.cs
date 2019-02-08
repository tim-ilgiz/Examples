using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;


namespace PluginForSmiDPaneL.Блок_плагинов
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class Раз : IExternalCommand
	{
		public class CopyUseDestination : IDuplicateTypeNamesHandler
		{
			public DuplicateTypeAction OnDuplicateTypeNamesFound(DuplicateTypeNamesHandlerArgs args)
			{
				return DuplicateTypeAction.UseDestinationTypes;
			}
		}

		public Result Execute(
		ExternalCommandData commandData,
	    ref string message,
		ElementSet elements)
		{
			Document hostDoc = commandData.Application.ActiveUIDocument.Document;

			FilteredElementCollector links = new FilteredElementCollector(hostDoc).OfClass(typeof(RevitLinkInstance));
			Document linkedDoc = links.Cast<RevitLinkInstance>().FirstOrDefault().GetLinkDocument();

			FilteredElementCollector linkedFamCollector = new FilteredElementCollector(linkedDoc);
			ICollection<ElementId> ids = new FilteredElementCollector(linkedDoc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_MechanicalEquipment).ToElementIds();

			if (ids.Count == 0)
			{
				TaskDialog.Show("Copy Paste", "В связанном файле не содержатся элементы данной категории");
			}
            else
            {
                Transaction targetTrans = new Transaction(hostDoc, "Копирование и вставка элементов");
                CopyPasteOptions copyOptions = new CopyPasteOptions();
                copyOptions.SetDuplicateTypeNamesHandler(new CopyUseDestination());
                targetTrans.Start();

                ElementTransformUtils.CopyElements(linkedDoc, ids, hostDoc, null, copyOptions);
                targetTrans.Commit();
            }
            return Result.Succeeded;
		}
	}
}