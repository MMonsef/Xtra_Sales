using Data_Access_Layer;
using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Presentation_Layer
{
    public partial class Frm_Main : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private void Menu_ElementClick_1(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            string Tag = e.Element.Tag as string;
            if (Tag != string.Empty)
            {
                OpenForm(Tag);
            }
        }

        private void OpenForm(string Name)
        {
            Form Frm = null;
            switch (Name)
            {
            
                case "Frm_Suppliers":
            
                Frm = new Frm_CustomersSuppliers(false);
                break;

                case "Frm_Customers":
            
                Frm = new Frm_CustomersSuppliers(true);
                break;
                
                case "Frm_SuppliersList":
                    Frm = new Frm_CustSupList(false);
                                   
                    break;
            
                case "Frm_CustomersList":
                    Frm = new Frm_CustSupList(true);
                    break;
                case "Frm_PurchaseInvoice":
                    Frm = new Frm_Invoices(Master.InvoiceType.Purshases);
                    break;

                default :

                var Ins = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == Name);
                if (Ins != null)
                {
                    Frm = Activator.CreateInstance(Ins) as Form;
                    if (Application.OpenForms[Frm.Name] != null)
                    {
                        Frm = Application.OpenForms[Frm.Name];
                    }
                    else
                    {
                            Frm.Show();
                            //if (!fluentDesignFormContainer1.Controls.Contains(Frm))
                            //{
                            //    fluentDesignFormContainer1.Controls.Add(Frm);
                            //    Frm.Dock = DockStyle.Fill;
                            //    Frm.BringToFront();
                            //}
                            //Frm.BringToFront();
                        }
                    Frm.BringToFront();
                }
                    break;
            }
            if (Frm != null) Frm.Show();
            //if (!fluentDesignFormContainer1.Controls.Contains(Frm))
            //{
            //    fluentDesignFormContainer1.Controls.Add(Frm);
            //    Frm.Dock = DockStyle.Fill;
            //    Frm.BringToFront();
            //}
            //Frm.BringToFront();
        }

        private void fluentDesignFormContainer1_Click(object sender, EventArgs e)
        {

        }
    }

    
}
