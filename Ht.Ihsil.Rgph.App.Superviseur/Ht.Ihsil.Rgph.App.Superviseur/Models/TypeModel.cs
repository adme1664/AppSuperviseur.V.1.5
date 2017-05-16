using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class TypeModel
    {
        private string name;
        private string type;

        public TypeModel(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
