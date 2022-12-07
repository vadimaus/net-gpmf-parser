// See https://aka.ms/new-console-template for more information

using (var fs = new FileStream("FILE_PATH.MP4", FileMode.Open))
{
    Cromatix.MP4Reader.MP4MetadataReader reader = new Cromatix.MP4Reader.MP4MetadataReader(fs);
    reader.ExportToFile("./out.gpx", Cromatix.MP4Reader.ExportFormat.GPX);
}
