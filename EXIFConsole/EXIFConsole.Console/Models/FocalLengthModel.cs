using CsvHelper.Configuration.Attributes;

namespace EXIFConsole.Models
{
    /// <summary>
    /// FocalLengthModel.
    /// </summary>
    public class FocalLengthModel
    {
        public string FileName { set; get; }
        public string FocalLength { set; get; }

        [Optional]
        public string FocalLength35mm { set; get; }
    }
}