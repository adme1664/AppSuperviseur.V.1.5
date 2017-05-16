using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class DeviceInformation
    {
        private string deviceName;
        private string imei;
        private string osVersion;
        private string serial;
        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        public string Imei
        {
            get { return imei; }
            set { imei = value; }
        }

        public string OsVersion
        {
            get { return osVersion; }
            set { osVersion = value; }
        }

    }
}
