using System;

namespace Cromatix.MP4Reader
{
    internal class GPS5
    {
        /// <summary>
        /// Latitude
        /// </summary>
        public double Lat;
        /// <summary>
        /// Longitude
        /// </summary>
        public double Lon;
        /// <summary>
        /// Altirude
        /// </summary>
        public double Alt;
        /// <summary>
        ///  2D speed
        /// </summary>
        public double Speed2d;
        /// <summary>
        /// 3D speed
        /// </summary>
        public double Speed3d;
        /// <summary>
        /// DOP
        /// </summary>
        public double DOP;
        /// <summary>
        /// Fix (0, 2D or 3D)
        /// </summary>
        public short Fix;
    }
}
