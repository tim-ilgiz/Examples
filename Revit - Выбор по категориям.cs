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
    class Выбор_по_категориям : IExternalCommand
    {
        DataGridView container = null;
        object valu = 0;
        DataGridViewTextBoxColumn newColumn2 = null;
        DataGridViewTextBoxColumn newColumn3 = null;
        IList<int> rows = new List<int>();
        int danet = 0;
        public void lisyop(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (container.Rows.Count > 20)
            {
                newColumn2.Width = 490;
            }
        }
        private void lolol2(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ((e.X > 0) && (e.X < 50))
            {
                danet = 0;
            }
            else
            {
                danet = 1;
            }
        }
        private void lolol(object sender, EventArgs e)
        {
            if (danet == 1)
            {
                rows.Clear();
            }
            else
            {
                foreach (DataGridViewRow jios in container.SelectedRows)
                {
                    if (rows.Contains(jios.Index) == false)
                    {
                        rows.Clear();
                        break;
                    }
                }
            }


            foreach (DataGridViewRow jios in container.SelectedRows)
            {
                if (rows.Contains(jios.Index) == false)
                {
                    rows.Add(jios.Index);
                }
            }
            foreach (int ui in rows)
            {
                container[0, ui].Selected = true;
            }
        }
        private void lolol3(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                int value = 1 - Int32.Parse(container[0, e.RowIndex].Value.ToString());
                foreach (int ikota in rows)
                {
                    container[0, ikota].Value = value;
                }
            }
        }

        private void stopselect(object sender, EventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            if ((DateTime.Now.Month == 8) || (DateTime.Now.Month == 7))
            {

                UIApplication uiapp = revit.Application;
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
                Document doc = uidoc.Document;


                System.Windows.Forms.Form newform = new System.Windows.Forms.Form();
                newform.ShowIcon = false;
                newform.StartPosition = FormStartPosition.CenterScreen;
                newform.Width = 340;
                newform.Height = 465;
                newform.BackColor = System.Drawing.Color.White;
                newform.FormBorderStyle = FormBorderStyle.Fixed3D;
                newform.MinimizeBox = false;
                newform.MaximizeBox = false;
                newform.TopMost = true;
                newform.AutoScroll = true;
                newform.Text = "Настройки фильтра";

                Button killer = new Button();
                killer.FlatStyle = FlatStyle.System;
                killer.Width = 300;
                killer.Height = 25;
                killer.DialogResult = DialogResult.OK;
                killer.Location = new System.Drawing.Point(10, 390);
                killer.Text = "Выбрать элементы";
                newform.Controls.Add(killer);

                Button stopper = new Button();
                stopper.Width = 1;
                stopper.Height = 1;
                stopper.DialogResult = DialogResult.Cancel;
                stopper.Location = new System.Drawing.Point(1, 1);
                newform.Controls.Add(stopper);



                GroupBox mestnost = new GroupBox();
                mestnost.FlatStyle = FlatStyle.System;
                mestnost.Text = "Выберите категории";
                mestnost.Location = new System.Drawing.Point(10, 10);
                mestnost.Width = 300;
                mestnost.Height = 300;
                newform.Controls.Add(mestnost);


                container = new DataGridView();
                container.Location = new System.Drawing.Point(15, 25);
                container.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                container.Height = 260;
                container.RowsAdded += new DataGridViewRowsAddedEventHandler(lisyop);
                container.Width = 270;
                container.MultiSelect = true;
                container.BackgroundColor = System.Drawing.Color.White;
                container.AllowUserToAddRows = false;
                container.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                container.RowHeadersVisible = false;
                container.SelectionChanged += new EventHandler(lolol);
                container.MouseMove += new System.Windows.Forms.MouseEventHandler(lolol2);
                container.CellContentClick += new DataGridViewCellEventHandler(lolol3);
                container.AllowUserToDeleteRows = false;
                container.AllowUserToOrderColumns = false;
                container.AllowUserToResizeColumns = false;
                container.AllowUserToResizeRows = false;


                DataGridViewCheckBoxColumn newColumn1 = new DataGridViewCheckBoxColumn();
                newColumn1.Width = 50;
                newColumn1.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                newColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
                newColumn1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                newColumn1.Name = "Выбор";
                newColumn1.ReadOnly = true;
                newColumn1.FalseValue = 0;
                newColumn1.TrueValue = 1;
                container.Columns.Add(newColumn1);

                newColumn2 = new DataGridViewTextBoxColumn();
                newColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                newColumn2.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                newColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
                newColumn2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                newColumn2.Name = "Имя категории";
                newColumn2.ReadOnly = true;
                container.Columns.Add(newColumn2);

                newColumn3 = new DataGridViewTextBoxColumn();
                newColumn3.Width = 100;
                newColumn3.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                newColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
                newColumn3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                newColumn3.Name = "Id";
                newColumn3.Visible = false;
                newColumn3.ReadOnly = true;
                container.Columns.Add(newColumn3);

                mestnost.Controls.Add(container);

                newform.AcceptButton = killer;
                newform.CancelButton = stopper;



                GroupBox mestnost2 = new GroupBox();
                mestnost2.FlatStyle = FlatStyle.System;
                mestnost2.Text = "Дополнительные возможности";
                mestnost2.Location = new System.Drawing.Point(10, 320);
                mestnost2.Width = 300;
                mestnost2.Height = 60;
                newform.Controls.Add(mestnost2);

                CheckBox ci = new CheckBox();
                ci.Location = new System.Drawing.Point(15, 25);
                ci.Width = 260;
                ci.FlatStyle = FlatStyle.System;
                ci.Text = "Включая элементы внутри групп";
                mestnost2.Controls.Add(ci);


                RegistryKey tcel = Registry.CurrentUser;
                RegistryKey tcel1 = tcel.CreateSubKey(@"Software\SmiDSoftware\SmiDCategoryFilter", RegistryKeyPermissionCheck.ReadWriteSubTree);
                IList<string> kikals = tcel1.GetValueNames().ToList();

                IList<ElementId> allelemnents = new FilteredElementCollector(doc, doc.ActiveView.Id).ToElementIds().ToList();
                IList<Category> allcati = new List<Category>();
                IList<int> allcatiID = new List<int>();
                foreach (ElementId jiu in allelemnents)
                {
                    try
                    {
                        if (allcatiID.Contains(doc.GetElement(jiu).Category.Id.IntegerValue) == false)
                        {
                            allcati.Add(doc.GetElement(jiu).Category);
                            allcatiID.Add(doc.GetElement(jiu).Category.Id.IntegerValue);
                        }
                    }
                    catch { }
                }
                IOrderedEnumerable<Category> legends3 = from Category vp in allcati orderby vp.Name ascending select vp;

                foreach (Category jios in legends3.ToList())
                {
                    if (kikals.Contains(jios.Name))
                    {
                        container.Rows.Add(Int32.Parse(tcel1.GetValue(jios.Name).ToString()), jios.Name, jios.Id.IntegerValue);
                    }
                    else
                    {
                        container.Rows.Add(0, jios.Name, jios.Id.IntegerValue);
                    }
                }

                newform.ShowDialog();

                if (newform.DialogResult == DialogResult.OK)
                {
                    ICollection<ElementId> elementinview = new FilteredElementCollector(doc, doc.ActiveView.Id).ToElementIds();
                    ICollection<ElementId> neededtoselect = new Collection<ElementId>();

                    for (int i = 0; i < container.Rows.Count; i++)
                    {
                        tcel1.SetValue(container[1, i].Value.ToString(), container[0, i].Value.ToString(), RegistryValueKind.ExpandString);
                        if (container[0, i].Value.ToString() == "1")
                        {
                            if (ci.Checked == false)
                            {
                                foreach (ElementId jiji in elementinview)
                                {
                                    try
                                    {
                                        if ((doc.GetElement(jiji).Category.Id.IntegerValue.ToString() == container[2, i].Value.ToString()) && (doc.GetElement(jiji).GroupId == ElementId.InvalidElementId))
                                        {
                                            neededtoselect.Add(jiji);
                                        }
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                foreach (ElementId jiji in elementinview)
                                {
                                    try
                                    {
                                        if ((doc.GetElement(jiji).Category.Id.IntegerValue.ToString() == container[2, i].Value.ToString()))
                                        {
                                            neededtoselect.Add(jiji);
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
            }
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
