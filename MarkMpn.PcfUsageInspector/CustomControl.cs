using System;
using System.Collections.Generic;

namespace MarkMpn.PcfUsageInspector
{
    class CustomControl
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Solution> Solutions { get; } = new List<Solution>();
        public List<Dependency> Dependencies { get; } = new List<Dependency>();
    }
}