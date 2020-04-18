using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Presentation_Layer
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public string Action = "";
        public string ErrorText = "";

        public XtraForm1()
        {
            InitializeComponent();
        }
        public virtual void New()
        {
            GetData();
        }
        public virtual void Save()
        {
            switch (Action)
            {
                case "Insert":
                    MessageBox.Show("تم حفظ البيانات بنجاح");
                    break;
                case "Update":
                    MessageBox.Show("تم تعديل البيانات بنجاح");
                    Close();
                    break;
            }
            RefreshData();
        }
        public virtual void Delete()
        {

        }
        public virtual void GetData()
        {

        }
        public virtual void SetData()
        {

        }
        public virtual void RefreshData()
        {

        }

        private void Btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void Btn_New_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New();
        }

        private void Btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        private void gridLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}