// See https://aka.ms/new-console-template for more information

using Cromatix.MP4Reader;

using (var fs = new FileStream("G:\\GoNoob\\GOPRO11\\GX0046-01_5K.mp4", FileMode.Open))
{
    MP4MetadataReader reader = new MP4MetadataReader(fs);
    reader.ProcessGPMFTelemetry();
    reader.ExportToFile("./out.gpx", ExportFormat.GPX);
}
