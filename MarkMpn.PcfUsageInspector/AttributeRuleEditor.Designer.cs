﻿namespace MarkMpn.PcfUsageInspector
{
    partial class AttributeRuleEditor
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
            this.entityNameComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.attributeNameComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Entity Name";
            // 
            // entityNameComboBox
            // 
            this.entityNameComboBox.FormattingEnabled = true;
            this.entityNameComboBox.Location = new System.Drawing.Point(3, 16);
            this.entityNameComboBox.Name = "entityNameComboBox";
            this.entityNameComboBox.Size = new System.Drawing.Size(1116, 21);
            this.entityNameComboBox.TabIndex = 1;
            this.entityNameComboBox.Validated += new System.EventHandler(this.entityNameComboBox_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Attribute Name";
            // 
            // attributeNameComboBox
            // 
            this.attributeNameComboBox.FormattingEnabled = true;
            this.attributeNameComboBox.Location = new System.Drawing.Point(3, 67);
            this.attributeNameComboBox.Name = "attributeNameComboBox";
            this.attributeNameComboBox.Size = new System.Drawing.Size(1116, 21);
            this.attributeNameComboBox.TabIndex = 3;
            this.attributeNameComboBox.Validated += new System.EventHandler(this.attributeNameComboBox_Validated);
            // 
            // AttributeRuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.attributeNameComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.entityNameComboBox);
            this.Controls.Add(this.label1);
            this.Name = "AttributeRuleEditor";
            this.Size = new System.Drawing.Size(1122, 607);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox entityNameComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox attributeNameComboBox;
    }
}