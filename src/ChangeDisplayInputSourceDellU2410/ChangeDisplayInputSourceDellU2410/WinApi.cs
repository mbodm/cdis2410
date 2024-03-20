using System.Runtime.InteropServices;

namespace ChangeDisplayInputSourceDellU2410
{
    public static class WinApi
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        public const uint MONITOR_DEFAULTTOPRIMARY = 1;

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(
            IntPtr hwnd,
            uint dwFlags);

        [DllImport("dxva2.dll")]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor,
            out uint pdwNumberOfPhysicalMonitors);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PHYSICAL_MONITOR
        {
            public IntPtr hPhysicalMonitor;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szPhysicalMonitorDescription;
        }

        [DllImport("dxva2.dll")]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor,
            uint dwPhysicalMonitorArraySize,
            [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        public enum LPMC_VCP_CODE_TYPE
        {
            MC_MOMENTARY,
            MC_SET_PARAMETER
        }

        [DllImport("dxva2.dll")]
        public static extern bool GetVCPFeatureAndVCPFeatureReply(
            IntPtr hMonitor,
            byte bVCPCode,
            out LPMC_VCP_CODE_TYPE pvct,
            out uint pdwCurrentValue,
            out uint pdwMaximumValue);

        [DllImport("Dxva2.dll")]
        public static extern bool SetVCPFeature(
            IntPtr hMonitor,
            byte bVCPCode,
            uint dwNewValue);
    }
}