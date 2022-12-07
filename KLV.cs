using System;

namespace Cromatix.MP4Reader
{
    /*
     GPMF -- GoPro Metadata Format or General Purpose Metadata Format -- is a modified Key, Length, Value solution, with a 32-bit aligned payload
     */
    public class KLV
    {
        public DateTime? Time { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Alt { get; set; }
        public int HDOP { get; set; }
        public string GPSFix { get; set; }
        public double VirtualSpeed { get; set; }
        public double GroundSpeed { get; set; }
    }
}
