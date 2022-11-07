# EXIF FocalLength Reader

The solution consists of two applications

1) EXIFConsole written in .NetCore 3.1 using WPF (Microsoft.NET.Sdk.WindowsDesktop) to enable folder selection from the console.

2) EXIFWeb written in showing results and graph. This app is written .Net 6.0, because in the first approach I tried to process images in browser app, but of course Web app can be in .NetCore too. But .NET 6.0 is much faster and reduce the amount of code you need to write, and also can run on Windows and Mac, The .NET 6 release includes support for macOS Arm64 (or "Apple Silicon") and Windows Arm64 operating systems, for both native Arm64 execution and x64 emulation. 

Folder selection is not possible from the browser due to browser security restrictions, so I decided on this approach (Window.showDirectoryPicker() is still an experimental method of the File System Access API and only works with Chrome, Edge and Opera browsers).

The project is set as Multiple startup project. EXIFWeb starts without starting the browser, then EXITConsole. The user selects a folder, the data processing starts and finally a CSV file is saved to disk.

Finally, the console application calls the URL https://localhost:7273/Report/Index which opens the default browser and displays the web application.
There are two options on this page: Table and Chart.

Clicking on Table shows the data in a table, clicking on Chart shows the Density plot with a confidence interval of 95%.

I also read two EXIF data: Focal length and 35mm Focal Length (equivalent to 35mm), which some cameras record in EXIF. Chart displays data for Focal Length only.

The Excel file records the values as written in EXIF, as a string in the format "xx.xx mm" (eg 18.00 mm), solely for display. When preparing the data for the Chart, "mm" is deleted and the string is converted to a double.

In my work, I focused on the flexibility of the solution and this MVP can certainly be better in terms of code structure, but that was not the focus this time (eg the location and name of the csv file is hardcoded in the console application and not in the environment variable or configuration file).

**NOTE**
After cloning repository, double click in Visual Studio on EXIFWeb solution, exit Visual Studio and start it again to get properly Multiple Startup project option.
