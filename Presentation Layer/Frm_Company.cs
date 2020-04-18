using Data_Access_Layer;
using System.Linq;


namespace Presentation_Layer
{
    public partial class Frm_Company : Frm_Master
    {
        public Frm_Company()
        {
            InitializeComponent();
            New();
        }

        public override void New()
        {
            base.New();
        }
        public override void GetData()
        {
            using (SalesDataContext db = new SalesDataContext())
            {
                company_info Company = db.company_info.FirstOrDefault();
                {
                    if (!(Company == null))
                    {
                        Txt_Name.Text = Company.company_name;
                        Txt_Address.Text = Company.company_address;
                        Txt_Phone.Text = Company.company_mobile;
                        Txt_Email.Text = Company.company_email;
                        //Txt_Account.Text = Company.company_account;
                        //Txt_VatNumber.Text = Company.company_vatnumber;
                        //Pic_Logo.Image = Company.companu_logo;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            base.GetData();
        }
     
    public override void Save()
    {
        if (string.IsNullOrEmpty(Txt_Name.Text))
        {
            Txt_Name.ErrorText = "يرجى إدخال اسم الشركة  !";
            return;
        }
        else
        {
                using (SalesDataContext db = new SalesDataContext())
                {
                    Action = "Update";
                    company_info Company = db.company_info.FirstOrDefault();
                    if (Company == null)
                    {
                        Company = new company_info();
                        db.company_info.InsertOnSubmit(Company);
                        Action = "Insetr";
                    }
                    Company.company_name = Txt_Name.Text;
                    Company.company_address = Txt_Address.Text;
                    Company.company_email = Txt_Email.Text;
                    Company.company_mobile = Txt_Phone.Text;
                    //Company.company_account = Txt_Account.Text;
                    //Company.company_vatnumber = Txt_VatNumber.Text;
                    //Company.companu_logo = Pic_Logo.Image; 
                    db.SubmitChanges();
                }
            }
            base.Save();
        }
    }

    //private void Txt_Name_EditValueChanged(object sender, EventArgs e)
    //{
    //    if (string.IsNullOrEmpty(Txt_Name.Text))
    //    {

    //        Txt_Name.ErrorText = "يرجى إدخال اسم الشركة  !";
    //        return;
    //    }
    //    else
    //    {
    //        Txt_Name.ErrorText = "";
    //    }
    //}
    //}
}
