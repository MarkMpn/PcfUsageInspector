using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xrm.Sdk.Metadata;

namespace MarkMpn.PcfUsageInspector
{
    public class ExpectedControls
    {
        public List<Rule> Rules { get; } = new List<Rule>();
    }

    [XmlInclude(typeof(AttributeRule))]
    [XmlInclude(typeof(AttributeTypeRule))]
    [XmlInclude(typeof(GlobalOptionSetRule))]
    public abstract class Rule
    {
        public string ControlName { get; set; }

        public abstract bool IsMatch(AttributeMetadata attribute);

        public abstract string GetParameterDescription();
    }

    [Description("Single Attribute")]
    public class AttributeRule : Rule
    {
        public string EntityName { get; set; }

        public string AttributeName { get; set; }

        public override bool IsMatch(AttributeMetadata attribute)
        {
            return attribute.EntityLogicalName == EntityName && attribute.LogicalName == AttributeName;
        }

        public override string GetParameterDescription() => $"Attribute {EntityName}.{AttributeName}";
    }

    [Description("Attribute Type")]
    public class AttributeTypeRule : Rule
    {
        public string AttributeTypeName { get; set; }

        public override bool IsMatch(AttributeMetadata attribute)
        {
            return attribute.AttributeTypeName?.Value == AttributeTypeName;
        }

        public override string GetParameterDescription() => $"All {AttributeTypeName} Attributes";
    }

    [Description("Global Option Set")]
    public class GlobalOptionSetRule : Rule
    {
        public string OptionSetName { get; set; }

        public override bool IsMatch(AttributeMetadata attribute)
        {
            return attribute is PicklistAttributeMetadata picklist && picklist.OptionSet.IsGlobal == true && picklist.OptionSet.Name == OptionSetName;
        }

        public override string GetParameterDescription() => $"All {OptionSetName} Picklist Attributes";
    }
}
