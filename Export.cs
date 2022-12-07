using System;
using System.Text;
using System.Xml;

namespace Cromatix.MP4Reader
{
    public static class Export
    {
        /// <summary>
        /// GPS Exchange format
        /// </summary>
        public static string ToGPX(Telemetry telemetry)
        {
            XmlDocument xmlDoc = new XmlDocument();
            
            string gpx = @"<?xml version=""1.0"" encoding=""UTF-8""?>" + 
                @$"<gpx xmlns=""http://www.topografix.com/GPX/1/1"" version=""1.0"">
                    <trk>
                        <trkseg>
                            {TelemetryToString(telemetry)}
                        </trkseg>
                    </trk>
                </gpx>";

            xmlDoc.LoadXml(gpx);
            return xmlDoc.Ident();
        }

        private static string TelemetryToString(Telemetry telemetry)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var klv in telemetry.KLVs)
            {
                sb.AppendLine(@$"<trkpt lat=""{klv.Lat}"" lon=""{klv.Lon}"">
                                    <ele>{klv.Alt}</ele>
                                    <time>{klv.Time.Value.ToString("yyyy-MM-ddTHH:mm:ss:fffZ")}</time>
                                    <fix>{klv.GPSFix}</fix>
                                    <hdop>{klv.HDOP}</hdop>
                                    <cmt>altitude system: MSLV; 2dSpeed: {klv.GroundSpeed}; 3dSpeed: {klv.VirtualSpeed}</cmt>
                                 </trkpt>");
            }

            return sb.ToString();
        }

        private static string Ident(this XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }

            return sb.ToString();
        }
    }
}
