using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class TextModel : INotifyPropertyChanged
    {
        private string username;

        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Username"));
                }
            }
        }
        private string deconnexion;

        public string Deconnexion
        {
            get { return deconnexion; }
            set
            { 
                deconnexion = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Deconnexion"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
