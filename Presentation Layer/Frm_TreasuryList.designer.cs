namespace Presentation_Layer
{
    partial class Frm_TreasuryList
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
            this.Grid_Treasury = new DevExpress.XtraGrid.GridControl();
            this.dgv = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Treasury)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid_Treasury
            // 
            this.Grid_Treasury.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Treasury.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grid_Treasury.Location = new System.Drawing.Point(6, 38);
            this.Grid_Treasury.MainView = this.dgv;
            this.Grid_Treasury.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grid_Treasury.Name = "Grid_Treasury";
            this.Grid_Treasury.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Grid_Treasury.Size = new System.Drawing.Size(386, 372);
            this.Grid_Treasury.TabIndex = 4;
            this.Grid_Treasury.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgv});
            // 
            // dgv
            // 
            this.dgv.DetailHeight = 294;
            this.dgv.GridControl = this.Grid_Treasury;
            this.dgv.Name = "dgv";
            // 
            // Frm_TreasuryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 418);
            this.Controls.Add(this.Grid_Treasury);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(400, 450);
            this.Name = "Frm_TreasuryList";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Text = "قائمة الخزائن";
            this.Load += new System.EventHandler(this.Frm_TreasuryList_Load);
            this.Controls.SetChildIndex(this.Grid_Treasury, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Treasury)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl Grid_Treasury;
        private DevExpress.XtraGrid.Views.Grid.GridView dgv;
    }
}