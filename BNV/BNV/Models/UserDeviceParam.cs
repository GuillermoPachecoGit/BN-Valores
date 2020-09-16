using System;
using Refit;

namespace BNV.Models
{
    public class UserDeviceParam
    {
        [AliasAs("DeviceId")]
        public string DeviceId { get; set; }

        public UserDeviceParam(string deviceId)
        {
            DeviceId = deviceId;
        }
    }
}
