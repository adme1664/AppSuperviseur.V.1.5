using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.utils
{
    public class NameValue
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        public NameValue(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
        public NameValue(string name, string value,string objet)
        {
            this.name = name;
            this.value = value;
            this.objet = objet;
        }

        private string objet;

        public string Objet
        {
            get { return objet; }
            set { objet = value; }
        }
    }
}
