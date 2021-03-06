﻿using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MarkMpn.PcfUsageInspector
{
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "PCF Usage Inspector"),
        ExportMetadata("Description", "Find where PCF controls are being used and identify deprecated controls"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAALESURBVFhH7ZVdaJJRGMdfPzZ1cyo6m+jmQJ0sP8s5mK6FEYvmx22Bu6vJ7jZYdLcLhaBLLypFCHbpTRd1EbQiYjG60W5DGkUJuovFwByaELz9jzsb2cqp043AHzycc/7vq/7Pc57zyHQ5azh0PICHUO9PT4bT6fyRTqd36LJhRhBsOwIGXmI8lj8zQAxkEWuITSI0S29vbx+Px3tgNptfIQPXqNwwBxm45fF4+P39/aOYNxVKpdI2MDDAWiyWtweaRqM5h7EhfjcwCgM1aW01YOAFxr/yryO4DQOvU6nUF4VCkc5msw+rT1tjFQY+5XK563RdlyMZmJqaelJ90jrv6mWAS8czo2ug4wYmJycXHQ7HY7o8QscMoIj7gsHgDbVabbbZbJlEIsGnj+py4lswODjINRgM92QyWRnLah/g8/ms3W5/HwqFDNWX6lA1gE62PTQ09Lmnp6dpA/jxKIfDYdE/Plqt1rt+v38F3fEZ0fC963NzcwL66lHGx8c1iAIJo9H4nThvxgCydgGf+anX6zcjkYiUyszS0hJPpVKtEhMwEKZyfVo5AvT/KDHtcrkuUemQhYUFObJSEolEz6lU5dgi3NrasmGI1guxWHwfI5PP58m/ITM/P0+WNXi93r1KpfK1XCal0QDIwAgysIvpsYG/31w8Hle43e47XC6XnZiYuAm9huXl5YtSqZTFrXhKpfYTCARkQqFwB8fwAcdwnspkM2KdTrdBihq3YZbK7cdkMk0LBIICpiyOpTQ2NraBc1/DldwmmUEhPgqHww31g6bB2c9ghwVkoIijWBkeHk5BKyJKWq32G5rRIhpTxxrfDKKAa1bELq/sSwyTTCaVsVhMS5cdYxpB0r6HuEqEUwOFdRm7LqDoiggPlU8H3HWVRCIpyOXyIjrnYdqbpeWiQLFVUOHrPp9vNpPJvKFyl/8NhvkFiNbp139+jxoAAAAASUVORK5CYII="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "DarkMagenta"),
        ExportMetadata("PrimaryFontColor", "White"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class PcfUsageInspectorPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new PcfUsageInspectorPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public PcfUsageInspectorPlugin()
        {
        }
    }
}