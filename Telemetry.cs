using System;
using System.Collections.Generic;

namespace Cromatix.MP4Reader
{
    public class Telemetry
    {
        public string FileName { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public List<KLV> KLVs { get; set; }
    }
}
