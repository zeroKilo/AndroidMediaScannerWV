namespace AndroidMediaScannerWV
{
    public abstract class PortableDeviceObject
    {
        protected PortableDeviceObject(string id, string name, PortableDeviceObject parent)
        {
            Id = id;
            Name = name;
            Parent = parent;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public PortableDeviceObject Parent { get; private set; }
    }
}
