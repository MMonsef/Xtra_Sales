using System;
using System.Data;
using System.Linq;

namespace Presentation_Layer
{
    public partial class Frm_ProductsCategory : Frm_Master
    {
        product_categories categoy;
        public Frm_ProductsCategory()
        {
            InitializeComponent();
            New();
        }
        public override void New()
        {
            categoy = new product_categories();
            base.New();
        }
        public override void GetData()
        {
            Txt_Name.Text = categoy.name;
            LUp_Maingrp.EditValue = categoy.parent_id;

            base.GetData();
        }
        public override void SetData()
        {
            categoy.name = Txt_Name.Text;
            categoy.parent_id = (LUp_Maingrp.EditValue as int?) ?? 0;
            categoy.number = "0";
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
            if (db.product_categories.Where(x => x.name.Trim() == Txt_Name.Text.Trim() &&
                 x.id != categoy.id).Count() > 0)
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
                if (categoy.id == 0)
                {
                    Action = "Insert";
                    db.product_categories.InsertOnSubmit(categoy);
                }
                else
                {
                    Action = "Update";
                    db.product_categories.Attach(categoy);
                }
                SetData();
                db.SubmitChanges();
                Txt_Name.Text = string.Empty;
                Txt_Name.Focus();
                base.Save();
            }
        }
        public override void RefreshData()
        {
            var db = new SalesDataContext();
            var groups = db.product_categories;
            LUp_Maingrp.Properties.DataSource = groups;
            T_ListCategory.DataSource = groups;
            T_ListCategory.ExpandAll();
            base.RefreshData();
        }
        private void Frm_ProductsCategory_Load(object sender, EventArgs e)
        {
            RefreshData();
            LUp_Maingrp.Properties.DisplayMember = nameof(categoy.name);
            LUp_Maingrp.Properties.ValueMember = nameof(categoy.id);
            T_ListCategory.ParentFieldName = nameof(categoy.parent_id);
            T_ListCategory.KeyFieldName = nameof(categoy.id);
            T_ListCategory.OptionsBehavior.Editable = false;
            T_ListCategory.Columns["number"].Visible = false;
            T_ListCategory.Columns["name"].Caption = "الإسم";
            T_ListCategory.FocusedNodeChanged += T_ListCategory_FocusedNodeChanged;

        }
        private void T_ListCategory_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            int id = 0;
            if (int.TryParse(e.Node.GetValue(nameof(categoy.id)).ToString(), out id))
            {
                var db = new SalesDataContext();
                categoy = db.product_categories.Single(x => x.id == id);
                GetData();
            }
        }
    }
}
