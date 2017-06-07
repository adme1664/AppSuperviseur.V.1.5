using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class KeyValue
    {
        private string value;
        private double cle;

        public double Cle
        {
            get { return cle; }
            set { cle = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private int key;

        public int Key
        {
            get { return key; }
            set { key = value; }
        }
        public KeyValue(int key, string value)
        {
            this.key = key;
            this.value = value;
        }
        public KeyValue(double cle, string value)
        {
            this.cle = cle;
            this.value = value;
        }
        public KeyValue()
        {

        }
    }
}
