using Data_Access_Layer;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;



namespace Presentation_Layer
{
    public partial class Frm_Products : Frm_Master
    {
        
        products Product;
        RepositoryItemLookUpEdit upEdit = new RepositoryItemLookUpEdit();
        RepositoryItemCalcEdit calcEdit = new RepositoryItemCalcEdit();
        SalesDataContext sdb = new SalesDataContext();
        products_units ins = new products_units();

        public Frm_Products(int id)
        {
            InitializeComponent();
            RefreshData();
            LoadProduct(id);

            this.Text = string.Format("بيانات الصنف : {0}", Product.Name);
        }

        void LoadProduct(int id)
        {
            using (SalesDataContext db = new SalesDataContext())
            {
                Product = db.products.Single(Pr => Pr.id == id);
            }
            GetData();
        }

        public Frm_Products()
        {
            InitializeComponent();
            RefreshData();
            New();
        }

        public override void New()
        {
            Product = new products()
            {
                code = GetNewProductCode(),
                status = true,
               
            }; base.New();

            var data = dgv.DataSource as BindingList<products_units>;
            var db = new SalesDataContext();
            if (db.units.Count() == 0)
            {
                db.units.InsertOnSubmit(new units() { name = "قطعة" });
                db.SubmitChanges();
                RefreshData();
            }


            Text = $"إضافة صنف جديد";

            data.Add(new products_units() { factor = 1, unitid = db.units.First().id, barcode = Master.GetNextNumberInString("00000000") });
        }

        public override void RefreshData()
        {
            using (SalesDataContext db = new SalesDataContext())
            {
                Txt_Category.Properties.DataSource = db.product_categories.ToList()
                .Where(x => db.product_categories.Where(w => w.parent_id == x.id).Count() == 0).ToList();
                upEdit.DataSource = db.units.ToList();
            }
            
           

            base.RefreshData();
        }


        public override void GetData()
        {
            Txt_Code.Text = Product.code;
            Txt_Name.Text = Product.Name;
            Txt_Type.EditValue = Product.type;//Convert.ToByte(Product.type);
            Txt_Category.EditValue = Product.category_id;
            Txt_Details.Text = Product.details;
            Status.IsOn = Product.status;
            switch (Product.image != null)
            {
                case true:
                   
                    Product_pic.Image = ConvertBinaryToImage(Product.image.ToArray());
                    break;
                default:
                    Product_pic.Image = null;
                    break;
            }
            Grid_Units.DataSource = sdb.products_units.Where(x => x.productid == Product.id);
            base.GetData();
        }

        public override void SetData()
        {
            Product.code = Txt_Code.Text;
            Product.Name = Txt_Name.Text;
            Product.type = Convert.ToByte(Txt_Type.EditValue);
            Product.category_id = (int)Txt_Category.EditValue;
            Product.details = Txt_Details.Text;
            Product.status = Status.IsOn;
       
            switch (Product_pic.Image != null)
            {
                case false:
                    Product.image = null;

                    break;
                default:
                    Product.image = ConvertImageToBinary(Product_pic.Image);

                    break;
            }
            
            base.SetData();
        }
       
