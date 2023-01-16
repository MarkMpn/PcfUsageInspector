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
using System.Diagnostics;
using XrmToolBox.Extensibility.Interfaces;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Crm.Sdk.Messages;
using System.Xml;

namespace MarkMpn.PcfUsageInspector
{
    public partial class PcfUsageInspectorPluginControl : PluginControlBase, IGitHubPlugin
    {

        private static readonly Dictionary<string, string> Deprecations = new Dictionary<string, string>
        {
            ["MscrmControls.Calendar.CalendarControl"] = "This control is deprecated in favour of MscrmControls.ActivityCalendarControl.ActivityCalendarControl",
            ["MscrmControls.FlipSwitch.FlipSwitchControl"] = "This control is deprecated in favour of MscrmControls.FieldControls.ToggleControl",
            ["MscrmControls.Slider.LinearSliderControl"] = "This control is deprecated in favour of MscrmControls.Slider.LinearRangeSliderControl",
            ["MscrmControls.Knob.RadialKnobControl"] = "This control is deprecated",
            ["MscrmControls.Knob.ArcKnobControl"] = "This control is deprecated",
            ["MscrmControls.LinearGauge.LinearGaugeControl"] = "This control is deprecated",
            ["MscrmControls.WebsitePreview.PreviewControl"] = "This control is deprecated",
            ["MscrmControls.MultiSelectPicklist.MultiSelectPicklistControl"] = "This control is deprecated",
            ["MscrmControls.AutoComplete.AutoCompleteControl"] = "This control is deprecated",
            ["MscrmControls.InputMask.InputMaskControl"] = "This control is deprecated",
            ["MscrmControls.Multimedia.MultimediaPlayerControl"] = "This control is deprecated",
            ["MscrmControls.NumberInput.NumberInputControl"] = "This control is deprecated",
            ["MscrmControls.OptionSet.OptionSetControl"] = "This control is deprecated",
            ["MscrmControls.Rating.StarRatingControl"] = "This control is deprecated"
        };

        private List<CustomControl> _controls;
        private ExpectedControls _expectedControls;

        string IGitHubPlugin.UserName => "MarkMpn";

        string IGitHubPlugin.RepositoryName => "PcfUsageInspector";

