using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class CheckListItemModel<T>:INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isChecked;
        private T item;

        public bool IsChecked
        {
            get { return _isChecked; }
            set 
            { 
                _isChecked = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }
        public CheckListItemModel(T item, bool isChecked)
        {
            this.item = item;
            this._isChecked = isChecked;
        }
        public T Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Item"));
            }
        }

    }
}