        private void Frm_Products_Load(object sender, EventArgs e)
        {
            Txt_Category.Properties.DisplayMember = "name";
            Txt_Category.Properties.ValueMember = "id";
            Txt_Category.ProcessNewValue += Txt_Category_ProcessNewValue;
            Txt_Category.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

            Txt_Type.Properties.DataSource = Master.ProductTypesList;
            Txt_Type.Properties.DisplayMember = "name";
            Txt_Type.Properties.ValueMember = "id";


            dgv.OptionsView.ShowGroupPanel = false;
            dgv.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;

            dgv.Columns[nameof(ins.id)].Visible = false;
            dgv.Columns[nameof(ins.productid)].Visible = false;

            Grid_Units.RepositoryItems.Add(calcEdit);
            dgv.Columns[nameof(ins.sellprice)].ColumnEdit = calcEdit;
            dgv.Columns[nameof(ins.buyprice)].ColumnEdit = calcEdit;
            dgv.Columns[nameof(ins.factor)].ColumnEdit = calcEdit;
            dgv.Columns[nameof(ins.selldiscount)].ColumnEdit = calcEdit;

            dgv.Columns[nameof(ins.unitid)].Caption = "إسم الوحدة";
            dgv.Columns[nameof(ins.factor)].Caption = "معالج التحويل";
            dgv.Columns[nameof(ins.buyprice)].Caption = "سعر الشراء";
            dgv.Columns[nameof(ins.sellprice)].Caption = "سعر البيع";
            dgv.Columns[nameof(ins.selldiscount)].Caption = "قيمة التخفيض";
            dgv.Columns[nameof(ins.barcode)].Caption = "الباركود";

            Grid_Units.RepositoryItems.Add(upEdit);
            dgv.Columns[nameof(ins.unitid)].ColumnEdit = upEdit;

            var unit = new units();
            upEdit.ValueMember = nameof(unit.id);
            upEdit.DisplayMember = nameof(unit.name);
            upEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

            upEdit.ProcessNewValue += UpEdit_ProcessNewValue;
            dgv.ValidateRow += Dgv_ValidateRow;
            dgv.InvalidRowException += Dgv_InvalidRowException;
            dgv.FocusedRowChanged += Dgv_FocusedRowChanged;
            dgv.RowCellStyle += Dgv_RowCellStyle;
            dgv.CustomDrawColumnHeader += Dgv_CustomDrawColumnHeader;
            dgv.CustomRowCellEditForEditing += Dgv_CustomRowCellEditForEditing;
        }

