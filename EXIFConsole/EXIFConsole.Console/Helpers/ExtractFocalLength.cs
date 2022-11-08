using EXIFConsole.Models;
using MetadataExtractor;
using System.Collections.Generic;
using System.Linq;
using Directory = MetadataExtractor.Directory;

namespace EXIFConsole.Helpers
{
    /// <summary>
    /// Extract Focal Length data from images.
    /// </summary>
    public class ExtractFocalLength
    {
        /// <summary>
        /// Exctracts the Focal length.
        /// </summary>
        /// <param name="selectedFolder">The selected folder.</param>
        /// <returns></returns>
        public (List<MetadataModel>, List<FocalLengthModel>) ExctractFocalLength(string selectedFolder)
        {
            List<MetadataModel> metadata = new();
            List<FocalLengthModel> focalLength = new();
            using (var progress = new ProgressBar())
            {
                var numFiles = System.IO.Directory.GetFiles(selectedFolder).Length;
                System.Console.WriteLine(numFiles);
                if (numFiles > 0)
                {
                    int i = 0;
                    foreach (string file in System.IO.Directory.GetFiles(selectedFolder))
                    {
                        progress.Report((double)++i / numFiles);

                        //FileType fileType = FileTypeDetector.DetectFileType
                        IEnumerable<Directory> directories;
                        try
                        {
                            directories = ImageMetadataReader.ReadMetadata(file);
                        }
                        catch
                        {
                            continue;
                        }


                        foreach (var directory in directories)
                        {
                            foreach (var tag in directory.Tags)
                            {
                                metadata.Add(new MetadataModel { FileName = file, DirectoryName = tag.DirectoryName, Name = tag.Name, Description = tag.Description });
                                if (tag.Name == "Focal Length")
                                {
                                    if (focalLength.Any(x => x.FileName == file))
                                    {
                                        focalLength.First(x => x.FileName == file).FocalLength = tag.Description;
                                    }
                                    else
                                    {
                                        focalLength.Add(new FocalLengthModel { FileName = file, FocalLength = tag.Description });
                                    }
                                }
                                else if (tag.Name == "Focal Length 35")
                                {
                                    if (focalLength.Any(x => x.FileName == file))
                                    {
                                        focalLength.First(x => x.FileName == file).FocalLength35mm = tag.Description;
                                    }
                                    else
                                    {
                                        focalLength.Add(new FocalLengthModel { FileName = file, FocalLength35mm = tag.Description });
                                    }
                                }
                            }

                            if (directory.HasError)
                            {
                                foreach (var error in directory.Errors)
                                    System.Console.WriteLine($"ERROR: {error}");
                            }
                        }
                    }
                    return (metadata, focalLength);
                }
                else
                {
                    return (null, null);
                }

            }
        }
    }
}