using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace Data_Access_Layer
{
    public static class Master
    {
       
        public class ValueAndID
        {
            public int id { get; set; }
            public string name { get; set; }
        }


        #region ProductTypesList
        public static List<ValueAndID> ProductTypesList = new List<ValueAndID>()
        {
            new ValueAndID() { id = 0, name = "منتج" },
            new ValueAndID() { id = 1, name = "خدمي" }
        };
        public enum ProductType
        {
            Product,
            Service
        }
        #endregion

        #region InvoiceTypesList
        public static List<ValueAndID> InvoiceTypesList = new List<ValueAndID>()
        {
            new ValueAndID() { id = (int)InvoiceType.Purshases, name = "مشتريات" },
            new ValueAndID() { id = (int)InvoiceType.Sales, name = "مبيعات" },
            new ValueAndID() { id = (int)InvoiceType.PurchasesReturn, name = "مرتجع مشتريات" },
            new ValueAndID() { id = (int)InvoiceType.SalesReturn, name = "مرتجع مبيعات" },
        };
        public enum InvoiceType
        {
            Purshases,
            Sales,
            PurchasesReturn,
            SalesReturn,

        }
        #endregion

        #region PartTypesList
        public static List<ValueAndID> PartTypesList = new List<ValueAndID>()
        {
            new ValueAndID() { id = 0, name = "مورد" },
            new ValueAndID() { id = 1, name = "عميل" }
        };
        public enum PartType
        {
            Supplier,
            Customer,
        }
        #endregion

        public static bool isEditValueOfTypeInt(this LookUpEditBase edit)
        {
            var val = edit.EditValue;
            return (val is int || val is byte);
        }

        public static void IntializeData(this LookUpEdit  Lkp, object DataSource)
        {
            Lkp.IntializeData(DataSource, "name", "id");
        }
      
        public static void IntializeData(this LookUpEdit Lkp, object DataSource, string DisplayMember, string ValueMember)
        {
            Lkp.Properties.DataSource = DataSource;
            Lkp.Properties.DisplayMember = DisplayMember;
            Lkp.Properties.ValueMember = ValueMember;
            //Lkp.Properties.PopulateColumns();
            //Lkp.Properties.Columns[ValueMember].Visible = false;
        }
        
        public static void IntializeData(this GridLookUpEdit Lkp, object DataSource)
        {
            Lkp.IntializeData(DataSource, "name", "id");
        }

        public static void IntializeData(this GridLookUpEdit Lkp, object DataSource, string DisplayMember, string ValueMember)
        {
            Lkp.Properties.DataSource = DataSource;
            Lkp.Properties.DisplayMember = DisplayMember;
            Lkp.Properties.ValueMember = ValueMember;
        }

        public static void IntializeData(this RepositoryItemLookUpEditBase Repoo, object DataSource,GridColumn column,GridControl grid)
        {
            IntializeData(Repoo, DataSource, column,grid, "name", "id");
        }
        public static void IntializeData(this RepositoryItemLookUpEditBase Repoo, object DataSource, GridColumn column, GridControl grid,string DisplayMember, string ValueMember)
        {
            if (Repoo == null)
                Repoo = new RepositoryItemLookUpEdit();
            Repoo.DataSource = DataSource;
            Repoo.DisplayMember = DisplayMember;
            Repoo.ValueMember = ValueMember;
            Repoo.NullText = "";
            column.ColumnEdit = Repoo;
            if (grid != null)
                grid.RepositoryItems.Add(Repoo);
        }

        public static string GetNextNumberInString(string Number)
        {
            if (Number == string.Empty || Number == null)
                return "1";
            string str1 = "";
            foreach (Char c in Number)
                str1 = char.IsDigit(c) ? str1 + c.ToString() : "";
            if (str1 == string.Empty)
                return Number + "1";

            string str2 = str1.Insert(0, "1");
            str2 = (Convert.ToInt32(str2) + 1).ToString();
            string str3 = str2[0] == '1' ? str2.Remove(0, 1) : str2.Remove(0, 1).Insert(0, "1");

            int indx = Number.LastIndexOf(str1);
            Number = Number.Remove(indx);
            Number = Number.Insert(indx, str3);
            return Number;
        }

        public static Color color(int opacity, string HtmlColor)
        {
            if (opacity < 0 || opacity > 250) opacity = 250;

            Color FromHtml = ColorTranslator.FromHtml(HtmlColor);
            Color ArgbColor = Color.FromArgb(opacity, FromHtml.R, FromHtml.G, FromHtml.B);
            return ArgbColor;
        }

    }



}
