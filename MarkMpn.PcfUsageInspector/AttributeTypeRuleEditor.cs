using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Metadata;
using System.Reflection;

namespace MarkMpn.PcfUsageInspector
{
    public partial class AttributeTypeRuleEditor : RuleEditorBase, IRuleEditor<AttributeTypeRule>
    {
        public AttributeTypeRuleEditor()
        {
            InitializeComponent();

            foreach (var field in typeof(AttributeTypeDisplayName).GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(AttributeTypeDisplayName)))
            {
                var value = (AttributeTypeDisplayName)field.GetValue(null);

                attributeTypeComboBox.Items.Add(value.Value);
            }
        }

        protected override void OnRuleChanged()
        {
            base.OnRuleChanged();

            var rule = (AttributeTypeRule)Rule;
            attributeTypeComboBox.SelectedItem = rule.AttributeTypeName;
        }

        private void attributeTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            var rule = (AttributeTypeRule)Rule;
            rule.AttributeTypeName = (string)attributeTypeComboBox.SelectedItem;

            OnParameterChanged();
        }
    }
}
