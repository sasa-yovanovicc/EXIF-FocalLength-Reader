using CsvHelper;
using EXIFConsole.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace EXIFConsole.Helpers
{
    /// <summary>
    /// Csv Export class.
    /// </summary>
    public class CsvExport
    {
        /// <summary>
        /// Writes the CSV.
        /// </summary>
        /// <param name="focalLength">Length of the focal.</param>
        /// <param name="dir">The dir.</param>
        /// <param name="csvFileName">Name of the CSV file.</param>
        public void WriteCsv(List<FocalLengthModel> focalLength, string dir, string csvFileName)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string csvFilePath = dir + @"\" + csvFileName;
            using var writer = new StreamWriter(csvFilePath);
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(focalLength);
            }
        }
    }
}