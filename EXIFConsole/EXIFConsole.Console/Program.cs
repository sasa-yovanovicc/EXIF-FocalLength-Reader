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
            csvExport.WriteCsv(results.focalLength, @"C:\EXIFdata", "focal_length.csv");

            System.Console.WriteLine("Done. Press any key to open web application https://localhost:7273/");
            System.Console.ReadLine();

            // this will launch the default browser
            var ps = new ProcessStartInfo("https://localhost:7273/Report/Index")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }
    }
}