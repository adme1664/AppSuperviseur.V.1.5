using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.MVVM
{
   public class ViewModelBase:INotifyPropertyChanged
    {
        #region PropertyChanged Definition
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool NotifyPropertyChanged<T>(ref T variable, T Valeur, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(variable, Valeur)) return false;

            variable = Valeur;
            OnPropertyChanged(propertyName);
            return true;

        }
        #endregion
    }
}
