using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class BatimentCEModel
    {
        public string DeptId { get; set; }
        public string ComId { get; set; }
        public string District { get; set; }
        public string Zone { get; set; }
        public string VqseId { get; set; }
        public long Id { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public string Qhabitation { get; set; }
        public string Qrec { get; set; }
        public string Qrgph { get; set; }
        public string Qadresse { get; set; }
        public string Qlocalite { get; set; }
        public Nullable<byte> Qb1Etat { get; set; }
        public Nullable<byte> Qb2Type { get; set; }
        public Nullable<byte> Qb3NombreEtage { get; set; }
        public Nullable<byte> Qb4MateriauMur { get; set; }
        public Nullable<byte> Qb5MateriauToit { get; set; }
        public Nullable<byte> Qb6StatutOccupation { get; set; }
        public Nullable<byte> Qb7Utilisation1 { get; set; }
        public Nullable<byte> Qb7Utilisation2 { get; set; }
        public Nullable<byte> Qb8NbreLogeCollectif { get; set; }
        public Nullable<byte> Qb8NbreLogeIndividuel { get; set; }
        public Nullable<byte> Statut { get; set; }
        public Nullable<bool> IsValidated { get; set; }
        public Nullable<bool> IsSynchroToCentrale { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public Nullable<int> DureeSaisie { get; set; }
        public Nullable<bool> IsContreEnqueteMade { get; set; }
        public Nullable<bool> IsValidate { get; set; }
        public Nullable<bool> ValidateCE { get; set; }
        public int TypeContreEnquete { get; set; }
        public bool IsChecked { get; set; }
        public bool IsMalRempli { get; set; }
        public bool IsFinished { get; set; }
        public bool IsNotFinished { get; set; }
        public Nullable<int> Termine { get; set; }
        public string BatimentName
        {
            get
            {
                return "Batiman-" + BatimentId;
            }
        }
    }
}
