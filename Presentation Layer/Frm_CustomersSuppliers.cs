using System;
using System.Data;
using System.Linq;

namespace Presentation_Layer
{

    public partial class Frm_CustomersSuppliers : Frm_Master
    {

        customers_suppliers Cust_Supp;
        bool IsCustomer;
        public Frm_CustomersSuppliers(bool iscustomer)
        {
            InitializeComponent();
            IsCustomer = iscustomer;
            New();
        }
        public Frm_CustomersSuppliers(int id)
        {
            InitializeComponent();
            LoadObject(id);
        }
        void LoadObject(int id)
        {
            using (SalesDataContext db = new SalesDataContext())
            {
                Cust_Supp = db.customers_suppliers.Single(x => x.id == id);
                IsCustomer = Convert.ToBoolean(Cust_Supp.Iscustomer);
                GetData();
            }
        }

        private void Frm_CustomersSuppliers_Load(object sender, EventArgs e)
        {
            this.Text = (IsCustomer) ? "شاشة العملاء" : "شاشة الموردين";
            Lc_name.Text = (IsCustomer) ? (string)Lc_name.Tag + " " + "العميل" : (string)Lc_name.Tag + " " + "المورد";
            Lc_address.Text = (IsCustomer) ? (string)Lc_address.Tag + " " + "العميل" : (string)Lc_address.Tag + " " + "المورد";
            Lc_phone.Text = (IsCustomer) ? (string)Lc_phone.Tag + " " + "العميل" : (string)Lc_phone.Tag + " " + "المورد";
            Lc_account.Text = (IsCustomer) ? (string)Lc_account.Tag + " " + "العميل" : (string)Lc_account.Tag + " " + "المورد";
            Txt_Account.ReadOnly = true;
        }
        public override void New()
        {
            Cust_Supp = new customers_suppliers();
            base.New();
        }
        public override void GetData()
        {
            Txt_Name.Text = Cust_Supp.name;
            Txt_Address.Text = Cust_Supp.address;
            Txt_Mobile.Text = Cust_Supp.mobile;
            Txt_Email.Text = Cust_Supp.email;
            Txt_Account.Text = Cust_Supp.account_id.ToString();
            base.GetData();
        }
        public override void SetData()
        {
            Cust_Supp.name = Txt_Name.Text;
            Cust_Supp.address = Txt_Address.Text;
            Cust_Supp.mobile = Txt_Mobile.Text;
            Cust_Supp.email = Txt_Email.Text;
            Cust_Supp.Iscustomer = IsCustomer;
            base.SetData();
        }
        bool IsEmpty()
        {
            if (Txt_Name.Text.Trim() == string.Empty)
            {
                Txt_Name.ErrorText = "هذا الحقل لايمكن ان يكون فارغا";
                return false;
            }
            var db = new SalesDataContext();
            if (db.customers_suppliers.Where(x => x.name.Trim() == Txt_Name.Text.Trim() &&
            x.Iscustomer == IsCustomer && x.id != Cust_Supp.id).Count() > 0)
            {
                Txt_Name.ErrorText = "هذا الاسم موجود مسبقا في قاعدة البيانات";
                return false;
            }
            return true;
        }
        public override void Save()
        {
            if (IsEmpty() == false)
            {
                return;
            }
            else
            {
                var db = new SalesDataContext();
                accounts Account;
                if (Cust_Supp.id == 0)
                {
                    Action = "Insert";
                    db.customers_suppliers.InsertOnSubmit(Cust_Supp);
                    Account = new accounts();
                    db.accounts.InsertOnSubmit(Account);
                }
                else
                {
                    Action = "Update";
                    db.customers_suppliers.Attach(Cust_Supp);
                    Account = db.accounts.First(S => S.id == Cust_Supp.account_id);
                }
                SetData();
                Account.name = Cust_Supp.name;
                //db.SubmitChanges();
                Cust_Supp.account_id = Account.id;
                db.SubmitChanges();
                //Txt_Name.Text = string.Empty;
                //Txt_Address.Text = string.Empty;
                //Txt_Mobile.Text = string.Empty;
                //Txt_Email.Text = string.Empty;
                //Txt_Name.Focus();
                base.Save();
            }
        }

    }
}
