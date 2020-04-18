using System;
using System.Linq;

namespace Presentation_Layer
{
    public partial class Frm_Stores : Frm_Master
    {
        stores Store;
        public Frm_Stores()
        {
            InitializeComponent();
            New();
        }
        public Frm_Stores(int id)
        {
            InitializeComponent();
            var db = new SalesDataContext();
            Store = db.stores.Where(S => S.id == id).First();
            GetData();
        }
        public override void New()
        {
            Store = new stores();
            base.New();
        }
        public override void GetData()
        {
            Txt_name.Text = Store.name;
            base.GetData();
        }
        public override void SetData()
        {
            Store.name = Txt_name.Text;
            base.SetData();
        }
        public override void Save()
        {
            if (string.IsNullOrEmpty(Txt_name.Text))
            {
                Txt_name.ErrorText = "يرجى إدخال اسم المخزن  !";
                return;
            }
            else
            {
                var db = new SalesDataContext();

                if (Store.id == 0)
                {
                    Action = "Insert";
                    db.stores.InsertOnSubmit(Store);
                }
                else
                {
                    Action = "Update";
                    db.stores.Attach(Store);
                }
                SetData();
                db.SubmitChanges();
                base.Save();
                Txt_name.Text = string.Empty;
                Txt_name.Focus();

            }
        }
        private void Txt_name_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_name.Text))
            {

                Txt_name.ErrorText = "يرجى إدخال اسم المخزن  !";
                return;
            }
            else
            {
                Txt_name.ErrorText = "";
            }
        }
    }
}
