namespace AndroidMediaScannerWV
{
    public class PortableDeviceFile : PortableDeviceObject
    {
        private string _fullPath;

        public PortableDeviceFile(string id, string name, PortableDeviceObject parent) : base(id, name, parent)
        { }

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
    }
}
