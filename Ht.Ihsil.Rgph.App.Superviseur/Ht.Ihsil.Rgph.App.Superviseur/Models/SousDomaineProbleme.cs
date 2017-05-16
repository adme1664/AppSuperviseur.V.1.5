
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
  public  class SousDomaineProbleme:BindableBase
    {
      private string _sousDomaine;
      private KeyValue _probleme;
      public string SousDomaine
      {
          get
          {
              return this._sousDomaine;
          }
          set
          {
              SetProperty(ref _sousDomaine, value, () => SousDomaine);
          }

      }
      public KeyValue Probleme 
      {
          get
          {
              return _probleme;
          }
          set
          {
              SetProperty(ref _probleme, value, () => Probleme);
          }
      }

      public SousDomaineProbleme(string _sousDomaine, KeyValue probleme)
      {
          this.SousDomaine = _sousDomaine;
          this.Probleme = probleme;
      }
    }
}
