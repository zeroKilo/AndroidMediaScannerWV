using PortableDeviceApiLib;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AndroidMediaScannerWV
{
    public class PortableDeviceCollection : Collection<PortableDevice>
    {
        private readonly PortableDeviceManager _deviceManager;

        public PortableDeviceCollection()
        {
            this._deviceManager = new PortableDeviceManager();
        }

        [ComImport]
        [Guid("A1567595-4C2F-4574-A6FA-ECEF917B9A40")]
        [InterfaceType(1)]
        public interface IPortableDeviceManagerFixed
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            void GetDevices([Out][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] pPnPDeviceIDs, [In][Out] ref uint pcPnPDeviceIDs);
        }

        public void Refresh()
        {
            Clear();
            Log.PrintLine("- refresh device list...");
            _deviceManager.RefreshDeviceList();
            var deviceIds = new string[100];
            uint count = 100;
            Log.PrintLine("- fetching device list...");
            var devManFixed = (IPortableDeviceManagerFixed)_deviceManager;
            devManFixed.GetDevices(deviceIds, ref count);
            Log.PrintLine("- got " + count + " devices");
            for (int i = 0; i < count; i++)
            {
                string deviceId = deviceIds[i];
                Add(new PortableDevice(deviceId));
                Log.PrintLine("- added " + deviceId);
            }
        }
    }
}
