namespace AdminPanel
{
    partial class frmCategory
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
            this.lblDelete = new System.Windows.Forms.Button();
            this.lblEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lstProducts = new System.Windows.Forms.ListBox();
            this.lblProducts = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDelete
            // 
            this.lblDelete.Location = new System.Drawing.Point(640, 395);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(133, 23);
            this.lblDelete.TabIndex = 10;
            this.lblDelete.Text = "Delete Product";
            this.lblDelete.UseVisualStyleBackColor = true;
            // 
            // lblEdit
            // 
            this.lblEdit.Location = new System.Drawing.Point(640, 333);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(133, 23);
            this.lblEdit.TabIndex = 9;
            this.lblEdit.Text = "Edit Product";
            this.lblEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(640, 275);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(133, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add New Product";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // lstProducts
            // 
            this.lstProducts.FormattingEnabled = true;
            this.lstProducts.ItemHeight = 16;
            this.lstProducts.Items.AddRange(new object[] {
            "Name                         Type                           Price                " +
                "             Quanity",
            "DVD1                          Used                            $50.00             " +
                "                 4",
            "DVD2                          Used                            $63.00             " +
                "                 2",
            "DVD3                          Used                          $25.00               " +
                "                10"});
            this.lstProducts.Location = new System.Drawing.Point(166, 94);
            this.lstProducts.Name = "lstProducts";
            this.lstProducts.Size = new System.Drawing.Size(468, 324);
            this.lstProducts.TabIndex = 7;
            // 
            // lblProducts
            // 
            this.lblProducts.AutoSize = true;
            this.lblProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducts.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblProducts.Location = new System.Drawing.Point(250, 9);
            this.lblProducts.Name = "lblProducts";
            this.lblProducts.Size = new System.Drawing.Size(277, 55);
            this.lblProducts.TabIndex = 6;
            this.lblProducts.Text = "Product List";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 395);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(133, 23);
            this.btnBack.TabIndex = 11;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // frmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblDelete);
            this.Controls.Add(this.lblEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstProducts);
            this.Controls.Add(this.lblProducts);
            this.Name = "frmCategory";
            this.Text = "Category";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button lblDelete;
        private System.Windows.Forms.Button lblEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lstProducts;
        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.Button btnBack;
    }
}