using PortableDeviceApiLib;
using PortableDeviceTypesLib;
using System;
using System.IO;
using IPortableDeviceValues = PortableDeviceApiLib.IPortableDeviceValues;

namespace AndroidMediaScannerWV
{
    public class PortableDevice
    {
        private bool _isConnected;
        private readonly PortableDeviceClass _device;
        private string _friendlyName;

        public PortableDevice(string deviceId)
        {
            this._device = new PortableDeviceClass();
            this.DeviceId = deviceId;
        }

        public string DeviceId { get; set; }

        public void Connect()
        {
            if (this._isConnected) { return; }

            var clientInfo = (IPortableDeviceValues)new PortableDeviceValuesClass();
            this._device.Open(this.DeviceId, clientInfo);
            this._isConnected = true;
        }

        public void Disconnect()
        {
            if (!this._isConnected) { return; }
            this._device.Close();
            this._isConnected = false;
        }

        public string FriendlyName
        {
            get
            {
                if (_friendlyName != null)
                    return _friendlyName;
                if (!this._isConnected)
                    throw new InvalidOperationException("Not connected to device.");
                IPortableDeviceContent content;
                IPortableDeviceProperties properties;
                Log.PrintLine("- getting device content");
                this._device.Content(out content);
                content.Properties(out properties);
                Log.PrintLine("- getting content properties");
                IPortableDeviceValues propertyValues;
                properties.GetValues("DEVICE", null, out propertyValues);
                Log.PrintLine("- getting property values");
                string propertyValue;
                Log.PrintLine("- getting friendly name");
                propertyValues.GetStringValue(ref PropertyKey.WPD_DEVICE_FRIENDLY_NAME, out propertyValue);
                _friendlyName = propertyValue;
                return propertyValue;
            }
        }

        public PortableDeviceFolder GetRootContents(bool recursive = false)
        {
            var root = new PortableDeviceFolder("DEVICE", "DEVICE", null);
            var content = GetDeviceContent();
            root.EnumerateContents(ref content, recursive);
            return root;
        }

        public IPortableDeviceContent GetDeviceContent()
        {
            IPortableDeviceContent content;
            this._device.Content(out content);
            return content;
        }
        public void DownloadFile(PortableDeviceFile file, string path)
        {
            try
            {
                IPortableDeviceContent content = GetDeviceContent();
                IPortableDeviceResources resources;
                Log.PrintLine("- getting transfer");
                content.Transfer(out resources);
                PortableDeviceApiLib.IStream wpdStream;
                uint optimalTransferSize = 0;
                Log.PrintLine("- getting stream");
                resources.GetStream(file.Id, ref PropertyKey.WPD_RESOURCE_DEFAULT, 0, ref optimalTransferSize, out wpdStream);
                System.Runtime.InteropServices.ComTypes.IStream sourceStream = (System.Runtime.InteropServices.ComTypes.IStream)wpdStream;
                var filename = Path.GetFileName(file.Id);
                Log.PrintLine("- writing to " + path);
                FileStream targetStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                unsafe
                {
                    var buffer = new byte[optimalTransferSize];
                    int bytesRead;
                    do
                    {
                        sourceStream.Read(buffer, (int)optimalTransferSize, new IntPtr(&bytesRead));
                        Log.PrintLine("- requested to read " + optimalTransferSize + ", actually read " + bytesRead);
                        targetStream.Write(buffer, 0, (int)optimalTransferSize);
                    } while (bytesRead > 0);
                    targetStream.Close();
                }
                Log.PrintLine("- stream closed");
            }
            catch { }
        }
    }
}
