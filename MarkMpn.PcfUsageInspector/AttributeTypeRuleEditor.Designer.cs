﻿namespace MarkMpn.PcfUsageInspector
{
    partial class AttributeTypeRuleEditor
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
            this.attributeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Attribute Type:";
            // 
            // attributeTypeComboBox
            // 
            this.attributeTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.attributeTypeComboBox.FormattingEnabled = true;
            this.attributeTypeComboBox.Location = new System.Drawing.Point(6, 16);
            this.attributeTypeComboBox.Name = "attributeTypeComboBox";
            this.attributeTypeComboBox.Size = new System.Drawing.Size(1113, 21);
            this.attributeTypeComboBox.TabIndex = 1;
            this.attributeTypeComboBox.SelectedValueChanged += new System.EventHandler(this.attributeTypeComboBox_SelectedValueChanged);
            // 
            // AttributeTypeRuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.attributeTypeComboBox);
            this.Controls.Add(this.label1);
            this.Name = "AttributeTypeRuleEditor";
            this.Size = new System.Drawing.Size(1122, 607);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox attributeTypeComboBox;
    }
}
