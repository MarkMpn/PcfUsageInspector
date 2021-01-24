using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk.Messages;

namespace MarkMpn.PcfUsageInspector
{
    public partial class GlobalOptionSetRuleEditor : RuleEditorBase, IRuleEditor<GlobalOptionSetRule>
    {
        public GlobalOptionSetRuleEditor()
        {
            InitializeComponent();
        }

        protected override void OnRuleChanged()
        {
            base.OnRuleChanged();

            var optionsets = (RetrieveAllOptionSetsResponse) Service.Execute(new RetrieveAllOptionSetsRequest());
            globalOptionSetComboBox.Items.AddRange(optionsets.OptionSetMetadata.Select(os => os.Name).OrderBy(name => name).ToArray());

            var rule = (GlobalOptionSetRule)Rule;
            globalOptionSetComboBox.Text = rule.OptionSetName;
        }

        private void globalOptionSetComboBox_Validated(object sender, EventArgs e)
        {
            var rule = (GlobalOptionSetRule)Rule;
            rule.OptionSetName = globalOptionSetComboBox.Text;

            OnParameterChanged();
        }
    }
}
