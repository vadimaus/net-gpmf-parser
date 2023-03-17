using System;
using System.Text;
using System.Xml;

namespace Cromatix.MP4Reader
{
    public static class Export
    {
        public static bool ExportToFile(this MP4MetadataReader reader, string filePath, ExportFormat format)
        {
            if (reader.Telemetry.KLVs == null || reader.Telemetry.KLVs.Count == 0)
                return false;

            try
            {
                switch (format)
                {
                    case ExportFormat.GPX:
                        {
                            try
                            {
                                string gpx = Export.ToGPX(reader.Telemetry);
                                File.WriteAllText(filePath, gpx);
                                return true;
                            }
                            catch (Exception e)
                            {
                                throw new Exception("Error exporting to GPX", e);
                            }
                        }
                    default:
                        break;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

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
                            {TelemetryToGPXString(telemetry)}
                        </trkseg>
                    </trk>
                </gpx>";

            xmlDoc.LoadXml(gpx);
            return xmlDoc.Ident();
        }

        private static string TelemetryToGPXString(Telemetry telemetry)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var klv in telemetry.KLVs)
            {
                sb.AppendLine(@$"<trkpt lat=""{klv.Lat}"" lon=""{klv.Lon}"">
                                    <ele>{klv.Alt}</ele>
                                    <time>{klv.Time.Value.ToString("yyyy-MM-ddTHH:mm:ss:fffZ")}</time>
                                    <fix>{klv.GPSFix}</fix>
                                    <hdop>{klv.HDOP}</hdop>
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

            return sb.ToString().Replace("utf-16", "utf-8");
        }
    }
}
