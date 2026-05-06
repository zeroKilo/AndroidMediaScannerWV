using PortableDeviceApiLib;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AndroidMediaScannerWV
{
    public class PortableDeviceFolder : PortableDeviceObject
    {
        [ComImport]
        [InterfaceType(1)]
        [Guid("10ECE955-CF41-4728-BFA0-41EEDF1BBF19")]
        public interface IEnumPortableDeviceObjectIDsFixed
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            void Next([In] uint cObjects, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)][Out] string[] result, [In][Out] ref uint pcFetched);
        }

        private string _fullPath;
        public PortableDeviceFolder(string id, string name, PortableDeviceObject parent) : base(id, name, parent)
        {
            this.Files = new List<PortableDeviceObject>();
        }

        public IList<PortableDeviceObject> Files { get; set; }

        public void EnumerateContents(ref IPortableDeviceContent content, bool recursive = false)
        {
            Log.PrintLine("Enumerating contents for " + Name + "...");
            Application.DoEvents();
            IPortableDeviceProperties properties;
            Log.PrintLine("Getting properties...");
            content.Properties(out properties);
            IEnumPortableDeviceObjectIDs objectIds;
            Log.PrintLine("Getting object IDs...");
            content.EnumObjects(0, Id, null, out objectIds);
            IEnumPortableDeviceObjectIDsFixed objIdsFixed = (IEnumPortableDeviceObjectIDsFixed)objectIds;
            uint fetched = 0;
            uint batch = 100;
            string[] result = new string[batch];
            do
            {
                objIdsFixed.Next(batch, result, ref fetched);
                Log.PrintLine("Requested " + batch + " IDs, got " + fetched);
                for (int i = 0; i < fetched; i++)
                {
                    var currentObject = WrapObject(properties, result[i], this);
                    Files.Add(currentObject);
                    if (currentObject is PortableDeviceFolder && recursive)
                        (currentObject as PortableDeviceFolder).EnumerateContents(ref content, recursive);
                }
            } while (fetched > 0);
        }

        private static PortableDeviceObject WrapObject(IPortableDeviceProperties properties, string objectId, PortableDeviceFolder parent)
        {
            IPortableDeviceKeyCollection keys;
            properties.GetSupportedProperties(objectId, out keys);
            IPortableDeviceValues values;
            properties.GetValues(objectId, keys, out values);
            string name;
            Guid contentType;
            values.GetStringValue(PropertyKey.WPD_OBJECT_NAME, out name);
            values.GetGuidValue(PropertyKey.WPD_OBJECT_CONTENT_TYPE, out contentType);
            if (contentType == DeviceGuid.WPD_CONTENT_TYPE_FOLDER || contentType == DeviceGuid.WPD_CONTENT_TYPE_FUNCTIONAL_OBJECT)
                return new PortableDeviceFolder(objectId, name, parent);
            return new PortableDeviceFile(objectId, name, parent);
        }

        public override string ToString()
        {
            return Name;
        }

        public string FullPath()
        {
            if (_fullPath != null)
                return _fullPath;
            string path = "/" + Name;
            if (Parent != null)
            {
                PortableDeviceFolder parent = Parent as PortableDeviceFolder;
                while (parent != null && parent.Name != "DEVICE")
                {
                    path = "/" + parent.Name + path;
                    parent = parent.Parent as PortableDeviceFolder;
                }
            }
            _fullPath = path;
            return path;            
        }

        public PortableDeviceObject Find(ref IPortableDeviceContent content, string path)
        {
            string[] parts = path.Split('/');
            return Find(ref content, parts, 0);
        }

        private PortableDeviceObject Find(ref IPortableDeviceContent content, string[] path, int index)
        {
            PortableDeviceObject result = null;
            bool found = false;
            if (Files.Count == 0)
                EnumerateContents(ref content, false);
            string pat = path[index].ToLower();
            foreach (var file in Files)
                if (file.Name.ToLower() == pat)
                {
                    result = file;
                    found = true;
                }
            if (found)
            {
                if (index < path.Length - 1)
                    return (result as PortableDeviceFolder).Find(ref content, path, index + 1);
                else
                    return result;
            }
            else
                return result;
        }
    }
}
