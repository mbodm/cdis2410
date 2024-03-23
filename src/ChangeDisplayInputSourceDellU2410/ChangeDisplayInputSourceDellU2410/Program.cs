using ChangeDisplayInputSourceDellU2410;

Console.WriteLine();
Console.WriteLine(Helper.AppTitle);
Console.WriteLine();
Console.WriteLine(Helper.ExeDescription);

var hWindow = WinApi.GetDesktopWindow();
var hMonitor = WinApi.MonitorFromWindow(hWindow, WinApi.MONITOR_DEFAULTTOPRIMARY);

if (!WinApi.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out uint numberOfPhysicalMonitors))
    ShowErrorAndExit("Could not determine number of physical monitors.", 1);

if (numberOfPhysicalMonitors <= 0)
    ShowErrorAndExit("Could not found any physical monitors.", 2);

var physicalMonitors = new WinApi.PHYSICAL_MONITOR[numberOfPhysicalMonitors];

if (!WinApi.GetPhysicalMonitorsFromHMONITOR(hMonitor, numberOfPhysicalMonitors, physicalMonitors))
    ShowErrorAndExit("Could not determine primary physical monitor.", 3);

var hPhysicalMonitor = physicalMonitors[0].hPhysicalMonitor;
var szPhysicalMonitorDescription = physicalMonitors[0].szPhysicalMonitorDescription;

Console.WriteLine();
Console.WriteLine($"Display description: {szPhysicalMonitorDescription}");

if (!WinApi.GetVCPFeatureAndVCPFeatureReply(hPhysicalMonitor, 0x60, out _, out uint currentValue, out _))
    ShowErrorAndExit("Could not get current VCP60 value.", 4);

Console.WriteLine($"Current VCP60 value: {currentValue} ({DellU2410.GetInputSourceNameFromVCP60Value(currentValue)})");

if (args.Length > 0)
{
    var changedValue = DellU2410.GetVCP60ValueFromInputSourceName(args[0]);
    if (changedValue == 0)
        ShowErrorAndExit("Given input source name is not supported.", 5);

    if (!WinApi.SetVCPFeature(hPhysicalMonitor, 0x60, changedValue))
        ShowErrorAndExit("Could not set VCP60 value.", 5);

    Console.WriteLine($"Changed VCP60 value: {changedValue} ({DellU2410.GetInputSourceNameFromVCP60Value(changedValue)})");
}

Console.WriteLine();
Console.WriteLine("Have a nice day.");
Environment.Exit(0);

static void ShowErrorAndExit(string errorMessage, int exitCode)
{
    Console.WriteLine();
    Console.WriteLine($"Error: {errorMessage}");
    Environment.Exit(exitCode);
};