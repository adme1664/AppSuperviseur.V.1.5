using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class BatimentModel
    {

        public long BatimentId { get; set; }
        public string DeptId { get; set; }
        public string ComId { get; set; }
        public string VqseId { get; set; }
        public string SdeId { get; set; }
        public short Zone { get; set; }
        public string DistrictId { get; set; }
        public string Qhabitation { get; set; }
        public string Qlocalite { get; set; }
        public string Qadresse { get; set; }
        public string Qrec { get; set; }
        public string Qrgph { get; set; }
        public short Qb1Etat { get; set; }
        public short Qb2Type { get; set; }
        public short Qb3NombreEtage { get; set; }
        public short Qb4MateriauMur { get; set; }
        public short Qb5MateriauToit { get; set; }
        public short Qb6StatutOccupation { get; set; }
        public short Qb7Utilisation1 { get; set; }
        public short Qb7Utilisation2 { get; set; }
        public short Qb8NbreLogeCollectif { get; set; }
        public short Qb8NbreLogeIndividuel { get; set; }
        public short Statut { get; set; }
        public string DateEnvoi { get; set; }
        public bool IsValidated { get; set; }
        public bool IsSynchroToAppSup { get; set; }
        public bool IsSynchroToCentrale { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public bool IsContreEnqueteMade { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CodeAgentRecenceur { get; set; }
        public bool IsChecked { get; set; }
        public bool IsMalRempli { get; set; }
        public bool IsFinished { get; set; }
        public bool IsNotFinished { get; set; }
        public Nullable<int> Termine { get; set; }
        public int TypeContreEnquete { get; set; }
        public bool IsValidate { get; set; }
        public long IsVerified { get; set; }
        public string BatimentName
        {
            get
            {
                return "Batiman-" + BatimentId;
            }
        }

        readonly List<LogementModel> _logements = new List<LogementModel>();

        public BatimentModel(long batimentId, string sdeId, string qRec, string qRgph)
        {
            this.BatimentId = batimentId;
            this.SdeId = sdeId;
            this.Qrgph = qRgph;
            this.Qrec = qRec;
        }
        public BatimentModel(long batimentId, string sdeId)
        {
            this.BatimentId = batimentId;
            this.SdeId = sdeId;
        }
        public BatimentModel()
        {

        }

        public BatimentModel(long batimentId, string sdeId, string qRec, string qRgph, string adresse)
        {

        }
        public List<LogementModel> Logement
        {
            get;
            set;
        }
    }
}
