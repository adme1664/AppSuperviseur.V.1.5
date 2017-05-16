using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class TableVerificationModel : INotifyPropertyChanged
    {
       private int _iD;
       private string _type;
       private int _parentID;
       public string _total;
       private string _indicateur;
       private string _taux;
       private string _color;
       private string _niveau;

       public string Niveau
       {
           get { return _niveau; }
           set { _niveau = value; }
       }

       public string Color
       {
           get { return _color; }
           set 
           { 
               _color = value;
               RaisePropertyChanged("Color");
           }
       }
       private string _image;

       public string Image
       {
           get { return _image; }
           set { _image = value; }
       }

       public event PropertyChangedEventHandler PropertyChanged;

       void RaisePropertyChanged(string name)
       {
           if (PropertyChanged != null)
               PropertyChanged(this, new PropertyChangedEventArgs(name));
       }
      
       public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
       public string Indicateur
       {
           get { return _indicateur; }
           set { _indicateur = value; }
       }

       public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }
       
       public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

       
        public string Total
        {
            get { return _total; }
            set { _total = value; }
        }
        public string Taux
        {
            get { return _taux; }
            set { _taux = value; }
        }

       
    }
}
