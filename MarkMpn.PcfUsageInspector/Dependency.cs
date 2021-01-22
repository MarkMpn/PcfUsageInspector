using System;

namespace MarkMpn.PcfUsageInspector
{
    class Dependency
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string EntityName { get; set; }
        public string Name { get; set; }
    }
}