namespace Presentation_Layer
{
    partial class Frm_StoresList
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
            this.Grd_Stores = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.Grd_Stores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Grd_Stores
            // 
            this.Grd_Stores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grd_Stores.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grd_Stores.Location = new System.Drawing.Point(6, 8);
            this.Grd_Stores.MainView = this.gridView1;
            this.Grd_Stores.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grd_Stores.Name = "Grd_Stores";
            this.Grd_Stores.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Grd_Stores.Size = new System.Drawing.Size(286, 302);
            this.Grd_Stores.TabIndex = 0;
            this.Grd_Stores.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 294;
            this.gridView1.GridControl = this.Grd_Stores;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // Frm_StoresList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 318);
            this.Controls.Add(this.Grd_Stores);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(330, 450);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 100);
            this.Name = "Frm_StoresList";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "قائمة الفروع والمخازن";
            this.Load += new System.EventHandler(this.Frm_StoresList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grd_Stores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl Grd_Stores;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}