        private void Dgv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName==nameof(ins.unitid))
            {
                var ids = ((Collection<products_units>)dgv.DataSource).Select(x => x.unitid).ToList();
                RepositoryItemLookUpEdit repo = new RepositoryItemLookUpEdit();
                using (SalesDataContext db = new SalesDataContext())
                {
                    var CurrentID = (Int32?)e.CellValue;
                    ids.Remove(CurrentID ?? 0);
                    repo.DataSource = db.units.Where(x => ids.Contains(x.id) == false).ToList();
                    repo.ValueMember = nameof(units.id);
                    repo.DisplayMember = nameof(units.name);
                    repo.PopulateColumns();
                    repo.Columns[nameof(units.id)].Visible = false;
                    repo.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                    repo.ProcessNewValue += UpEdit_ProcessNewValue;
                    e.RepositoryItem = repo;
                }
            }
        }

        private void Dgv_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Dgv_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        private void Dgv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
                dgv.Columns[nameof(ins.factor)].OptionsColumn.AllowEdit = !(e.FocusedRowHandle == 0);
        }

        private void Dgv_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void Dgv_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            var row = e.Row as products_units;
            if (row == null)
                return;
            if (row.factor <= 1 && e.RowHandle != 0)
            {
                e.Valid = false;
                dgv.SetColumnError(dgv.Columns[nameof(row.factor)], "لا يمكن إدخال قيمة أصغر من أو تساوي 1");
            }
            if (row.unitid <= 0)
            {
                e.Valid = false;
                dgv.SetColumnError(dgv.Columns[nameof(row.unitid)], ErrorText);
            }
            if (ChekExistBarcode(row.barcode,Prd_id : Product.id) )
            {
                e.Valid = false;
                dgv.SetColumnError(dgv.Columns[nameof(row.barcode)], "تم إستعمال هذا الكود مسبقا لمنتج آخر يرجى التأكد ");
            }
            
        }

        Boolean ChekExistBarcode(string Barcode,int Prd_id) 
        {
            using (SalesDataContext db = new SalesDataContext())
                return db.products_units.Where(x => x.barcode == Barcode && x.productid != Prd_id).Count() > 0;
        }

        private void UpEdit_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (e.DisplayValue is string st && st.Trim() != string.Empty)
            {
                var newObject = new units() { name = st.Trim() };
                using (SalesDataContext db = new SalesDataContext())
                {
                    db.units.InsertOnSubmit(newObject);
                    db.SubmitChanges();
                }
                 ((List<units>)upEdit.DataSource).Add(newObject);
                ((List<units>)(((LookUpEdit)sender).Properties.DataSource)).Add(newObject);
                e.Handled = true;
            }
        }

        private void Txt_Category_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (e.DisplayValue is string st && st.Trim() != string.Empty)
            {
                var newObject = new product_categories() { name = st, parent_id = 0, number = "0" };
                using (SalesDataContext db = new SalesDataContext())
                {
                    db.product_categories.InsertOnSubmit(newObject);
                    db.SubmitChanges();
                }
                ((List<product_categories>)Txt_Category.Properties.DataSource).Add(newObject);
                e.Handled = true;
            }
        }

        #region Validate and Save Data
        bool ValidateData()
        {
            if (Txt_Name.Text.Trim() == string.Empty)
            {
                Txt_Name.ErrorText = ErrorText;
                return false;
            }
            if (Txt_Type.EditValue is int == false)
            {
                Txt_Type.ErrorText = ErrorText;
                return false;
            }
            if (Convert.ToInt32(Txt_Category.EditValue) <= 0)
            {
                Txt_Category.ErrorText = ErrorText;
                return false;
            }

            using (SalesDataContext db = new SalesDataContext())
            {
                if (db.products.Where(x => x.id != Product.id && x.Name.Trim() == Txt_Name.Text.Trim() &&
                                           x.id != Product.id).Count() > 0)
                {
                    Txt_Name.ErrorText = "هذا الاسم موجود مسبقا في قاعدة البيانات";
                    return false;
                }
                if (db.products.Where(x => x.id != Product.id && x.code.Trim() == Txt_Code.Text.Trim() &&
                                           x.id != Product.id).Count() > 0)
                {
                    Txt_Code.ErrorText = "هذا الكود موجود مسبقا في قاعدة البيانات";
                    return false;
                }
            }
            return true;
        }
        public override void Save()
        {
            if (ValidateData() == false)
            {
                return;
            }
            using (SalesDataContext db = new SalesDataContext())
            {
                switch (Product.id)
                {
                    case 0:
                        Action = "Insert";
                        db.products.InsertOnSubmit(Product);
                        break;
                    default:
                        Action = "Update";
                        db.products.Attach(Product);
                        break;
                }
                SetData();
                db.SubmitChanges();
            
                var data = dgv.DataSource as BindingList<products_units>;
                foreach (var item in data)
                {
                    item.productid = Product.id;
                    if (string.IsNullOrEmpty(item.barcode))
                        item.barcode = "";
                }
                sdb.SubmitChanges();
            }
            base.Save();
        }
        #endregion
        
        #region Encoding Barecode and Product Number
        string GetNewProductCode()
        {
            string maxCode;
            using (var db=new SalesDataContext())
            {
                maxCode = db.products.Select(x => x.code).Max();
            }
            return Master.GetNextNumberInString(maxCode);
        }

        string GetNewBarCode()
        {
            string maxCode;
            using (var db = new SalesDataContext())
            {
                maxCode = db.products_units.Select(x => x.barcode).Max();
            }
            return Master.GetNextNumberInString(maxCode);
        }

        #endregion

        #region Load and Convert Image :  

        private void Btn_Pic_Click(object sender, EventArgs e)
        {
            //Read image file
            string fileName;
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "jpeg|*.jpg|bitmap|*.bnp|gif|*.gif|png|*.png"
                ,
                ValidateNames = true,
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofd.FileName;
                    Product_pic.Image = Image.FromFile(fileName);
                }
            }
        }

        byte[] ConvertImageToBinary(Image img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
                byte[] imgByte = ms.ToArray();
                ms.Close();
                return imgByte;
            }
            catch
            {
                return Product.image.ToArray();
            }

        }

        //Convert binary to image
        Image ConvertBinaryToImage(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            Image Pic_Imag = Image.FromStream(stream);
            stream.Close();
            return Pic_Imag;
        }
        #endregion       


    }
}
