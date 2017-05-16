using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
   public  class SdeInformation
    {
        private string deptId;

        public string DeptId
        {
            get { return deptId; }
            set { deptId = value; }
        }
        private string comId;

        public string ComId
        {
            get { return comId; }
            set { comId = value; }
        }
        private string vqseId;

        public string VqseId
        {
            get { return vqseId; }
            set { vqseId = value; }
        }
    }
}
