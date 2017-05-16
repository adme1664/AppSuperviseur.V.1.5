using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class CouvertureModel
    {
        private string _couverture;

        public string Couverture
        {
            get { return _couverture; }
            set { _couverture = value; }
        }
        private int _actualisation;

        public int Actualisation
        {
            get { return _actualisation; }
            set { _actualisation = value; }
        }
        private int _total;

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }
        private string _validite;

        public string Validite
        {
            get { return _validite; }
            set { _validite = value; }
        }
    }
}
