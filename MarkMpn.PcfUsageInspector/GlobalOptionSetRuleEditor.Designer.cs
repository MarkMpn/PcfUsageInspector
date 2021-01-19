namespace MarkMpn.PcfUsageInspector
{
    partial class GlobalOptionSetRuleEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.globalOptionSetComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "for all picklist attributes using global option set";
            // 
            // globalOptionSetComboBox
            // 
            this.globalOptionSetComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.globalOptionSetComboBox.FormattingEnabled = true;
            this.globalOptionSetComboBox.Location = new System.Drawing.Point(6, 25);
            this.globalOptionSetComboBox.Name = "globalOptionSetComboBox";
            this.globalOptionSetComboBox.Size = new System.Drawing.Size(1088, 21);
            this.globalOptionSetComboBox.TabIndex = 1;
            this.globalOptionSetComboBox.Validated += new System.EventHandler(this.globalOptionSetComboBox_Validated);
            // 
            // GlobalOptionSetRuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.globalOptionSetComboBox);
            this.Controls.Add(this.label1);
            this.Name = "GlobalOptionSetRuleEditor";
            this.Size = new System.Drawing.Size(1097, 607);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox globalOptionSetComboBox;
    }
}
