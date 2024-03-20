using ChangeDisplayInputSourceDellU2410;

Console.WriteLine();
Console.WriteLine(Helper.AppTitle);
Console.WriteLine();

var hWindow = WinApi.GetDesktopWindow();
var hMonitor = WinApi.MonitorFromWindow(hWindow, WinApi.MONITOR_DEFAULTTOPRIMARY);

if (WinApi.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out uint numberOfPhysicalMonitors))
{
    if (numberOfPhysicalMonitors > 0)
    {
        var physicalMonitors = new WinApi.PHYSICAL_MONITOR[numberOfPhysicalMonitors];

        if (WinApi.GetPhysicalMonitorsFromHMONITOR(hMonitor, numberOfPhysicalMonitors, physicalMonitors))
        {
            var hPhysicalMonitor = physicalMonitors[0].hPhysicalMonitor;
            var szPhysicalMonitorDescription = physicalMonitors[0].szPhysicalMonitorDescription;

            Console.WriteLine($"Display description: {szPhysicalMonitorDescription}");

            if (WinApi.GetVCPFeatureAndVCPFeatureReply(hPhysicalMonitor, 0x60, out _, out uint currentValue, out _))
            {
                Console.WriteLine($"Current VCP60 value: {currentValue} ({DellU2410.GetInputSourceNameFromVCP60Value(currentValue)})");

                if (args.Length > 0)
                {
                    var changedValue = DellU2410.GetVCP60ValueFromInputSourceName(args[0]);

                    if (changedValue == 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: Given input source name is not supported.");

                        return;
                    }

                    Console.WriteLine($"Changed VCP60 value: {changedValue} ({DellU2410.GetInputSourceNameFromVCP60Value(changedValue)})");

                    WinApi.SetVCPFeature(hPhysicalMonitor, 0x60, changedValue);
                }
            }
        }
    }
}

Console.WriteLine();
Console.WriteLine("Have a nice day.");
