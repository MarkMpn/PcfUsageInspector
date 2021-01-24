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
using Microsoft.Xrm.Sdk.Metadata.Query;
using Microsoft.Xrm.Sdk.Metadata;

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

            var entities = (RetrieveMetadataChangesResponse)Service.Execute(new RetrieveMetadataChangesRequest
            {
                Query = new EntityQueryExpression
                {
                    Properties = new MetadataPropertiesExpression
                    {
                        PropertyNames = { nameof(EntityMetadata.LogicalName) }
                    }
                }
            });

            entityNameComboBox.Items.AddRange(entities.EntityMetadata.Select(e => e.LogicalName).OrderBy(name => name).ToArray());

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

        private void entityNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            attributeNameComboBox.Items.Clear();

            if (entityNameComboBox.SelectedIndex == -1)
                return;

            var entities = (RetrieveMetadataChangesResponse)Service.Execute(new RetrieveMetadataChangesRequest
            {
                Query = new EntityQueryExpression
                {
                    Criteria = new MetadataFilterExpression
                    {
                        Conditions =
                        {
                            new MetadataConditionExpression(nameof(EntityMetadata.LogicalName), MetadataConditionOperator.Equals, entityNameComboBox.Text)
                        }
                    },
                    Properties = new MetadataPropertiesExpression
                    {
                        PropertyNames = { nameof(EntityMetadata.Attributes) }
                    },
                    AttributeQuery = new AttributeQueryExpression
                    {
                        Properties = new MetadataPropertiesExpression
                        {
                            PropertyNames = { nameof(AttributeMetadata.LogicalName) }
                        }
                    }
                }
            });

            attributeNameComboBox.Items.AddRange(entities.EntityMetadata[0].Attributes.Select(attr => attr.LogicalName).OrderBy(name => name).ToArray());
        }
    }
}
