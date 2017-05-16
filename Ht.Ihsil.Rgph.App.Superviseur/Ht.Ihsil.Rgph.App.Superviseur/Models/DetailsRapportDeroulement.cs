using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class DetailsRapportDeroulement:BindableBase
    {
        #region DECLARATIONS
        private int _num;

        public int Num
        {
            get { return _num; }
            set { _num = value; }
        }
        private KeyValue _domaine;
        private KeyValue _sousDomaine;
        private KeyValue _probleme;
        private KeyValue _solution;
        private string _precision;
        private string _suggestion;
        private KeyValue _suivi;
        private string _commentaire;
        private long _rapportId;
        private long _detailsRapportId;

        public long DetailsRapportId
        {
            get { return _detailsRapportId; }
            set { _detailsRapportId = value; }
        }

        public long RapportId
        {
            get { return _rapportId; }
            set { _rapportId = value; }
        }
        #endregion

        #region PROPERTIES
        public KeyValue Domaine 
        {
            get
            {
                return _domaine;
            }
            set
            {
                SetProperty(ref _domaine, value, () => Domaine);
            }
        }
        public KeyValue SousDomaine
        {
            get
            {
                return _sousDomaine;
            }
            set
            {
                SetProperty(ref _sousDomaine, value, () => SousDomaine);
            }
        }
        public KeyValue Probleme {
            get
            {
                return _probleme;
            }
            set
            {
                SetProperty(ref _probleme, value, () => Probleme);
            }
        }
        public KeyValue Solution {
            get
            {
                return _solution;
            }
            set
            {
                SetProperty(ref _solution, value, () => Solution);
            }
        }
        public string Precisions
        {
            get
            {
                return _precision;
            }
            set
            {
                SetProperty(ref _precision, value, () => Precisions);
            }
        }
        public string Suggestions
        {
            get
            {
                return _suggestion;
            }
            set
            {
                SetProperty(ref _suggestion, value, () => Suggestions);
            }
        }
        public KeyValue Suivi
        {
            get
            {
                return _suivi;
            }
            set
            {
                SetProperty(ref _suivi, value, () => Suivi);
            }
        }
        public string Commentaire
        {
            get
            {
                return _commentaire;
            }
            set
            {
                SetProperty(ref _commentaire, value, () => Commentaire);
            }
        }
        #endregion


        public DetailsRapportDeroulement(int num,KeyValue domaine,KeyValue sousDomaine,KeyValue probleme,KeyValue solution, string precision, string suggestion, KeyValue suivi, string commentaie)
        {
            this.Domaine = domaine;
            this.Num = num;
            this.SousDomaine = sousDomaine;
            this.Probleme = probleme;
            this.Suggestions = suggestion;
            this.Solution = solution;
            this.Precisions = precision;
            this.Solution = solution;
            this.Suivi = suivi;
            this.Commentaire = commentaie;
        }
        public DetailsRapportDeroulement()
        {

        }

    }
}
