using Presentation_Layer;
using System;
using System.Linq;
using System.Windows.Forms;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Abstracts;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace Data_Access_Layer
{
    class DBWatcher

    {

        /**************************************************/
        public static SqlTableDependency<products> Products;
        public static void Products_Changed(object sender, RecordChangedEventArgs<products>e)
        {
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Products.Remove(Session.Products.Single(x => x.id == e.Entity.id));
                        Session.ProductView.Remove(Session.ProductView.Single(x => x.id == e.Entity.id));
                        break;
                    case ChangeType.Insert:
                        Session.Products.Add(e.Entity);
                        Session.ProductView.Add(Session.ProductViewClass.GetProduct(e.Entity.id));
                        break;
                    case ChangeType.Update:
                        var Index = Session.Products.IndexOf(Session.Products.Single(x => x.id == e.Entity.id));
                        Session.Products.Remove(Session.Products.Single(x => x.id == e.Entity.id));
                        Session.Products.Insert(Index, e.Entity);

                        var ViwIndex = Session.ProductView.IndexOf(Session.ProductView.Single(x => x.id == e.Entity.id));
                        Session.ProductView.Remove(Session.ProductView.Single(x => x.id == e.Entity.id));
                        Session.ProductView.Insert(Index, Session.ProductViewClass.GetProduct(e.Entity.id)); ;

                        break;
                    default:
                        break;
                }
            }));
        }
        /*************************/
        public static SqlTableDependency<customers_suppliers> Suppliers;
        public static void Suppliers_Changed(object sender, RecordChangedEventArgs<customers_suppliers> e)
        {
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Suppliers.Remove(Session.Suppliers.Single(x => x.id == e.Entity.id));
                        break;
                    case ChangeType.Insert:
                        Session.Suppliers.Add(e.Entity);
                        break;
                    case ChangeType.Update:
                        var index = Session.Suppliers.IndexOf(Session.Suppliers.Single(x => x.id == e.Entity.id));
                        Session.Suppliers.Remove(Session.Suppliers.Single(x => x.id == e.Entity.id));
                        Session.Suppliers.Insert(index, e.Entity);
                        break;
                    default:
                        break;
                }


            }));
        }
        /*************************/
        public static SqlTableDependency<customers_suppliers> Customers;
        public static void Customers_Changed(object sender, RecordChangedEventArgs<customers_suppliers> e)
        {
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case ChangeType.None:
                        break;
                    case ChangeType.Delete:
                        Session.Customers.Remove(Session.Customers.Single(x => x.id == e.Entity.id));
                        break;
                    case ChangeType.Insert:
                        Session.Customers.Add(e.Entity);
                        break;
                    case ChangeType.Update:
                        var index = Session.Customers.IndexOf(Session.Customers.Single(x => x.id == e.Entity.id));
                        Session.Customers.Remove(Session.Customers.Single(x => x.id == e.Entity.id));
                        Session.Customers.Insert(index, e.Entity);
                        break;
                    default:
                        break;
                }

            }));
        }
        /*************************/
        public class SuppliersOnly : ITableDependencyFilter
        {
            public string Translate()
            {
                return "[IsCustomer]=0";
            }
        }
        public class CustomersOnly : ITableDependencyFilter
        {
            public string Translate()
            {
                return "[IsCustomer]=1";
            }
        }
    }        
}