        public PcfUsageInspectorPluginControl()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            if (!SettingsManager.Instance.TryLoad(GetType(), out _expectedControls, "ExpectedControls"))
                _expectedControls = new ExpectedControls();

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
                Message = "Loading Custom Controls...",
                Work = (worker, args) =>
                {
                    var solutions = new Dictionary<Guid, Solution>();
                    var controls = new Dictionary<Guid, CustomControl>();

                    var qry = new QueryExpression("customcontrol")
                    {
                        ColumnSet = new ColumnSet("name"),
                        LinkEntities =
                        {
                            new LinkEntity("customcontrol", "solutioncomponent", "customcontrolid", "objectid", JoinOperator.LeftOuter)
                            {
                                LinkEntities =
                                {
                                    new LinkEntity("solutioncomponent", "solution", "solutionid", "solutionid", JoinOperator.LeftOuter)
                                    {
                                        EntityAlias = "solution",
                                        Columns = new ColumnSet("solutionid", "friendlyname"),
                                        LinkCriteria = new FilterExpression
                                        {
                                            Conditions =
                                            {
                                                new ConditionExpression("isvisible", ConditionOperator.Equal, true)
                                            }
                                        }
                                    }
                                }
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
                                    Name = record.GetAttributeValue<string>("name")
                                };
                                controls[control.Id] = control;
                            }

                            var solutionId = (Guid?) record.GetAttributeValue<AliasedValue>("solution.solutionid")?.Value;
                            if (solutionId != null)
                            {
                                if (!solutions.TryGetValue(solutionId.Value, out var solution))
                                {
                                    solution = new Solution
                                    {
                                        Id = solutionId.Value,
                                        Name = (string)record.GetAttributeValue<AliasedValue>("solution.friendlyname").Value
                                    };
                                    solutions[solution.Id] = solution;
                                }

                                if (!control.Solutions.Contains(solution))
                                {
                                    control.Solutions.Add(solution);
                                    control.Solutions.Sort((x, y) => x.Name.CompareTo(y.Name));
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

                                if (!control.Dependencies.Any(d => d.Id == form.Id))
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

                                if (!control.Dependencies.Any(d => d.Id == view.Id))
                                    control.Dependencies.Add(view);
                            }
                        }

                        if (!results.MoreRecords)
                            break;

                        qry.PageInfo.PageNumber++;
                        qry.PageInfo.PagingCookie = results.PagingCookie;
                    }

                    if (_expectedControls.Rules.Count > 0)
                    {
                        worker.ReportProgress(0, "Loading Metadata...");

                        // Load attribute details and apply rules
                        var metadataQry = new RetrieveMetadataChangesRequest
                        {
                            Query = new EntityQueryExpression
                            {
                                AttributeQuery = new AttributeQueryExpression
                                {
                                    Properties = new MetadataPropertiesExpression
                                    {
                                        PropertyNames =
                                        {
                                            nameof(AttributeMetadata.EntityLogicalName),
                                            nameof(AttributeMetadata.LogicalName),
                                            nameof(AttributeMetadata.MetadataId),
                                            nameof(PicklistAttributeMetadata.OptionSet)
                                        }
                                    },
                                    Criteria = new MetadataFilterExpression
                                    {
                                        FilterOperator = LogicalOperator.Or
                                    }
                                },
                                Properties = new MetadataPropertiesExpression
                                {
                                    PropertyNames =
                                    {
                                        nameof(EntityMetadata.Attributes)
                                    }
                                }
                            }
                        };

                        foreach (var rule in _expectedControls.Rules)
                            rule.AddCriteria(metadataQry);

                        var metadata = (RetrieveMetadataChangesResponse)Service.Execute(metadataQry);

                        foreach (var attribute in metadata.EntityMetadata.SelectMany(e => e.Attributes))
                        {
                            var expectedControlName = _expectedControls.Rules.FirstOrDefault(r => r.IsMatch(attribute))?.ControlName;
                            if (expectedControlName == null)
                                continue;

                            var expectedControl = controls.Values.FirstOrDefault(c => c.Name == expectedControlName);
                            if (expectedControl == null)
                                continue;

                            worker.ReportProgress(0, $"Checking expected control for {attribute.EntityLogicalName}.{attribute.LogicalName}...");

                            var formsQry = new QueryExpression("systemform")
                            {
                                ColumnSet = new ColumnSet("name", "formxml"),
                                LinkEntities =
                                {
                                    new LinkEntity("systemform", "dependency", "formid", "dependentcomponentobjectid", JoinOperator.Inner)
                                    {
                                        LinkCriteria = new FilterExpression
                                        {
                                            Conditions =
                                            {
                                                new ConditionExpression("requiredcomponentobjectid", ConditionOperator.Equal, attribute.MetadataId)
                                            }
                                        }
                                    }
                                }
                            };

                            foreach (var form in Service.RetrieveMultiple(formsQry).Entities)
                            {
                                var xml = new XmlDocument();
                                xml.LoadXml(form.GetAttributeValue<string>("formxml"));

                                foreach (XmlElement control in xml.SelectNodes("//control[@datafieldname='" + attribute.LogicalName + "']"))
                                {
                                    var id = control.GetAttribute("uniqueid");

                                    var controlEnabled = (XmlElement) xml.SelectSingleNode($"//controlDescription[@forControl='{id}']/customControl[@name='{expectedControlName}']");

                                    if (controlEnabled == null)
                                        expectedControl.MissingForms.Add(new Dependency { Type = "Form", Id = form.Id, EntityName = attribute.EntityLogicalName, Name = form.GetAttributeValue<string>("name") });
                                }
                            }
                        }
                    }

                    var solutionList = solutions.Values.ToList();
                    solutionList.Sort();
                    solutionList.Insert(0, new Solution { Id = Guid.Empty, Name = "-- All --" });
                    ExecuteMethod(() => solutionComboBox.DataSource = solutionList);
                    args.Result = controls;
                },
                ProgressChanged = (args) =>
                {
                    SetWorkingMessage(args.UserState.ToString());
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var controls = (Dictionary<Guid, CustomControl>)args.Result;
                    _controls = controls.Values.ToList();
                    var solutions = ((List<Solution>)solutionComboBox.DataSource).ToDictionary(sln => sln.Id);
                    dataGridView.Rows.Clear();

                    foreach (var control in controls.Values.OrderBy(cc => String.Join(", ", cc.Solutions.Select(s => s.Name))).ThenBy(cc => cc.Name))
                    {
                        var rowIndex = dataGridView.Rows.Add();
                        var row = dataGridView.Rows[rowIndex];
                        row.Tag = control;
                        row.Cells[0] = new DataGridViewTextBoxCell { Value = String.Join(", ", control.Solutions.Select(s => s.Name)) };
                        row.Cells[1] = new DataGridViewTextBoxCell { Value = control.Name };
                        row.Cells[2] = new DataGridViewTextBoxCell { Value = String.Join(", ", control.Dependencies.Select(dep => $"{dep.Type} \"{dep.Name}\" on {dep.EntityName}")) };
                        row.Cells[3] = new DataGridViewTextBoxCell { Value = String.Join(", ", control.MissingForms.Select(dep => $"{dep.Type} \"{dep.Name}\" on {dep.EntityName}")) };
                        
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
                        else if (control.MissingForms.Any())
                        {
                            row.DefaultCellStyle.BackColor = Color.Gold;
                        }
                    }

                    FilterItems(this, EventArgs.Empty);

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

            if (detail != null && _expectedControls != null)
                LoadControls();
        }

        private void FilterItems(object sender, EventArgs e)
        {
            var solutionId = (Guid)solutionComboBox.SelectedValue;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var control = (CustomControl)row.Tag;

                if (control.Name.IndexOf(nameTextBox.Text, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    row.Visible = false;
                }
                else if (solutionId != Guid.Empty && !control.Solutions.Any(s => s.Id == solutionId))
                {
                    row.Visible = false;
                }
                else if (Deprecations.TryGetValue(control.Name, out var deprecation))
                {
                    if (control.Dependencies.Any())
                        row.Visible = deprecatedUsedCheckBox.Checked;
                    else
                        row.Visible = deprecatedUnusedCheckBox.Checked;
                }
                else if (control.MissingForms.Any())
                {
                    row.Visible = missingExpectedUsageCheckBox.Checked;
                }
                else
                {
                    row.Visible = normalCheckBox.Checked;
                }
            }
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://markcarrington.dev/");
        }

        private void btnExpectedControls_Click(object sender, EventArgs e)
        {
            using (var form = new ExpectedControlsForm(_controls, _expectedControls.Rules, Service))
            {
                form.ShowDialog(this);
                
                SettingsManager.Instance.Save(GetType(), _expectedControls, "ExpectedControls");
                LoadControls();
            }
        }
    }
}