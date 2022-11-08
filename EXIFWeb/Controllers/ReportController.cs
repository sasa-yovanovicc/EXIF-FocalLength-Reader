﻿using CsvHelper;
using CsvHelper.Configuration;
using EXIFWeb.Models;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EXIFWeb.Controllers
{
    /// <summary>
    /// Report class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ReportController : Controller
    {
        /// <summary>
        /// Report home page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// EXIF data Table.
        /// </summary>
        /// <returns></returns>
        public IActionResult Table()
        {
            var focalLengthData = GetCsvFocalLengthData();

            return View("Table", focalLengthData);
        }

        /// <summary>
        /// EXIF data Distribution of Focal lengths.
        /// </summary>
        /// <returns></returns>
        public IActionResult Distribution()
        {
            var focalLengthData = GetCsvFocalLengthData();
            char[] charsToTrim = { ' ', 'm' };
            var sample = focalLengthData.Select(x => Convert.ToDouble(x.FocalLength?.TrimEnd(charsToTrim))).ToArray();
            var data = Frequency(sample);

            return View("Distribution", data);
        }


        /// <summary>
        /// Chart Controller.
        /// </summary>
        /// <returns></returns>
        public IActionResult Chart()
        {
            var focalLengthData = GetCsvFocalLengthData();

            char[] charsToTrim = { ' ', 'm' };

            var sample = focalLengthData.Select(x => Convert.ToDouble(x.FocalLength?.TrimEnd(charsToTrim))).ToArray();

            var ci = ConfidenceInterval(sample, 0.95);

            ViewData["Data"] = String.Join(",", sample);
            ViewData["lowerBound"] = ci.lowerBound;
            ViewData["upperBound"] = ci.upperBound;

            string freqString = "";
            foreach (var item in Frequency(sample))
            {
                freqString += "{name: " + item.Value + ", y: " + item.Frequency + ", },";
            }
            ViewData["frequency"] = freqString;

            return View("Chart");
        }

        /// <summary>
        /// Gets the CSV focal length data out from file.
        /// </summary>
        /// <returns></returns>
        private List<FocalLengthModel> GetCsvFocalLengthData()
        {
            List<FocalLengthModel> focalLengthData = new();

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            };

            using (var reader = new StreamReader(@"C:\EXIFdata\focal_length.csv"))
            using (var csv = new CsvReader(reader, configuration))
            {
                var records = csv.GetRecords<FocalLengthModel>();
                focalLengthData = records.Skip(1).ToList();
            }
            return focalLengthData;
        }

        /// <summary>
        /// Calculate confidences the interval.
        /// </summary>
        /// <param name="samples">The samples.</param>
        /// <param name="interval">The interval.</param>
        /// <returns></returns>
        public static (double lowerBound, double upperBound) ConfidenceInterval(double[] samples, double interval)
        {
            double theta = (interval + 1.0) / 2;
            double T = FindRoots.OfFunction(x => StudentT.CDF(0, 1, samples.Length - 1, x) - theta, -800, 800);

            double mean = samples.Mean();
            double sd = samples.StandardDeviation();
            double t = T * (sd / Math.Sqrt(samples.Length));
            return (mean - t, mean + t);
        }

        public static List<FrequencyModel> Frequency(double[] samples)
        {
            var count = samples.Count();
            var result = (from s in samples
                          group s by s into g
                          select new FrequencyModel { Value = g.Key.ToString(), Frequency = (double)g.Count() / count * 100, }).ToList();

            return result;
        }
    }
}