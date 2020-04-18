namespace Presentation_Layer
{
    partial class Frm_ProductsCategory
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
            this.T_ListCategory = new DevExpress.XtraTreeList.TreeList();
            this.LUp_Maingrp = new DevExpress.XtraEditors.LookUpEdit();
            this.Txt_Name = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.T_ListCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUp_Maingrp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.T_ListCategory);
            this.layoutControl1.Controls.Add(this.LUp_Maingrp);
            this.layoutControl1.Controls.Add(this.Txt_Name);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(6, 38);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(436, 522);
            this.layoutControl1.TabIndex = 4;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // T_ListCategory
            // 
            this.T_ListCategory.HorzScrollStep = 2;
            this.T_ListCategory.Location = new System.Drawing.Point(10, 74);
            this.T_ListCategory.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.T_ListCategory.MinWidth = 16;
            this.T_ListCategory.Name = "T_ListCategory";
            this.T_ListCategory.Size = new System.Drawing.Size(416, 438);
            this.T_ListCategory.TabIndex = 6;
            this.T_ListCategory.TreeLevelWidth = 14;
            // 
            // LUp_Maingrp
            // 
            this.LUp_Maingrp.Location = new System.Drawing.Point(10, 42);
            this.LUp_Maingrp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LUp_Maingrp.Name = "LUp_Maingrp";
            this.LUp_Maingrp.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LUp_Maingrp.Properties.NullText = "";
            this.LUp_Maingrp.Size = new System.Drawing.Size(294, 28);
            this.LUp_Maingrp.StyleController = this.layoutControl1;
            this.LUp_Maingrp.TabIndex = 5;
            // 
            // Txt_Name
            // 
            this.Txt_Name.Location = new System.Drawing.Point(10, 10);
            this.Txt_Name.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Txt_Name.Name = "Txt_Name";
            this.Txt_Name.Size = new System.Drawing.Size(294, 28);
            this.Txt_Name.StyleController = this.layoutControl1;
            this.Txt_Name.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(436, 522);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.Txt_Name;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(420, 32);
            this.layoutControlItem1.Text = "  اسم فئة الصنف";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(120, 21);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.LUp_Maingrp;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(420, 32);
            this.layoutControlItem2.Text = "  اسم المجموعة الرئيسية";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(120, 21);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.T_ListCategory;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(420, 442);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Frm_ProductsCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 568);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(450, 600);
            this.Name = "Frm_ProductsCategory";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "شاشة ادارة فئة الصنف";
            this.Load += new System.EventHandler(this.Frm_ProductsCategory_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.T_ListCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LUp_Maingrp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Txt_Name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraTreeList.TreeList T_ListCategory;
        private DevExpress.XtraEditors.LookUpEdit LUp_Maingrp;
        private DevExpress.XtraEditors.TextEdit Txt_Name;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}