namespace Presentation_Layer
{
    partial class Frm_ProductsList
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
            this.Grid_Products = new DevExpress.XtraGrid.GridControl();
            this.dgv = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Products)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid_Products
            // 
            this.Grid_Products.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Products.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grid_Products.Location = new System.Drawing.Point(6, 38);
            this.Grid_Products.MainView = this.dgv;
            this.Grid_Products.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grid_Products.Name = "Grid_Products";
            this.Grid_Products.Size = new System.Drawing.Size(686, 522);
            this.Grid_Products.TabIndex = 4;
            this.Grid_Products.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgv});
            // 
            // dgv
            // 
            this.dgv.DetailHeight = 294;
            this.dgv.GridControl = this.Grid_Products;
            this.dgv.Name = "dgv";
            // 
            // Frm_ProductsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 568);
            this.Controls.Add(this.Grid_Products);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Frm_ProductsList";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Text = "شاشة ادارة الاصناف";
            this.Load += new System.EventHandler(this.Frm_ProductsList_Load);
            this.Controls.SetChildIndex(this.Grid_Products, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Products)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl Grid_Products;
        private DevExpress.XtraGrid.Views.Grid.GridView dgv;
    }
}