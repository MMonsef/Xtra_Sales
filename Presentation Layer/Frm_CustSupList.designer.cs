namespace Presentation_Layer
{
    partial class Frm_CustSupList
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
            this.Grid_CustSup = new DevExpress.XtraGrid.GridControl();
            this.dgv = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_CustSup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid_CustSup
            // 
            this.Grid_CustSup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_CustSup.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grid_CustSup.Location = new System.Drawing.Point(6, 38);
            this.Grid_CustSup.MainView = this.dgv;
            this.Grid_CustSup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Grid_CustSup.Name = "Grid_CustSup";
            this.Grid_CustSup.Size = new System.Drawing.Size(486, 522);
            this.Grid_CustSup.TabIndex = 4;
            this.Grid_CustSup.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgv});
            // 
            // dgv
            // 
            this.dgv.DetailHeight = 294;
            this.dgv.GridControl = this.Grid_CustSup;
            this.dgv.Name = "dgv";
            // 
            // Frm_CustSupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 568);
            this.Controls.Add(this.Grid_CustSup);
            this.IconOptions.ShowIcon = false;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(500, 600);
            this.Name = "Frm_CustSupList";
            this.Padding = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Text = "Frm_CustSupList";
            this.Load += new System.EventHandler(this.Frm_CustSupList_Load);
            this.Controls.SetChildIndex(this.Grid_CustSup, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_CustSup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl Grid_CustSup;
        private DevExpress.XtraGrid.Views.Grid.GridView dgv;
    }
}