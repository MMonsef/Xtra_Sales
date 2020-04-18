using Data_Access_Layer;
using System;
using System.Linq;

namespace Presentation_Layer
{
    public partial class Frm_Treasurys : Frm_Master
    {
        private treasurys Treasury;
        public Frm_Treasurys()
        {
            InitializeComponent();
            New();
        }
        public Frm_Treasurys(int id)
        {
            InitializeComponent();
            LoadTreasury(id);
        }
        void LoadTreasury(int id)
        {
            using (SalesDataContext db = new SalesDataContext())
            {
                Treasury = db.treasurys.Single(x => x.id == id);
                GetData();
            }
        }
        public override void New()
        {
            Treasury = new treasurys();
            base.New();
        }
        public override void GetData()
        {
            Txt_name.Text = Treasury.name;
            base.GetData();
        }
        public override void SetData()
        {
            Treasury.name = Txt_name.Text;
            base.SetData();
        }
        public override void Save()
        {
            if (string.IsNullOrEmpty(Txt_name.Text))
            {
                Txt_name.ErrorText = "يرجى إدخال اسم الخزنة  !";
                return;
            }
            else
            {
                var db = new SalesDataContext();
                accounts Account;

                if (Treasury.id == 0)
                {
                    Action = "Insert";
                    Account = new accounts();
                    db.treasurys.InsertOnSubmit(Treasury);
                    db.accounts.InsertOnSubmit(Account);
                }
                else
                {
                    Action = "Update";
                    db.treasurys.Attach(Treasury);
                    Account = db.accounts.Single(S => S.id == Treasury.account_id);
                }
                SetData();
                Account.name = Treasury.name;
                db.SubmitChanges();
                Treasury.account_id = Account.id;
                db.SubmitChanges();
                Txt_name.Text = string.Empty;
                Txt_name.Focus();
                base.Save();
            }
        }
        private void Txt_name_EditValueChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_name.Text))
            {

                Txt_name.ErrorText = "يرجى إدخال اسم الخزنة  !";
                return;
            }
            else
            {
                Txt_name.ErrorText = "";
            }
        }
    }
}
