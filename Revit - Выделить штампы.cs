using System;
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

namespace SmiDPaneL.Общее.Ускорители
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    class Выделить_штампы : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            if ((DateTime.Now.Month == 8) || (DateTime.Now.Month == 7))
            {
                UIApplication uiapp = revit.Application;
                Document doc = uiapp.ActiveUIDocument.Document;

                ICollection<ViewSheet> Elements = new List<ViewSheet>();
                foreach (ElementId gu in uiapp.ActiveUIDocument.Selection.GetElementIds())
                {
                    if ((doc.GetElement(gu) as ViewSheet) != null)
                    {
                        Elements.Add(doc.GetElement(gu) as ViewSheet);
                    }
                }
                IList<ElementId> elementstoselect = new List<ElementId>();
                foreach (ViewSheet ji in Elements)
                {
                    foreach (FamilyInstance jis in new FilteredElementCollector(doc, ji.Id).OfCategory(BuiltInCategory.OST_TitleBlocks).OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>())
                    {
                        if (jis.Symbol.FamilyName != "N-L-TBL - Apex")
                        {
                            elementstoselect.Add(jis.Id);
                        }
                    }
                }
                uiapp.ActiveUIDocument.Selection.SetElementIds(elementstoselect);
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
