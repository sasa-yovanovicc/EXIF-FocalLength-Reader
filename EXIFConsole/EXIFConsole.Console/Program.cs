using EXIFConsole.Explorer;
using EXIFConsole.Helpers;
using EXIFConsole.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EXIFConsole.Console
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            string dir = @"C:\EXIFdata";
            string csvFile = "focal_length.csv";
            string url = "https://localhost:7273/Report/Index";

            System.Console.WriteLine("--------- EXIF Reader ---------");
            ISelect select = new Select();
            select.InitialFolder = "C:\\";
            select.ShowDialog();

            System.Console.WriteLine($"Folder Selected: {select.Folder}");
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadLine();

            System.Console.Write("Processing images... ");

            (List<MetadataModel> metadata, List<FocalLengthModel> focalLength) results = new ExtractFocalLength().ExctractFocalLength(select.Folder);

            CsvExport csvExport = new();
            csvExport.WriteCsv(results.focalLength, dir, csvFile);

            System.Console.WriteLine($"Done. Press any key to open web application {url}");
            System.Console.ReadLine();

            // this will launch the default browser
            var ps = new ProcessStartInfo(url)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }
    }
}