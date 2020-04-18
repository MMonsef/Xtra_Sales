using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Data_Access_Layer;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Presentation_Layer
{
    public partial class Frm_ProductsList : Frm_Master
    {
        public Frm_ProductsList()
        {
            InitializeComponent();
            RefreshData();
        }
        public override void New()
        {
            Frm_Products Frm = new Frm_Products();
            Frm.Show();
        }
        private void Frm_ProductsList_Load(object sender, EventArgs e)
        {
            dgv.OptionsBehavior.Editable = false;
            dgv.OptionsSelection.EnableAppearanceFocusedCell = false;  // Prevent the focused cell from being highlighted. 
            dgv.FocusRectStyle = DrawFocusRectStyle.RowFocus;          // Draw a dotted focus rectangle around the entire row. 
            Btn_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            Btn_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            dgv.OptionsDetail.ShowDetailTabs = false;

            dgv.DoubleClick += Dgv_DoubleClick;
            dgv.CustomColumnDisplayText += Dgv_CustomColumnDisplayText;
            dgv.RowCellStyle += Dgv_RowCellStyle;
            dgv.CustomDrawColumnHeader += Dgv_CustomDrawColumnHeader;
            Grid_Products.ViewRegistered += Grid_Products_ViewRegistered;

            var ins = new Session.ProductViewClass();

            dgv.Columns[nameof(ins.code)].Caption = "كود الصنف";
            dgv.Columns[nameof(ins.name)].Caption = "إسم الصنف";
            dgv.Columns[nameof(ins.category)].Caption = "فئة الصنف";
            dgv.Columns[nameof(ins.type)].Caption = "نوع الصنف";
            dgv.Columns[nameof(ins.details)].Caption = "وصف الصنف";
            dgv.Columns[nameof(ins.status)].Caption = "حالة الصنف";
            dgv.Columns[nameof(ins.id)].Visible = false;
        }

        private void Grid_Products_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            if (e.View.LevelName=="UOM")
            {
                products_units ins = new products_units();
                {
                    GridView view = e.View as GridView;
                    view.OptionsView.ShowViewCaption = true;
                    view.ViewCaption = "وحدات القياس";
                    view.Columns["UnitName"].Caption = "إسم الوحدة";
                    view.Columns[nameof(ins.factor)].Caption = "معامل التحويل";
                    view.Columns[nameof(ins.sellprice)].Caption = "سعر الشراء";
                    view.Columns[nameof(ins.buyprice)].Caption = "سعر البيع";
                    view.Columns[nameof(ins.barcode)].Caption = "باركود الوحدة";
                }
            }
        }

        private void Dgv_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);
            if (info.InRow || info.InRowCell)
            {
                Frm_Products Frm = new Frm_Products(Convert.ToInt32(view.GetFocusedRowCellValue(nameof(products.id))));
                Frm.Show();
            }
        }

        private void Dgv_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Dgv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Dgv_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == nameof(products.type))
            {
                e.DisplayText = Master.ProductTypesList.Single(x => x.id == Convert.ToInt32(e.Value)).name;
            }
        }
        public override void RefreshData()
        {
            base.RefreshData();

            Grid_Products.DataSource = Session.ProductView;

        }
    }
}
