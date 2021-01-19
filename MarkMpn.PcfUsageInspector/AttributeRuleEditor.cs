using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkMpn.PcfUsageInspector
{
    public partial class AttributeRuleEditor : RuleEditorBase, IRuleEditor<AttributeRule>
    {
        public AttributeRuleEditor()
        {
            InitializeComponent();
        }

        protected override void OnRuleChanged()
        {
            base.OnRuleChanged();

            var rule = (AttributeRule)Rule;
            entityNameComboBox.Text = rule.EntityName;
            attributeNameComboBox.Text = rule.AttributeName;
        }

        private void entityNameComboBox_Validated(object sender, EventArgs e)
        {
            var rule = (AttributeRule)Rule;
            rule.EntityName = entityNameComboBox.Text;

            OnParameterChanged();
        }

        private void attributeNameComboBox_Validated(object sender, EventArgs e)
        {
            var rule = (AttributeRule)Rule;
            rule.AttributeName = attributeNameComboBox.Text;

            OnParameterChanged();
        }
    }
}
