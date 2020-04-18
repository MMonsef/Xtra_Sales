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
    public partial class Frm_Master : DevExpress.XtraEditors.XtraForm
    {
        public string Action = "";
        public static string ErrorText
        {
            get
            {
                return "هذا المجال إلزامي";
            }
        }

        public Frm_Master()
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
                    New();
                    break;
                case "Update":
                    MessageBox.Show("تم تعديل البيانات بنجاح");
                    Close();
                    break;
            }
           
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
        public virtual bool IsDataValide()
        {
            return true;
        }
      
        private void Btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (IsDataValide())
            {
                    Save(); 
            }
        }

        private void Btn_New_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New();
        }

        private void Btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

    }
}