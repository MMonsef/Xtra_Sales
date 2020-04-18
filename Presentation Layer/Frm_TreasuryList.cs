using Data_Access_Layer;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class Frm_TreasuryList : Frm_Master
    {
        public override void New()
        {
            Frm_Treasurys frm = new Frm_Treasurys();
            frm.ShowDialog();
            base.New();
            
        }
        public Frm_TreasuryList()
        {
            InitializeComponent();
            RefreshData();
        }
        public override void RefreshData()
        {
            using (SalesDataContext db = new SalesDataContext())
            {
                Grid_Treasury.DataSource = db.treasurys.ToList();//GetNewBindingList();
            }
            dgv.OptionsView.ShowIndicator = false;
            dgv.Columns[nameof(treasurys.id)].Visible = false;
            dgv.Columns[nameof(treasurys.name)].Caption = "اسم الخزنة";
            dgv.Columns[nameof(treasurys.account_id)].Caption = "رقم الحساب";
            dgv.OptionsBehavior.Editable = false;                      // Make the grid read-only. 
            dgv.OptionsSelection.EnableAppearanceFocusedCell = false;  // Prevent the focused cell from being highlighted. 
            dgv.FocusRectStyle = DrawFocusRectStyle.RowFocus;          // Draw a dotted focus rectangle around the entire row. 
            dgv.CustomDrawColumnHeader += Dgv_CustomDrawColumnHeader;
            dgv.RowCellStyle += Dgv_RowCellStyle;

            base.RefreshData();
        }
       
        private void Dgv_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Dgv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Frm_TreasuryList_Load(object sender, EventArgs e)
        {
            Btn_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            Btn_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            dgv.DoubleClick += Dgv_DoubleClick;
        }

        private void Dgv_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs args = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(args.Location);
            if (info.InRow || info.InRowCell)
            {
                Frm_Treasurys Frm = new Frm_Treasurys(Convert.ToInt32(view.GetFocusedRowCellValue(nameof(treasurys.id))));
                Frm.ShowDialog();
                RefreshData();
            }
        }

    }
}
