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
    class Выбор_из_группы : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            if ((DateTime.Now.Month == 8) || (DateTime.Now.Month == 7))
            {
                UIApplication uiapp = revit.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
                Document doc = uidoc.Document;

                Selection selection = uidoc.Selection;
                ICollection<ElementId> rooms = uidoc.Selection.GetElementIds();
                if (rooms.Count == 0)
                {
                    try
                    {
                        ICollection<Reference> Elements = selection.PickObjects(ObjectType.Element, Char.ConvertFromUtf32(1042) + Char.ConvertFromUtf32(1099) + Char.ConvertFromUtf32(1073) + Char.ConvertFromUtf32(1077) + Char.ConvertFromUtf32(1088) + Char.ConvertFromUtf32(1080) + Char.ConvertFromUtf32(1090) + Char.ConvertFromUtf32(1077) + Char.ConvertFromUtf32(32) + Char.ConvertFromUtf32(1075) + Char.ConvertFromUtf32(1088) + Char.ConvertFromUtf32(1091) + Char.ConvertFromUtf32(1087) + Char.ConvertFromUtf32(1087) + Char.ConvertFromUtf32(1099));
                        foreach (Reference gi in Elements)
                        {
                            rooms.Add(doc.GetElement(gi).Id);
                        }
                    }
                    catch { }
                }
                ICollection<ElementId> elementinview = new FilteredElementCollector(doc, doc.ActiveView.Id).ToElementIds();
                ICollection<ElementId> neededtoselect = new Collection<ElementId>();
                foreach (ElementId jiji in rooms)
                {
                    Autodesk.Revit.DB.Group grip = doc.GetElement(jiji) as Autodesk.Revit.DB.Group;
                    if (grip != null)
                    {
                        foreach (ElementId jiu in grip.GetMemberIds())
                        {
                            if (elementinview.Contains(jiu))
                            {
                                try
                                {
                                    if ((doc.GetElement(jiu).IsHidden(doc.ActiveView) == false) && (doc.GetElement(jiu).Category.get_Visible(doc.ActiveView) == true))
                                    {
                                        neededtoselect.Add(jiu);
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
                Transaction siska = new Transaction(doc, "SmiDPaneL");
                siska.Start();
                uidoc.Selection.SetElementIds(neededtoselect);
                siska.Commit();
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
