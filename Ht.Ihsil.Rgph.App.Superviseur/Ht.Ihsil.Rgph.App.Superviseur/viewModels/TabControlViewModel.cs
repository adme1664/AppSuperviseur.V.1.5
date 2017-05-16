using Ht.Ihsil.Rgph.App.Superviseur.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.viewModels
{
    public class TabControlViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string header;

        public string Header
        {
            get { return this.header; }
            set
            { 
                header = value;
                OnPropertyChanged(Header);
            }
        }

    }

    
}
