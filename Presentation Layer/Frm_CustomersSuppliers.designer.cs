namespace Presentation_Layer
{
    partial class Frm_CustomersSuppliers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Txt_Name = new DevExpress.XtraEditors.TextEdit();
            this.Txt_Address = new DevExpress.XtraEditors.TextEdit();
            this.Txt_Mobile = new DevExpress.XtraEditors.TextEdit();
            this.Txt_Email = new DevExpress.XtraEditors.TextEdit();
            this.Txt_Account = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.Lc_name = new DevExpress.XtraLayout.LayoutControlItem();
            this.Lc_address = new DevExpress.XtraLayout.LayoutControlItem();
            this.Lc_phone = new DevExpress.XtraLayout.LayoutControlItem();
            this.Lc_mail = new DevExpress.XtraLayout.LayoutControlItem();
            this.Lc_account = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Address.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Mobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Email.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Account.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_phone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_mail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_account)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Txt_Name);
            this.layoutControl1.Controls.Add(this.Txt_Address);
            this.layoutControl1.Controls.Add(this.Txt_Mobile);
            this.layoutControl1.Controls.Add(this.Txt_Email);
            this.layoutControl1.Controls.Add(this.Txt_Account);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(6, 38);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(386, 182);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Txt_Name
            // 
            this.Txt_Name.Location = new System.Drawing.Point(12, 12);
            this.Txt_Name.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Txt_Name.Name = "Txt_Name";
            this.Txt_Name.Size = new System.Drawing.Size(283, 28);
            this.Txt_Name.StyleController = this.layoutControl1;
            this.Txt_Name.TabIndex = 4;
            // 
            // Txt_Address
            // 
            this.Txt_Address.Location = new System.Drawing.Point(12, 44);
            this.Txt_Address.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Txt_Address.Name = "Txt_Address";
            this.Txt_Address.Size = new System.Drawing.Size(283, 28);
            this.Txt_Address.StyleController = this.layoutControl1;
            this.Txt_Address.TabIndex = 5;
            // 
            // Txt_Mobile
            // 
            this.Txt_Mobile.Location = new System.Drawing.Point(12, 76);
            this.Txt_Mobile.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Txt_Mobile.Name = "Txt_Mobile";
            this.Txt_Mobile.Size = new System.Drawing.Size(283, 28);
            this.Txt_Mobile.StyleController = this.layoutControl1;
            this.Txt_Mobile.TabIndex = 6;
            // 
            // Txt_Email
            // 
            this.Txt_Email.Location = new System.Drawing.Point(12, 108);
            this.Txt_Email.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Txt_Email.Name = "Txt_Email";
            this.Txt_Email.Size = new System.Drawing.Size(283, 28);
            this.Txt_Email.StyleController = this.layoutControl1;
            this.Txt_Email.TabIndex = 7;
            // 
            // Txt_Account
            // 
            this.Txt_Account.Location = new System.Drawing.Point(12, 140);
            this.Txt_Account.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Txt_Account.Name = "Txt_Account";
            this.Txt_Account.Size = new System.Drawing.Size(283, 28);
            this.Txt_Account.StyleController = this.layoutControl1;
            this.Txt_Account.TabIndex = 8;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.Lc_name,
            this.Lc_address,
            this.Lc_phone,
            this.Lc_mail,
            this.Lc_account});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(386, 182);
            this.Root.TextVisible = false;
            // 
            // Lc_name
            // 
            this.Lc_name.Control = this.Txt_Name;
            this.Lc_name.Location = new System.Drawing.Point(0, 0);
            this.Lc_name.Name = "Lc_name";
            this.Lc_name.Size = new System.Drawing.Size(366, 32);
            this.Lc_name.Tag = "اسم";
            this.Lc_name.Text = "الاسم";
            this.Lc_name.TextSize = new System.Drawing.Size(76, 21);
            // 
            // Lc_address
            // 
            this.Lc_address.Control = this.Txt_Address;
            this.Lc_address.Location = new System.Drawing.Point(0, 32);
            this.Lc_address.Name = "Lc_address";
            this.Lc_address.Size = new System.Drawing.Size(366, 32);
            this.Lc_address.Tag = "عنوان";
            this.Lc_address.Text = "العنوان";
            this.Lc_address.TextSize = new System.Drawing.Size(76, 21);
            // 
            // Lc_phone
            // 
            this.Lc_phone.Control = this.Txt_Mobile;
            this.Lc_phone.Location = new System.Drawing.Point(0, 64);
            this.Lc_phone.Name = "Lc_phone";
            this.Lc_phone.Size = new System.Drawing.Size(366, 32);
            this.Lc_phone.Tag = "رقم هاتف";
            this.Lc_phone.Text = "رقم الهاتف";
            this.Lc_phone.TextSize = new System.Drawing.Size(76, 21);
            // 
            // Lc_mail
            // 
            this.Lc_mail.Control = this.Txt_Email;
            this.Lc_mail.Location = new System.Drawing.Point(0, 96);
            this.Lc_mail.Name = "Lc_mail";
            this.Lc_mail.Size = new System.Drawing.Size(366, 32);
            this.Lc_mail.Text = "البريد الالكتروني";
            this.Lc_mail.TextSize = new System.Drawing.Size(76, 21);
            // 
            // Lc_account
            // 
            this.Lc_account.Control = this.Txt_Account;
            this.Lc_account.Location = new System.Drawing.Point(0, 128);
            this.Lc_account.Name = "Lc_account";
            this.Lc_account.Size = new System.Drawing.Size(366, 34);
            this.Lc_account.Tag = "رقم حساب";
            this.Lc_account.Text = "رقم الحساب";
            this.Lc_account.TextSize = new System.Drawing.Size(76, 21);
            // 
            // Frm_CustomersSuppliers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 228);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(400, 260);
            this.Name = "Frm_CustomersSuppliers";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Load += new System.EventHandler(this.Frm_CustomersSuppliers_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Address.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Mobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Email.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Account.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_phone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_mail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Lc_account)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit Txt_Name;
        private DevExpress.XtraEditors.TextEdit Txt_Address;
        private DevExpress.XtraEditors.TextEdit Txt_Mobile;
        private DevExpress.XtraEditors.TextEdit Txt_Email;
        private DevExpress.XtraEditors.TextEdit Txt_Account;
        private DevExpress.XtraLayout.LayoutControlItem Lc_name;
        private DevExpress.XtraLayout.LayoutControlItem Lc_address;
        private DevExpress.XtraLayout.LayoutControlItem Lc_phone;
        private DevExpress.XtraLayout.LayoutControlItem Lc_mail;
        private DevExpress.XtraLayout.LayoutControlItem Lc_account;
    }
}