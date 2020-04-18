using System;
using Data_Access_Layer;
using DevExpress.XtraGrid.Views.Grid;

namespace Presentation_Layer
{
    public partial class Frm_StoresList : DevExpress.XtraEditors.XtraForm
    {
        public Frm_StoresList()
        {
            InitializeComponent();
        }
        private void Frm_StoresList_Load(object sender, EventArgs e)
        {
            using (SalesDataContext db = new SalesDataContext())
            {
                Grd_Stores.DataSource = db.stores.GetNewBindingList();
            }
            using (GridView dgv = Grd_Stores.MainView as GridView)
            {
                dgv.OptionsView.ShowIndicator = false;
                dgv.OptionsView.ShowColumnHeaders = false;
                dgv.Columns[0].Visible = false;
                dgv.Columns[1].Caption = "الفروع والمخازن";
                dgv.OptionsBehavior.Editable = false;                      // Make the grid read-only. 
                dgv.OptionsSelection.EnableAppearanceFocusedCell = false;  // Prevent the focused cell from being highlighted. 
                dgv.FocusRectStyle = DrawFocusRectStyle.RowFocus;          // Draw a dotted focus rectangle around the entire row. 
                dgv.RowCellStyle += Dgv_RowCellStyle;
                dgv.CustomDrawColumnHeader += Dgv_CustomDrawColumnHeader;
            }
            Grd_Stores.DoubleClick += Grd_Stores_DoubleClick;
        }

        private void Grd_Stores_DoubleClick(object sender, EventArgs e)
        {
            GridView dgv = Grd_Stores.MainView as GridView;
            int count = dgv.RowCount;
            if (count >= 0)
            {
                int id = Convert.ToInt32(dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["id"]));
                string name = (dgv.GetRowCellValue(dgv.FocusedRowHandle, dgv.Columns["name"])).ToString();
                Frm_Stores Frm = new Frm_Stores(id);
                Frm.ShowDialog();
            }
            else return;
        }
        private void Dgv_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        private void Dgv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
    }
}