namespace MarkMpn.PcfUsageInspector
{
    partial class PcfUsageInspectorPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExpectedControls = new System.Windows.Forms.Button();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.solutionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.solutionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.controlNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usagesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.missingUsagesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.solutionColumn,
            this.controlNameColumn,
            this.usagesColumn,
            this.missingUsagesColumn});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 86);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(743, 214);
            this.dataGridView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExpectedControls);
            this.panel1.Controls.Add(this.linkLabel);
            this.panel1.Controls.Add(this.nameTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.solutionComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(743, 86);
            this.panel1.TabIndex = 1;
            // 
            // btnExpectedControls
            // 
            this.btnExpectedControls.Location = new System.Drawing.Point(91, 56);
            this.btnExpectedControls.Name = "btnExpectedControls";
            this.btnExpectedControls.Size = new System.Drawing.Size(132, 23);
            this.btnExpectedControls.TabIndex = 2;
            this.btnExpectedControls.Text = "Edit Expected Controls";
            this.btnExpectedControls.UseVisualStyleBackColor = true;
            this.btnExpectedControls.Click += new System.EventHandler(this.btnExpectedControls_Click);
            // 
            // linkLabel
            // 
            this.linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel.Image = global::MarkMpn.PcfUsageInspector.Properties.Resources.PcfUsageInspector16x16;
            this.linkLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel.Location = new System.Drawing.Point(522, 6);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(218, 13);
            this.linkLabel.TabIndex = 2;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "PCF Usage Inspector by Mark Carrington";
            this.linkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(91, 30);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(278, 20);
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search by name";
            // 
            // solutionComboBox
            // 
            this.solutionComboBox.DisplayMember = "Name";
            this.solutionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.solutionComboBox.FormattingEnabled = true;
            this.solutionComboBox.Location = new System.Drawing.Point(91, 3);
            this.solutionComboBox.Name = "solutionComboBox";
            this.solutionComboBox.Size = new System.Drawing.Size(278, 21);
            this.solutionComboBox.TabIndex = 1;
            this.solutionComboBox.ValueMember = "Id";
            this.solutionComboBox.SelectedValueChanged += new System.EventHandler(this.solutionComboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter by solution";
            // 
            // solutionColumn
            // 
            this.solutionColumn.HeaderText = "Solution";
            this.solutionColumn.Name = "solutionColumn";
            this.solutionColumn.ReadOnly = true;
            // 
            // controlNameColumn
            // 
            this.controlNameColumn.HeaderText = "Control Name";
            this.controlNameColumn.Name = "controlNameColumn";
            this.controlNameColumn.ReadOnly = true;
            // 
            // usagesColumn
            // 
            this.usagesColumn.HeaderText = "Usages";
            this.usagesColumn.Name = "usagesColumn";
            this.usagesColumn.ReadOnly = true;
            // 
            // missingUsagesColumn
            // 
            this.missingUsagesColumn.HeaderText = "Missing Expected Usages";
            this.missingUsagesColumn.Name = "missingUsagesColumn";
            this.missingUsagesColumn.ReadOnly = true;
            // 
            // PcfUsageInspectorPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Name = "PcfUsageInspectorPluginControl";
            this.Size = new System.Drawing.Size(743, 300);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox solutionComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Button btnExpectedControls;
        private System.Windows.Forms.DataGridViewTextBoxColumn solutionColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn controlNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usagesColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn missingUsagesColumn;
    }
}
