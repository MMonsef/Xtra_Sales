using Presentation_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TableDependency.SqlClient;

namespace Data_Access_Layer
{
    public static partial class Session
    {
        public static class GlobalSettings
        {

            public static byte BarcodeLength { get => 13; }
            public static Boolean ReadFromScalBarcode { get=> true; }
            public static string ScaleBarcodePrefix { get => "20"; }
            public static byte ProductCodeLength { get => 5; }
            public static byte ValueCodeLenghth { get => 5; }
            public static ReadValueMode ReadMode { get => ReadValueMode.Weight; }
            public static Boolean IgnoreCheckDigit { get => true; }
            public static byte DivideValueBy { get => 3; }

            public enum ReadValueMode
            {
                Weight,
                Price
            }
        }
        #region : Default Table

        /// <summary>
        ///  تعريف القيم الإفتراضية
        /// </summary>
        /// 
        public static class Defaults
        {
            public static int Drawer
            {
                get
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        int x = (db.treasurys.First().id);
                        return x;
                    }
                }
            }

            public static int Customer
            {
                get
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        int id = db.customers_suppliers.Where(x => x.Iscustomer == false).First().id;
                        return id;
                    }
                }
            }
            public static int Supplier
            {
                get
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        int id = db.customers_suppliers.Where(x => x.Iscustomer == false).First().id;
                        return id;
                    }
                }
            }

            public static int Store
            {
                get
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        int x = db.stores.First().id;
                        return x;
                    }
                }
            }

            public static int RawStore { get => 1; }  // تعريف مخزن المبيعات

        }
        #endregion

        #region : Load Default value of Products List

        private static BindingList<units> _Units;
        public static BindingList<units> units
        {
            get
            {
                if (_Units == null)
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        _Units = new BindingList<units>(db.units.ToList());
                    }
                }
                return _Units;
            }
        }

        private static BindingList<products> _Products;
        public static BindingList<products> Products
        {
            get
            {
                if (_Products == null)
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        _Products = new BindingList<products>(db.products.ToList());
                    }

                    SqlTableDependency<products> Products = new SqlTableDependency<products>
                        (Presentation_Layer.Properties.Settings.Default.SalesManagmentSystemConnectionString);

                    Products.OnChanged += DBWatcher.Products_Changed;
                    Products.Start();


                }
                return _Products;
            }
        }

        private static BindingList<ProductViewClass> productViewClass;
        public static BindingList<ProductViewClass> ProductView
        {
            get
            {
                if (productViewClass==null)
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        IEnumerable<ProductViewClass> data = from pr in Products
                                                             join cat in db.product_categories on pr.category_id equals cat.id
                                                             select new ProductViewClass
                                                             {
                                                                 id = pr.id,
                                                                 code = pr.code,
                                                                 name = pr.Name,
                                                                 category = cat.name,
                                                                 type = pr.type,
                                                                 details = pr.details,
                                                                 status = pr.status,
                                                                 Units = (from u in db.products_units
                                                                          where u.productid == pr.id
                                                                          join un in db.units on u.unitid equals un.id
                                                                          select new ProductViewClass.ProductUOMView
                                                                          {
                                                                              unitid = u.unitid,
                                                                              UnitName = un.name,
                                                                              factor = u.factor,
                                                                              sellprice = u.sellprice,
                                                                              buyprice = u.buyprice,
                                                                              barcode = u.barcode,

                                                                          }).ToList()
                                                             };
                        productViewClass = new BindingList<ProductViewClass>(data.ToList());
                    }
                }
                return productViewClass;
            }
        }
        public class ProductViewClass
        {
            public static ProductViewClass GetProduct(int ID)
            {
                ProductViewClass Obj;
                using (SalesDataContext db = new SalesDataContext())
                {
                    IEnumerable<ProductViewClass> data = from pr in Products
                                                         join cat in db.product_categories on pr.category_id equals cat.id
                                                         where pr.id == ID
                                                         select new ProductViewClass
                                                         {
                                                             id = pr.id,
                                                             code = pr.code,
                                                             name = pr.Name,
                                                             category = cat.name,
                                                             type = pr.type,
                                                             details = pr.details,
                                                             status = pr.status,
                                                             Units = (from u in db.products_units
                                                                      where u.productid == pr.id
                                                                      join un in db.units on u.unitid equals un.id
                                                                      select new ProductViewClass.ProductUOMView
                                                                      {
                                                                          unitid = u.unitid,
                                                                          UnitName = un.name,
                                                                          factor = u.factor,
                                                                          sellprice = u.sellprice,
                                                                          buyprice = u.buyprice,
                                                                          barcode = u.barcode,

                                                                      }).ToList()
                                                         };
                    Obj = data.First();
                };
                return Obj;
            }
            public int id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string category { get; set; }
            public string details { get; set; }
            public Boolean status { get; set; }
            public int type { get; set; }
            public List<ProductUOMView> Units { get; set; }
            public class ProductUOMView
            {
                public int unitid { get; set; }
                public string UnitName { get; set; }
                public Double factor { get; set; }
                public Double sellprice { get; set; }
                public Double buyprice { get; set; }
                public string barcode { get; set; }
            }
        }
            
        #endregion

        #region : Load Default value of Suppliers and Customers List

        private static BindingList<customers_suppliers> _Suppliers;
        public static BindingList<customers_suppliers> Suppliers
        {
            get
            {
                if (_Suppliers == null)
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        _Suppliers = new BindingList<customers_suppliers>(db.customers_suppliers.Where(x => x.Iscustomer == false).ToList());
                    }
                    SqlTableDependency<customers_suppliers> Suppliers = new SqlTableDependency<customers_suppliers>
                        (Presentation_Layer.Properties.Settings.Default.SalesManagmentSystemConnectionString);
                    
                        Suppliers.OnChanged += DBWatcher.Suppliers_Changed;
                        Suppliers.Start();
                }
                return _Suppliers;
            }

        }

        private static BindingList<customers_suppliers> _Customers;
        public static BindingList<customers_suppliers> Customers
        {
            get
            {
                if (_Customers == null) 
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        _Customers = new BindingList<customers_suppliers>(db.customers_suppliers.Where(x => x.Iscustomer == true).ToList());
                    }
                    SqlTableDependency<customers_suppliers> Customers = new SqlTableDependency<customers_suppliers>
                        (Presentation_Layer.Properties.Settings.Default.SalesManagmentSystemConnectionString);
                    
                        Customers.OnChanged += DBWatcher.Customers_Changed;
                        Customers.Start();
                }
                return _Customers;
            }
        }


        #endregion

        #region : Load Default value of Treasorys and Stores

        private static BindingList<treasurys> _Treasorys;
        public static BindingList<treasurys> Treasurys
        {
            get
            {
                if (_Treasorys == null)
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        _Treasorys = new BindingList<treasurys>(db.treasurys.ToList());
                    }
                }
                return _Treasorys;
            }
        }
        private static BindingList<stores> _Stores;
        public static BindingList<stores> Stores
        {
            get
            {
                if (_Stores == null)
                {
                    using (SalesDataContext db = new SalesDataContext())
                    {
                        _Stores = new BindingList<stores>(db.stores.ToList());
                    }
                }
                return _Stores;
            }
        }
        #endregion

    }
}
