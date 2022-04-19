using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Emovision.Models.VideoCapture
{
    public class DeviceInfoCollection : ObservableCollection<DeviceInfo>
    {
        public DeviceInfoCollection() : base() 
        {
        }

        public DeviceInfoCollection(IEnumerable<DeviceInfo> collection) : base(collection)
        {
        }

        public DeviceInfoCollection(List<DeviceInfo> list) : base(list)
        {
        }
    }
}