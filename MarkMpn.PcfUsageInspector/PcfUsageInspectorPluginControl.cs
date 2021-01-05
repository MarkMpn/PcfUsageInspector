using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;

namespace MarkMpn.PcfUsageInspector
{
    public partial class PcfUsageInspectorPluginControl : PluginControlBase
    {
        class CustomControl
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Guid? SolutionId { get; set; }
            public List<Dependency> Dependencies { get; } = new List<Dependency>();
        }

        class Dependency
        {
            public Guid Id { get; set; }
            public string Type { get; set; }
            public string EntityName { get; set; }
            public string Name { get; set; }
        }

        class Solution : IComparable
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public int CompareTo(object obj)
            {
                return Name.CompareTo(((Solution)obj).Name);
            }
        }

        private static readonly Dictionary<string, string> Deprecations = new Dictionary<string, string>
        {
            ["MscrmControls.Calendar.CalendarControl"] = "This control is deprecated in favour of MscrmControls.ActivityCalendarControl.ActivityCalendarControl",
            ["MscrmControls.FlipSwitch.FlipSwitchControl"] = "This control is deprecated in favour of MscrmControls.FieldControls.ToggleControl",
            ["MscrmControls.Slider.LinearSliderControl"] = "This control is deprecated in favour of MscrmControls.Slider.LinearRangeSliderControl",
            ["MscrmControls.Knob.RadialKnobControl"] = "This control is deprecated",
            ["MscrmControls.Knob.ArcKnobControl"] = "This control is deprecated",
            ["MscrmControls.LinearGauge.LinearGaugeControl"] = "This control is deprecated",
            ["MscrmControls.WebsitePreview.PreviewControl"] = "This control is deprecated",
            ["MscrmControls.MultiSelectPicklist.MultiSelectPicklistControl"] = "This control is deprecated"
        };

        public PcfUsageInspectorPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            if (ConnectionDetail != null)
                LoadControls();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void LoadControls()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading PCF Usage",
                Work = (worker, args) =>
                {
                    var solutions = new Dictionary<Guid, Solution>();
                    var controls = new Dictionary<Guid, CustomControl>();

                    var qry = new QueryExpression("customcontrol")
                    {
                        ColumnSet = new ColumnSet("name", "solutionid"),
                        LinkEntities =
                        {
                            new LinkEntity("customcontrol", "solution", "solutionid", "solutionid", JoinOperator.LeftOuter)
                            {
                                EntityAlias = "solution",
                                Columns = new ColumnSet("friendlyname")
                            },
                            new LinkEntity("customcontrol", "dependency", "customcontrolid", "requiredcomponentobjectid", JoinOperator.LeftOuter)
                            {
                                EntityAlias = "dependency",
                                Columns = new ColumnSet("requiredcomponentobjectid"),
                                LinkEntities =
                                {
                                    new LinkEntity("dependency", "systemform", "dependentcomponentobjectid", "formid", JoinOperator.LeftOuter)
                                    {
                                        EntityAlias = "form",
                                        Columns = new ColumnSet("objecttypecode", "name")
                                    },
                                    new LinkEntity("dependency", "savedquery", "dependentcomponentobjectid", "savedqueryid", JoinOperator.LeftOuter)
                                    {
                                        EntityAlias = "view",
                                        Columns = new ColumnSet("returnedtypecode", "name")
                                    }
                                }
                            }
                        },
                        PageInfo = new PagingInfo
                        {
                            Count = 500,
                            PageNumber = 1
                        }
                    };

                    while (true)
                    {
                        var results = Service.RetrieveMultiple(qry);

                        foreach (var record in results.Entities)
                        {
                            if (!controls.TryGetValue(record.Id, out var control))
                            {
                                control = new CustomControl
                                {
                                    Id = record.Id,
                                    Name = record.GetAttributeValue<string>("name"),
                                    SolutionId = record.GetAttributeValue<Guid?>("solutionid")
                                };
                                controls[control.Id] = control;

                                if (control.SolutionId != null && !solutions.ContainsKey(control.SolutionId.Value))
                                {
                                    var solution = new Solution
                                    {
                                        Id = control.SolutionId.Value,
                                        Name = (string)record.GetAttributeValue<AliasedValue>("solution.friendlyname").Value
                                    };
                                    solutions[solution.Id] = solution;
                                }
                            }

                            if (record.Contains("form.name"))
                            {
                                var form = new Dependency
                                {
                                    Id = (Guid)record.GetAttributeValue<AliasedValue>("dependency.requiredcomponentobjectid").Value,
                                    Type = "Form",
                                    EntityName = (string)record.GetAttributeValue<AliasedValue>("form.objecttypecode").Value,
                                    Name = (string)record.GetAttributeValue<AliasedValue>("form.name").Value
                                };
                                control.Dependencies.Add(form);
                            }

                            if (record.Contains("view.name"))
                            {
                                var view = new Dependency
                                {
                                    Id = (Guid)record.GetAttributeValue<AliasedValue>("dependency.requiredcomponentobjectid").Value,
                                    Type = "View",
                                    EntityName = (string)record.GetAttributeValue<AliasedValue>("view.returnedtypecode").Value,
                                    Name = (string)record.GetAttributeValue<AliasedValue>("view.name").Value
                                };
                                control.Dependencies.Add(view);
                            }
                        }

                        if (!results.MoreRecords)
                            break;

                        qry.PageInfo.PageNumber++;
                        qry.PageInfo.PagingCookie = results.PagingCookie;
                    }

                    var solutionList = solutions.Values.ToList();
                    solutionList.Sort();
                    solutionList.Insert(0, new Solution { Id = Guid.Empty, Name = "-- All --" });
                    ExecuteMethod(() => solutionComboBox.DataSource = solutionList);
                    args.Result = controls;
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var controls = (Dictionary<Guid, CustomControl>)args.Result;
                    var solutions = ((List<Solution>)solutionComboBox.DataSource).ToDictionary(sln => sln.Id);
                    dataGridView.Rows.Clear();

                    foreach (var control in controls.Values.OrderBy(cc => cc.SolutionId == null ? "" : solutions[cc.SolutionId.Value].Name).ThenBy(cc => cc.Name))
                    {
                        var rowIndex = dataGridView.Rows.Add();
                        var row = dataGridView.Rows[rowIndex];
                        row.Cells[0] = new DataGridViewTextBoxCell { Value = control.SolutionId == null ? "" : solutions[control.SolutionId.Value].Name, Tag = control.SolutionId };
                        row.Cells[1] = new DataGridViewTextBoxCell { Value = control.Name };
                        row.Cells[2] = new DataGridViewTextBoxCell { Value = String.Join(", ", control.Dependencies.Select(dep => $"{dep.Type} \"{dep.Name}\" on {dep.EntityName}")) };

                        if (Deprecations.TryGetValue(control.Name, out var deprecation))
                        {
                            if (control.Dependencies.Any())
                            {
                                row.DefaultCellStyle.BackColor = Color.Tomato;
                                ((DataGridViewTextBoxCell)row.Cells[2]).Value = "⚠ " + ((DataGridViewTextBoxCell)row.Cells[2]).Value;
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = Color.Silver;
                            }

                            row.Cells[2].ToolTipText = deprecation;
                        }
                    }

                    dataGridView.AutoResizeColumns();
                }
            });
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (detail != null)
                LoadControls();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            FilterItems();
        }

        private void solutionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            FilterItems();
        }

        private void FilterItems()
        {
            var solutionId = (Guid)solutionComboBox.SelectedValue;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var name = (string)((DataGridViewTextBoxCell)row.Cells[1]).Value;

                if (name.IndexOf(nameTextBox.Text, StringComparison.OrdinalIgnoreCase) == -1)
                    row.Visible = false;
                else if (solutionId != Guid.Empty && (Guid?) row.Cells[0].Tag != solutionId)
                    row.Visible = false;
                else
                    row.Visible = true;
            }
        }
    }
}