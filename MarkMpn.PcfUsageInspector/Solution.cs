using System;

namespace MarkMpn.PcfUsageInspector
{
    class Solution : IComparable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int CompareTo(object obj)
        {
            return Name.CompareTo(((Solution)obj).Name);
        }
    }
}