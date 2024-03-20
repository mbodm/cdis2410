namespace ChangeDisplayInputSourceDellU2410
{
    public static class DellU2410
    {
        public static string GetInputSourceNameFromVCP60Value(uint vcp60Value)
        {
            if (vcp60Value == 3) return "DVI";
            if (vcp60Value == 17) return "HDMI";

            return "Unknown";
        }

        public static uint GetVCP60ValueFromInputSourceName(string inputSourceName)
        {
            if (inputSourceName.Equals("DVI", StringComparison.CurrentCultureIgnoreCase)) return 3;
            if (inputSourceName.Equals("HDMI", StringComparison.CurrentCultureIgnoreCase)) return 17;

            return 0;
        }
    }
}