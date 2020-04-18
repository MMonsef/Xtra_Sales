using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Data_Access_Layer;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Presentation_Layer

{
    public partial class Frm_Invoices : Frm_Master
    {
        #region Block GridView :

        #region : Controls Gridview

        private void GridCellValueChanged(object sender, CellValueChangedEventArgs e)
        {

            var row = Grid.GetRow(e.RowHandle) as invoices_details;
            if (row == null)
            {
                return;
            }
            Session.ProductViewClass item_view = null;
            Session.ProductViewClass.ProductUOMView unit_View = null;
            if (e.Column.FieldName == "Code")
            {
                string itemCode = e.Value.ToString();
                if (Session.GlobalSettings.ReadFromScalBarcode &&
                    itemCode.Length == Session.GlobalSettings.BarcodeLength &&
                    itemCode.ToString().StartsWith(Session.GlobalSettings.ScaleBarcodePrefix))
                {

                    var itemCodeString = itemCode.ToString()
                        .Substring(Session.GlobalSettings.ScaleBarcodePrefix.Length,
                        Session.GlobalSettings.ProductCodeLength);
                    itemCode = Convert.ToInt32(itemCodeString).ToString();
                    string ReadValue = itemCode.ToString().Substring(
                        Session.GlobalSettings.ScaleBarcodePrefix.Length +
                        Session.GlobalSettings.ProductCodeLength);
                    if (Session.GlobalSettings.IgnoreCheckDigit)

                        ReadValue = ReadValue.Remove(ReadValue.Length - 1, 1);
                    double value = Convert.ToDouble(ReadValue);
                    value = value / (Math.Pow(10, Session.GlobalSettings.DivideValueBy));
                    if (Session.GlobalSettings.ReadMode == Session.GlobalSettings.ReadValueMode.Weight)
                        row.item_Qty = value;
                    else if (Session.GlobalSettings.ReadMode == Session.GlobalSettings.ReadValueMode.Price)
                    {
                        item_view = Session.ProductView.FirstOrDefault(X => X.Units.Select(u => u.barcode).Contains(itemCode));
                        if (item_view != null)
                        {
                            unit_View = item_view.Units.First(x => x.barcode == itemCode);
                            switch (Type)
                            {
                                case Master.InvoiceType.Purshases:
                                case Master.InvoiceType.PurchasesReturn:
                                    row.item_Qty = value / unit_View.buyprice;

                                    break;
                                case Master.InvoiceType.Sales:
                                case Master.InvoiceType.SalesReturn:
                                    row.item_Qty = value / unit_View.sellprice;

                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (item_view == null)
                        item_view = Session.ProductView.FirstOrDefault(X => X.Units.Select(u => u.barcode).Contains(itemCode));
                    if (item_view != null)
                    {
                        row.itemid = item_view.id;
                        if (unit_View == null)

                            unit_View = item_view.Units.First(x => x.barcode == itemCode);
                        row.itemunit_id = unit_View.unitid;

                        GridCellValueChanged(sender, new CellValueChangedEventArgs(e.RowHandle, Grid.Columns[nameof(details.itemid)], row.itemid));
                        GridCellValueChanged(sender, new CellValueChangedEventArgs(e.RowHandle, Grid.Columns[nameof(details.itemunit_id)], row.itemunit_id));

                        Barcode = string.Empty;
                        return;
                    }
                    Barcode = string.Empty;
                }
                /*** WAIT FOR UPDATE ***/

                if (row.itemid == 0)
                {
                    return;
                }
                item_view = Session.ProductView.SingleOrDefault(x => x.id == row.itemid);
                if (item_view != null)
                {
                    if (row.itemunit_id == 0)
                    {
                        row.itemunit_id = item_view.Units.First().unitid;
                        GridCellValueChanged(sender, new CellValueChangedEventArgs(e.RowHandle, Grid.Columns[nameof(details.itemunit_id)], row.itemunit_id));
                    }
                    unit_View = item_view.Units.SingleOrDefault(x => x.unitid == row.itemunit_id);
                }



                /*****************************/

                switch (e.Column.FieldName)
                {
                    //TO DO READ FROM BARCODE

                    case nameof(details.itemid):
                        if (row.store_id == 0)
                        {
                            row.store_id = ((int?)Lkp_Branch.EditValue ?? 0);
                        }

                        break;
                    case nameof(details.itemunit_id):
                        if (Type == Master.InvoiceType.Purshases || Type == Master.InvoiceType.PurchasesReturn)
                            row.price = unit_View.buyprice;

                        if (row.item_Qty == 0)

                            row.item_Qty = 1;
                        GridCellValueChanged(sender, new CellValueChangedEventArgs(e.RowHandle, Grid.Columns[nameof(details.price)], row.price));

                        break;

                    case nameof(details.price):
                    case nameof(details.discount):
                    case nameof(details.item_Qty):
                        row.discount_value = row.discount * (row.item_Qty * row.price);
                        GridCellValueChanged(sender, new CellValueChangedEventArgs(e.RowHandle, Grid.Columns[nameof(details.discount_value)], row.discount_value));
                        break;
                    case nameof(details.discount_value):
                        if (Grid.FocusedColumn.FieldName == nameof(details.discount_value))
                            row.discount = row.discount_value / (row.item_Qty * row.price);
                        row.total_price = (row.item_Qty * row.price) - row.discount_value;
                        break;

                    default:
                        break;
                }
            }
        }
        private void Grid_Details_ProcessGridKey(object sender, System.Windows.Forms.KeyEventArgs e)

        {
            GridControl control = sender as GridControl;
            if (control == null) return;

            GridView view = control.FocusedView as GridView;
            if (view == null) return;
            if (view.FocusedColumn == null) return;
            if (e.KeyCode == Keys.Return)
            {
                string FocusedColumn = view.FocusedColumn.FieldName;
                if (view.FocusedColumn.FieldName == "Code" || view.FocusedColumn.FieldName == "itemid")
                {
                    Grid_Details_ProcessGridKey(sender, new KeyEventArgs(Keys.Tab));
                }
                if (view.FocusedRowHandle < 0)
                {
                    view.AddNewRow();
                    view.FocusedColumn = view.Columns["Code"];
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab)
            {
                view.FocusedColumn = view.VisibleColumns[view.FocusedColumn.VisibleIndex - 1];
                e.Handled = true;
            }

            else if (e.KeyCode == Keys.Delete)
            {
                var FocusedRow = view.FocusedRowHandle;

                if (FocusedRow >= 0)
                {
                    if (XtraMessageBox.Show(
                        caption: "تأكيد الحذف",
                        text: "هل تؤكد حذف هذا الصنف من الفاتورة ؟",
                        icon: MessageBoxIcon.Question,
                        buttons: MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        view.DeleteRow(FocusedRow);
                    }
                }
            }
        }
        private void GridCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Code")
            {
                if (e.IsSetData)
                {
                    Barcode = e.Value.ToString();
                }
                else if (e.IsGetData)
                {
                    e.Value = Barcode;
                }
            }
            else if (e.Column.FieldName == "Index") { e.Value = Grid.GetVisibleRowHandle(e.ListSourceRowIndex) + 1; }
        }
        private void GridInvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            if (e.Row == null || (e.Row as invoices_details).itemid == 0)
                e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore;
        }
        private void GridCustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == nameof(invoices_details.itemunit_id))
            {
                RepositoryItemLookUpEdit repo = new RepositoryItemLookUpEdit();
                repo.NullText = "";
                e.RepositoryItem = repo;
                var ins = new Session.ProductViewClass.ProductUOMView();
                var row = Grid.GetRow(e.RowHandle) as invoices_details;
                if (row == null)
                    return;
                var item = Session.ProductView.SingleOrDefault(x => x.id == row.itemid);
                if (item == null)
                    return;

                repo.DataSource = item.Units;
                repo.ValueMember = nameof(ins.unitid);
                repo.DisplayMember = nameof(ins.UnitName);

                repo.ForceInitialize();
                repo.PopulateColumns();

                repo.BestFitMode = BestFitMode.BestFitResizePopup;
                repo.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Standard;
                repo.Columns[nameof(ins.unitid)].Visible = false;
                repo.Columns[nameof(ins.UnitName)].Caption = "إسم الوحدة";
                repo.Columns[nameof(ins.factor)].Caption = "معالج التحويل";
                repo.Columns[nameof(ins.buyprice)].Caption = "سعر الشراء";
                repo.Columns[nameof(ins.sellprice)].Caption = "سعر البيع";
                repo.Columns[nameof(ins.barcode)].Caption = "الباركود";
            }
            else if (e.Column.FieldName == nameof(details.itemid))
            {
                e.RepositoryItem = repoItems;
            }
        }
        private void GridValidateRow(object sender, ValidateRowEventArgs e)
        {
            var row = e.Row as invoices_details;
            if (row == null || row.itemid == 0)
            {
                e.Valid = false;
                return;
            }
        }



        #endregion

        #region : MISE EN FORME

        private void Lkp_Branch_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            var items = Grid.DataSource as Collection<invoices_details>;
            if (e.OldValue is int && e.NewValue is int)
                foreach (var row in items)
                {
                    if (row.store_id.Equals(e.OldValue))
                    {
                        row.store_id = Convert.ToInt32(e.NewValue);
                    }
                }
        }
        private void Sp_Net_DoubleClick(object sender, EventArgs e)
        {
            Sp_Paid.EditValue = Sp_Net.EditValue;
        }
        private void Lkp_PartType_EditValueChanged(object sender, EventArgs e)

        {

            if (Lkp_PartType.IsEditValueOfTypeInt())
            {
                int partType = Convert.ToInt32(Lkp_PartType.EditValue);
                switch (partType)
                {
                    case (int)Master.PartType.Customer:
                        GLkp_PartName.IntializeData(Session.Customers);
                        break;
                    case (int)Master.PartType.Supplier:
                        GLkp_PartName.IntializeData(Session.Suppliers);
                        break;
                }
            }
        }
        private void GLkp_PartName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                using (var Frm = new Frm_CustomersSuppliers(Convert.ToInt32(Lkp_PartType.EditValue) == (int)Master.PartType.Customer))
                {
                    Frm.ShowDialog();
                    RefreshData();
                }
            }
        }
        int CurrentRowCount = 0;
        private void GridRowCountChanged(object sender, EventArgs e)
        {
            if (CurrentRowCount < Grid.RowCount)
            {
                var rows = (Grid.DataSource as Collection<invoices_details>);
                var lastRow = rows.Last();
                var row = rows.FirstOrDefault(x => x.itemid == lastRow.itemid && x.itemid == lastRow.itemid && x != lastRow);
                if (row != null)
                {
                    row.item_Qty += lastRow.item_Qty;
                    rows.Remove(lastRow);
                }

            }
            CurrentRowCount = Grid.RowCount;
            GridRowUpdated(sender, null);
        }
        private void GridRowUpdated(object sender, RowObjectEventArgs e)
        {
            var items = Grid.DataSource as Collection<invoices_details>;
            if (items == null)

                Sp_Total.EditValue = 0;
            else
                Sp_Total.EditValue = items.Sum(x => x.total_price);
        }

        #endregion

        #endregion

        #region BLOCK SPINEDIT :

        #region  : احتساب قيم و نسب الضريبة

        Boolean IsTaxValueFocused;
        private void Sp_InvTaxValue_Enter(object sender, EventArgs e)
        {
            IsTaxValueFocused = true;
        }

        private void Sp_InvTaxValue_Leave(object sender, EventArgs e)
        {
            IsTaxValueFocused = false;
        }

        private void Sp_InvTaxValue_EditValueChanged(object sender, EventArgs e)
        {
            var Total = Convert.ToDouble(Sp_Total.EditValue);
            var TaxValue = Convert.ToDouble(Sp_TaxValue.EditValue);
            var TaxRatio = Convert.ToDouble(Sp_TaxRatio.EditValue);

            if (IsTaxValueFocused)
                Sp_TaxRatio.EditValue = (TaxValue / Total);
            else
                Sp_TaxValue.EditValue = (Total * TaxRatio);
            Spn_EditValueChanged(null, null);
            Sp_InvPaid_EditValueChanged(null, null);
        }

        #endregion

        #region : احتساب قيم ونسب الخصم

        Boolean isDiscountValueFocused;
        private void Sp_InvDiscValue_Enter(object sender, EventArgs e)
        {
            isDiscountValueFocused = true;
        }

        private void Sp_InvDiscValue_Leave(object sender, EventArgs e)
        {
            isDiscountValueFocused = false;
        }

        private void Sp_InvDiscValue_EditValueChanged(object sender, EventArgs e)
        {
            var Total = Convert.ToDouble(Sp_Total.EditValue);
            var DiscountVal = Convert.ToDouble(Sp_DiscValue.EditValue);
            var DiscountRatio = Convert.ToDouble(Sp_DiscRatio.EditValue);

            if (isDiscountValueFocused)
            {
                Sp_DiscRatio.EditValue = (DiscountVal / Total);
            }
            else
            {
                Sp_DiscValue.EditValue = (Total * DiscountRatio);
            }
            Spn_EditValueChanged(null, null);
            Sp_InvPaid_EditValueChanged(null, null);
        }

        #endregion

        #region : احتساب قيمة صافي الفاتورة

        private void Spn_EditValueChanged(object sender, EventArgs e)
        {
            var Total = Convert.ToDouble(Sp_Total.EditValue);
            var Tax = Convert.ToDouble(Sp_TaxValue.EditValue);
            var Discount = Convert.ToDouble(Sp_DiscValue.EditValue);
            var Expences = Convert.ToDouble(Sp_Expences.EditValue);
            Sp_Net.EditValue = (Total + Tax - Discount + Expences);
            Sp_InvPaid_EditValueChanged(null, null);
        }
        private void Sp_Net_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.OldValue.Equals(Sp_Paid.EditValue))
                Sp_Paid.EditValue = e.NewValue;
        }

        #endregion

        #region : احتساب المبلغ المتبقي

        private void Sp_InvPaid_EditValueChanged(object sender, EventArgs e)
        {
            var Net = Convert.ToDouble(Sp_Net.EditValue);
            var Paid = Convert.ToDouble(Sp_Paid.EditValue);
            Sp_Remaing.EditValue = Net - Paid;
        }

        #endregion


        #endregion
       
        #region : التحقق من البيانات

        public override bool IsDataValide()
        {
            int NumberOfErrors = 0;
            if (Grid.RowCount==0)
            {
                NumberOfErrors++;
                XtraMessageBox.Show(
                    text: "لإتمام عملية الحفظ يجب إدخال صنف واحد على الأقل",
                    caption: "تنبيه :",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
            }
            NumberOfErrors += Txt_Code.IsTextValide() ? 0 : 1;
            NumberOfErrors += Lkp_PartType.IsEditValueValide() ? 0 : 1;
            NumberOfErrors += Lkp_Treasurys.IsEditValueValide() ? 0 : 1;
            NumberOfErrors += Lkp_Branch.IsEditValueValide() ? 0 : 1;
            NumberOfErrors += GLkp_PartName.IsEditValueValide() ? 0 : 1;
            NumberOfErrors += Dt_Date.IsDateValide() ? 0 : 1;
            if (Chk_PostedTostore.Checked)
            {
                NumberOfErrors += Dt_PostedDate.IsDateValide() ? 0 : 1;
            }
            return (NumberOfErrors == 0);
        }

        #endregion 

        //*********************

        #region BLOCK DECLAR VARIABLE :

        private string Barcode;
        invoices_header Invoice;
        Master.InvoiceType Type;
        SalesDataContext GenralDB;
        Session.ProductViewClass product = new Session.ProductViewClass();
        invoices_details details = new invoices_details();
        Session.ProductViewClass.ProductUOMView unit = new Session.ProductViewClass.ProductUOMView();
        RepositoryItemLookUpEdit repoItemsAll = new RepositoryItemLookUpEdit();
        RepositoryItemLookUpEdit repoUOM = new RepositoryItemLookUpEdit();
        RepositoryItemLookUpEdit repoStores = new RepositoryItemLookUpEdit();
        RepositoryItemGridLookUpEdit repoItems = new RepositoryItemGridLookUpEdit(); 

        #endregion
        
        #region INVOICE CREATE NEW FORM
        public override void RefreshData()
        {
            Lkp_Branch.IntializeData(Session.Stores);
            Lkp_Treasurys.IntializeData(Session.Treasurys);
            base.RefreshData();
        }
        string GetNewInvoiceCode()
        {
            string maxCode;
            using (var db = new SalesDataContext())
            {
                maxCode = db.invoices_header.Where(x => x.invoice_type == (int)Type).Select(x => x.code).Max();
            }
            return Master.GetNextNumberInString(maxCode);
        }
        public Frm_Invoices(Master.InvoiceType _Type)
        {
            InitializeComponent();
            Lkp_PartType.EditValueChanged += Lkp_PartType_EditValueChanged;
            Load += Frm_Invoices_Load;
            Type = _Type;
            RefreshData();
            New();
        }

        public override void New()
        {
            Invoice = new invoices_header()
            {
                treasurys = Session.Defaults.Drawer,
                date = DateTime.Now,
                posted_tostore = true,
                post_date = DateTime.Now,
                delivery_date = DateTime.Now,
                code = GetNewInvoiceCode(),

            };
            switch (Type)
            {
                case Master.InvoiceType.PurchasesReturn:
                case Master.InvoiceType.Purshases:
                    Invoice.part_type = (int)Master.PartType.Supplier;
                    Invoice.part_id = Session.Defaults.Supplier;
                    Invoice.branch = Session.Defaults.Store;
                    break;
                case Master.InvoiceType.SalesReturn:
                case Master.InvoiceType.Sales:
                    Invoice.part_type = (int)Master.PartType.Customer;
                    Invoice.part_id = Session.Defaults.Customer;
                    Invoice.branch = Session.Defaults.RawStore;

                    break;
                default:
                    throw new NotImplementedException();
            }

            MoveFocusToGrid();
            base.New();
        }
        private void MoveFocusToGrid()
        {
            Grid.Focus();
            Grid.FocusedColumn = Grid.Columns["Code"];
            Grid.AddNewRow();
        }

        #endregion

        #region INVOICE FORM LOAD
        private void Frm_Invoices_Load(object sender, EventArgs e)
        {
            switch (Type)
            {
                case Master.InvoiceType.Purshases:
                    this.LabelName.Text = "فاتورة مشتريات";
                    break;
                case Master.InvoiceType.Sales:
                    this.LabelName.Text = "فاتورة مبيعات";
                    break;
                case Master.InvoiceType.PurchasesReturn:
                    this.LabelName.Text = "فاتورة مرتجع مشتريات";
                    break;
                case Master.InvoiceType.SalesReturn:
                    this.LabelName.Text = "فاتورة مرتجع مبيعات";
                    break;
                default:
                    break;
            }
            //ColorTranslator.FromHtml("#FF99FFCC");//
            Grid.OptionsView.ShowIndicator = false;
            Lkp_PartType.IntializeData(Master.PartTypesList);
            Grid.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
            Grid.Columns[nameof(details.total_price)].OptionsColumn.AllowFocus = false;
            Grid.Columns[nameof(details.id)].Visible = false;
            Grid.Columns[nameof(details.invoiceid)].Visible = false;
            Grid.OptionsView.ShowVerticalLines = DefaultBoolean.False;
            Grid.OptionsView.BestFitMode = GridBestFitMode.Full;

            Grid.Appearance.EvenRow.BackColor = Master.color(220, "#FAFAD2");//Color.FromArgb(220,255, 249, 210);
            Grid.OptionsView.EnableAppearanceEvenRow = true;
            Grid.Appearance.OddRow.BackColor = Master.color(50, "#D2D2FA");//Color.FromArgb(50,220, 220, 220);
            Grid.OptionsView.EnableAppearanceOddRow = true;

            Grid.Columns.Add(new GridColumn()
            {
                Name = "clmCode",
                FieldName = "Code",
                Caption = "باركود",
                UnboundType = UnboundColumnType.String,
            });
            Grid.Columns.Add(new GridColumn()
            {
                Name = "clmIndex",
                FieldName = "Index",
                Caption = "#",
                UnboundType = UnboundColumnType.Integer,
                MaxWidth = 30,
                //Nulltext
            });
            Grid.Columns["Index"].OptionsColumn.AllowFocus = false;
            Grid.Columns[nameof(details.total_price)].OptionsColumn.AllowFocus = false;

            Grid.Columns[nameof(details.itemid)].Caption = "إسم الصنف";
            Grid.Columns[nameof(details.itemunit_id)].Caption = "الوحدة";
            Grid.Columns[nameof(details.item_Qty)].Caption = "الكمية";
            Grid.Columns[nameof(details.price)].Caption = "السعر";
            Grid.Columns[nameof(details.total_price)].Caption = "إجمالي السعر";
            Grid.Columns[nameof(details.cost_value)].Caption = "التكلفة";
            Grid.Columns[nameof(details.totalcoste_value)].Caption = "إجمالي التكلفة";
            Grid.Columns[nameof(details.discount)].Caption = "% الخصم";
            Grid.Columns[nameof(details.discount_value)].Caption = "الخصم";
            Grid.Columns[nameof(details.store_id)].Caption = "المخزن";

            Grid.Columns["Index"].VisibleIndex = 0;
            Grid.Columns["Code"].VisibleIndex = 1;
            Grid.Columns[nameof(details.itemid)].VisibleIndex = 2;
            Grid.Columns[nameof(details.itemunit_id)].VisibleIndex = 3;
            Grid.Columns[nameof(details.item_Qty)].VisibleIndex = 4;
            Grid.Columns[nameof(details.price)].VisibleIndex = 5;
            Grid.Columns[nameof(details.discount)].VisibleIndex = 6;
            Grid.Columns[nameof(details.discount_value)].VisibleIndex = 7;
            Grid.Columns[nameof(details.total_price)].VisibleIndex = 8;
            Grid.Columns[nameof(details.cost_value)].VisibleIndex = 9;
            Grid.Columns[nameof(details.totalcoste_value)].VisibleIndex = 10;
            Grid.Columns[nameof(details.store_id)].VisibleIndex = 11;







            /*Create Grid view items in cells itemid*/


            repoItems.IntializeData(Session.ProductView.Where(x => x.status == true), Grid.Columns[nameof(details.itemid)], Grid_Details);
            repoItems.ValidateOnEnterKey = true;
            repoItems.AllowNullInput = DefaultBoolean.False;
            repoItems.BestFitMode = BestFitMode.BestFitResizePopup;
            repoItems.ImmediatePopup = true;

            repoItemsAll.IntializeData(Session.ProductView, Grid.Columns[nameof(details.itemid)], Grid_Details);
            GridView repoview = repoItems.View;
            repoview.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            repoview.OptionsSelection.UseIndicatorForSelection = true;
            repoview.OptionsView.ShowAutoFilterRow = true;

            repoItems.PopulateViewColumns();

            repoview.Columns[nameof(product.status)].Visible = false;
            repoview.Columns[nameof(product.type)].Visible = false;
            repoview.Columns[nameof(product.id)].Visible = false;
            repoview.Columns[nameof(product.code)].Caption = "الكود ";
            repoview.Columns[nameof(product.name)].Caption = "الإسم ";
            repoview.Columns[nameof(product.category)].Caption = "الفئة ";
            repoview.Columns[nameof(product.details)].Caption = "الوصف ";

            repoUOM.IntializeData(Session.units, Grid.Columns[nameof(details.itemunit_id)], Grid_Details);
            repoStores.IntializeData(Session.Stores, Grid.Columns[nameof(details.store_id)], Grid_Details);                                                                                                   //repoItems.ValueMember = "id";
            repoStores.ForceInitialize();
            repoStores.PopulateColumns();
            repoStores.Columns[0].Visible = false;
            repoview.Columns[1].Caption = "اسم المخزن ";
            repoStores.BestFitMode = BestFitMode.BestFitResizePopup;

            #region : SpinEdit

            RepositoryItemSpinEdit spinEdit = new RepositoryItemSpinEdit();
            Grid.Columns[nameof(details.total_price)].ColumnEdit = spinEdit;
            Grid.Columns[nameof(details.price)].ColumnEdit = spinEdit;
            Grid.Columns[nameof(details.discount_value)].ColumnEdit = spinEdit;
            spinEdit.Mask.EditMask = "N3";
            spinEdit.Mask.UseMaskAsDisplayFormat = true;
            spinEdit.MinValue = Convert.ToDecimal(0.1);
            spinEdit.MaxValue = Convert.ToDecimal(1000000000);
            spinEdit.Increment = 1;

            RepositoryItemSpinEdit spinRatioEdit = new RepositoryItemSpinEdit();
            Grid.Columns[nameof(details.discount)].ColumnEdit = spinRatioEdit;
            spinRatioEdit.Increment = 0.01m;
            spinRatioEdit.Mask.EditMask = "p";
            spinRatioEdit.Mask.UseMaskAsDisplayFormat = true;
            spinRatioEdit.MaxValue = 1;

            RepositoryItemSpinEdit spinspinQteEdit = new RepositoryItemSpinEdit();
            Grid.Columns[nameof(details.item_Qty)].ColumnEdit = spinspinQteEdit;
            spinspinQteEdit.Increment = 0.1m;
            spinspinQteEdit.Mask.EditMask = "n";
            spinRatioEdit.Mask.UseMaskAsDisplayFormat = true;
            spinspinQteEdit.MinValue = Convert.ToDecimal(0.1);
            spinspinQteEdit.MaxValue = Convert.ToDecimal(1000000000);
            spinspinQteEdit.Increment = 1;

            Grid_Details.RepositoryItems.Add(spinspinQteEdit);
            Grid_Details.RepositoryItems.Add(spinRatioEdit);
            Grid_Details.RepositoryItems.Add(spinEdit);

            #endregion

            #region : Events

            this.Activated += Frm_Invoices_Activated;
            GLkp_PartName.ButtonClick += GLkp_PartName_ButtonClick;

            /*احتساب قيم و نسب الضريبة*/
            Sp_DiscValue.Enter += new EventHandler(Sp_InvDiscValue_Enter);
            Sp_DiscValue.Leave += Sp_InvDiscValue_Leave; ;
            Sp_DiscValue.EditValueChanged += Sp_InvDiscValue_EditValueChanged;
            Sp_DiscRatio.EditValueChanged += Sp_InvDiscValue_EditValueChanged;

            Sp_TaxValue.Enter += Sp_InvTaxValue_Enter;
            Sp_TaxValue.Leave += Sp_InvTaxValue_Leave;
            Sp_TaxValue.EditValueChanged += Sp_InvTaxValue_EditValueChanged;
            Sp_TaxRatio.EditValueChanged += Sp_InvTaxValue_EditValueChanged;

            Sp_Total.EditValueChanged += Spn_EditValueChanged;
            Sp_Expences.EditValueChanged += Spn_EditValueChanged;

            Sp_Expences.EditValueChanged += Sp_InvPaid_EditValueChanged;
            Sp_Paid.EditValueChanged += Sp_InvPaid_EditValueChanged;

            Sp_Net.DoubleClick += Sp_Net_DoubleClick;
            Sp_Net.EditValueChanging += Sp_Net_EditValueChanging;
            /* الصنف "ID" جلب الوحدات حسب*/

            Grid.CustomRowCellEditForEditing += GridCustomRowCellEditForEditing;
            /* جلب بيانات وحدة الصنف */
            Grid.CellValueChanged += GridCellValueChanged;
            Grid.RowCountChanged += GridRowCountChanged;
            Grid.RowUpdated += GridRowUpdated;

            Lkp_Branch.EditValueChanging += Lkp_Branch_EditValueChanging;

            Grid.CustomUnboundColumnData += GridCustomUnboundColumnData;
            Grid_Details.ProcessGridKey += Grid_Details_ProcessGridKey;

            Grid.ValidateRow += GridValidateRow;
            Grid.InvalidRowException += GridInvalidRowException;

            #endregion

            MoveFocusToGrid();

        }

        private void Frm_Invoices_Activated(object sender, EventArgs e)
        {
            MoveFocusToGrid();
        }

        #endregion

        #region GET DATA FROM DATABASE
        public override void GetData()
        {
            Txt_Code.EditValue = Invoice.code;
            Dt_Date.EditValue = Invoice.date;
            Lkp_PartType.EditValue = Invoice.part_type;
            GLkp_PartName.EditValue = Invoice.part_id;
            Chk_PostedTostore.Checked = Invoice.posted_tostore;
            Lkp_Branch.EditValue = Invoice.branch;
            Dt_PostedDate.EditValue = Invoice.post_date;
            Mem_Notes.EditValue = Invoice.notes;
            Dt_DeliveryDate.EditValue = Invoice.delivery_date;
            Mem_ShippmentAddress.EditValue = Invoice.shippment_address;
            Lkp_Treasurys.EditValue = Invoice.treasurys;
            Sp_Paid.EditValue = Invoice.paid;
            Sp_Remaing.EditValue = Invoice.remaing;
            Sp_Total.EditValue = Invoice.total;
            Sp_TaxValue.EditValue = Invoice.tax_value;
            Sp_TaxRatio.EditValue = Invoice.tax;
            Sp_Expences.EditValue = Invoice.expences;
            Sp_DiscValue.EditValue = Invoice.discount_value;
            Sp_DiscRatio.EditValue = Invoice.discount_ratio;
            Sp_Net.EditValue = Invoice.net;
            GenralDB = new SalesDataContext();
            Grid_Details.DataSource = GenralDB.invoices_details.Where(x => x.invoiceid == Invoice.id);

            base.GetData();
        }
        #endregion

        #region SET DATA TO SEND TO DATABASE
        public override void SetData()
        {
            Invoice.code = Txt_Code.Text;
            Invoice.date = Dt_Date.DateTime;
            Invoice.part_type = Convert.ToByte(Lkp_PartType.EditValue);
            Invoice.part_id = Convert.ToInt32(GLkp_PartName.EditValue);
            Invoice.posted_tostore = Chk_PostedTostore.Checked;
            Invoice.branch = Convert.ToInt32(Lkp_Branch.EditValue);
            Invoice.post_date = Dt_PostedDate.EditValue as DateTime?;
            Invoice.notes = Mem_Notes.Text;
            Invoice.delivery_date = Dt_DeliveryDate.EditValue as DateTime?;
            Invoice.shippment_address = Mem_ShippmentAddress.Text;
            Invoice.treasurys = Convert.ToInt32(Lkp_Treasurys.EditValue);
            Invoice.paid = Convert.ToDouble(Sp_Paid.EditValue);
            Invoice.remaing = Convert.ToDouble(Sp_Remaing.EditValue);
            Invoice.total = Convert.ToDouble(Sp_Total.EditValue);
            Invoice.tax_value = Convert.ToDouble(Sp_TaxValue.EditValue);
            Invoice.tax = Convert.ToDouble(Sp_TaxRatio.EditValue);
            Invoice.expences = Convert.ToDouble(Sp_Expences.EditValue);
            Invoice.discount_value = Convert.ToDouble(Sp_DiscValue.EditValue);
            Invoice.discount_ratio = Convert.ToDouble(Sp_DiscRatio.EditValue);
            Invoice.net = Convert.ToDouble(Sp_Net.EditValue);
            Invoice.invoice_type = (byte)Type;
            base.SetData();
        }
        #endregion

        public override void Save()
        {
            if (Grid.FocusedRowHandle<0)
                GridValidateRow(Grid, new ValidateRowEventArgs(Grid.FocusedRowHandle, Grid.GetRow(Grid.FocusedRowHandle)));
            using (SalesDataContext db = new SalesDataContext())
            {
                if (Invoice.id==0)
                {
                    db.invoices_header.InsertOnSubmit(Invoice);
                }
                else
                {
                    db.invoices_header.Attach(Invoice);
                }
                SetData();
            }
           
            return;
            
           
            base.Save();    
        }

    }
}
