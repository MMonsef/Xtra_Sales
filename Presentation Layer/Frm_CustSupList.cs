using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Access_Layer;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Presentation_Layer
{
    public partial class Frm_CustSupList : Frm_Master
    {
        bool IsCustomer;
        public Frm_CustSupList(bool isCustomer)
        {
            InitializeComponent();
            IsCustomer = isCustomer;
            RefreshData();
            this.Text = IsCustomer ? "قائمة العملاء" : "قائمة الموردين";
        }

        public override void New()
        {
            Frm_CustomersSuppliers Frm = new Frm_CustomersSuppliers(IsCustomer);
            Frm.Show();

        }

        public override void RefreshData()
        {
            using (SalesDataContext db =new SalesDataContext())
            {
                Grid_CustSup.DataSource =(IsCustomer)? Session.Customers.ToList():Session.Suppliers.ToList();
            }
            base.RefreshData();
        }

        private void Dgv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Dgv_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Frm_CustSupList_Load(object sender, EventArgs e)
        {
            dgv.Columns[nameof(customers_suppliers.id)].Visible = false;
            dgv.Columns[nameof(customers_suppliers.account_id)].Visible = false;
            dgv.Columns[nameof(customers_suppliers.Iscustomer)].Visible = false;
            dgv.Columns[nameof(customers_suppliers.name)].Caption = $"اسم{(IsCustomer ? " " + "العميل" : " " + "المورد")}";
            dgv.Columns[nameof(customers_suppliers.address)].Caption = $"عنوان{(IsCustomer ? " " + "العميل" : " " + "المورد")}";
            dgv.Columns[nameof(customers_suppliers.mobile)].Caption = $"هاتف{(IsCustomer ? " " + "العميل" : " " + "المورد")}";
            dgv.Columns[nameof(customers_suppliers.email)].Caption = $"ايميل{(IsCustomer ? " " + "العميل" : " " + "المورد")}";
              
            dgv.OptionsView.ShowIndicator = false;
            dgv.OptionsBehavior.Editable = false;                      // Make the grid read-only. 
            dgv.OptionsSelection.EnableAppearanceFocusedCell = false;  // Prevent the focused cell from being highlighted. 
            dgv.FocusRectStyle = DrawFocusRectStyle.RowFocus;          // Draw a dotted focus rectangle around the entire row. 
            dgv.CustomDrawColumnHeader += Dgv_CustomDrawColumnHeader;
            dgv.RowCellStyle += Dgv_RowCellStyle;
            Btn_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            Btn_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            dgv.DoubleClick += Dgv_DoubleClick;
            if (IsCustomer)
                Session.Customers.ListChanged += Customers_ListChanged;
            else
                Session.Suppliers.ListChanged += Suppliers_ListChanged;
        }

        private void Customers_ListChanged(object sender, ListChangedEventArgs e)
        {
            RefreshData();
        }

        private void Suppliers_ListChanged(object sender, ListChangedEventArgs e)
        {
            RefreshData();
        }

        private void Dgv_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);
            if (info.InRow || info.InRowCell)
            {
                Frm_CustomersSuppliers Frm = new Frm_CustomersSuppliers(Convert.ToInt32(view.GetFocusedRowCellValue(nameof(treasurys.id))));
                Frm.Show();
            }
        }
    }
}
