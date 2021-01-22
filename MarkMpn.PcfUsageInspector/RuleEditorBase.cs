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
    public partial class RuleEditorBase : UserControl
    {
        private Rule _rule;

        public RuleEditorBase()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public Rule Rule
        {
            get { return _rule; }
            set
            {
                _rule = value;
                OnRuleChanged();
            }
        }

        protected virtual void OnRuleChanged()
        {
        }

        public event EventHandler ParameterChanged;

        protected virtual void OnParameterChanged()
        {
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
