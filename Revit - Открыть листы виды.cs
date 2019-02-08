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
    class Открыть_листы_виды : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            if ((DateTime.Now.Month == 8) || (DateTime.Now.Month == 7))
            {

                UIApplication uiapp = revit.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
                Document doc = uidoc.Document;

                foreach (ElementId jios in uidoc.Selection.GetElementIds())
                {
                    Autodesk.Revit.DB.View kisa = doc.GetElement(jios) as Autodesk.Revit.DB.View;
                    if (kisa != null)
                    {
                        uidoc.ActiveView = kisa;
                    }
                }
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
