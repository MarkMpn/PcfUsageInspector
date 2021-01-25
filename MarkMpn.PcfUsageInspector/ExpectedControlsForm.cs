using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;

namespace MarkMpn.PcfUsageInspector
{
    partial class ExpectedControlsForm : Form
    {
        private IDictionary<Type, Type> _ruleTypeToEditorType;
        private IOrganizationService _org;
        private List<Rule> _rules;
        private RuleEditorBase _ruleEditor;

        public ExpectedControlsForm(List<CustomControl> controls, List<Rule> rules, IOrganizationService org)
        {
            InitializeComponent();

            _ruleTypeToEditorType = new Dictionary<Type, Type>();
            _rules = rules;
            _org = org;
            controlComboBox.Items.AddRange(controls.ToArray());

            foreach (var type in GetType().Assembly.GetTypes().Where(t => t.BaseType == typeof(Rule)))
            {
                var desc = (DescriptionAttribute)type.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
                var menuItem = addContextMenuStrip.Items.Add(desc.Description);
                menuItem.Tag = type;
                menuItem.Click += AddRule;

                var editorType = GetType().Assembly.GetTypes().Where(t => typeof(Control).IsAssignableFrom(t) && t.GetInterfaces().Contains(typeof(IRuleEditor<>).MakeGenericType(type))).Single();
                _ruleTypeToEditorType[type] = editorType;
            }

            foreach (var rule in _rules)
            {
                var listItem = ruleListView.Items.Add(rule.ControlName);
                listItem.SubItems.Add(rule.GetParameterDescription());
                listItem.Tag = rule;
            }
        }

        private void AddRule(object sender, EventArgs e)
        {
            var menuItem = (ToolStripItem)sender;
            var type = (Type)menuItem.Tag;
            var rule = Activator.CreateInstance(type);

            var item = ruleListView.Items.Add("");
            item.SubItems.Add("");
            item.Tag = rule;

            item.Selected = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addContextMenuStrip.Show((Control)sender, new Point(0, addButton.Height), ToolStripDropDownDirection.Default);
        }

        private void ruleListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            deleteButton.Enabled = ruleListView.SelectedItems.Count == 1;
            controlComboBox.Enabled = ruleListView.SelectedItems.Count == 1;

            if (ruleListView.SelectedItems.Count != 1)
            {
                if (_ruleEditor != null)
                {
                    _ruleEditor.Parent.Controls.Remove(_ruleEditor);
                    _ruleEditor.Dispose();
                    _ruleEditor = null;
                }

                return;
            }

            var rule = (Rule)ruleListView.SelectedItems[0].Tag;
            controlComboBox.SelectedItem = controlComboBox.Items.OfType<CustomControl>().SingleOrDefault(cc => cc.Name == rule.ControlName);
            var editorType = _ruleTypeToEditorType[rule.GetType()];
            _ruleEditor = (RuleEditorBase) Activator.CreateInstance(editorType);
            _ruleEditor.Service = _org;
            _ruleEditor.Rule = rule;
            _ruleEditor.ParameterChanged += (s, a) => ruleListView.SelectedItems[0].SubItems[1].Text = rule.GetParameterDescription();
            splitContainer1.Panel2.Controls.Add(_ruleEditor);
            splitContainer1.Panel2.Controls.SetChildIndex(_ruleEditor, 0);
        }

        private void controlComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = (CustomControl)controlComboBox.SelectedItem;

            if (control == null)
                return;

            var rule = (Rule)ruleListView.SelectedItems[0].Tag;
            rule.ControlName = control.Name;
            ruleListView.SelectedItems[0].Text = control.Name;

            if (!_rules.Contains(rule))
                _rules.Add(rule);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var rule = (Rule)ruleListView.SelectedItems[0].Tag;
            _rules.Remove(rule);
            ruleListView.Items.Remove(ruleListView.SelectedItems[0]);
        }
    }
}
