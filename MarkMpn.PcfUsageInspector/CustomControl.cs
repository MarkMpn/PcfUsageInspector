using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace MarkMpn.PcfUsageInspector
{
    class CustomControl
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Solution> Solutions { get; } = new List<Solution>();
        public List<Dependency> Dependencies { get; } = new List<Dependency>();
        public List<Dependency> MissingForms { get; } = new List<Dependency>();
    }
}