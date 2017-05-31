using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
  public  class BatimentJson
    {
        public long batimentId { get; set; }
        public string deptId { get; set; }
        public string comId { get; set; }
        public string vqseId { get; set; }
        public string sdeId { get; set; }
        public short zone { get; set; }
        public string disctrictId { get; set; }
        public string qhabitation { get; set; }
        public string qlocalite { get; set; }
        public string qadresse { get; set; }
        public string qrec { get; set; }
        public string qrgph { get; set; }
        public short qb1Etat { get; set; }
        public short qb2Type { get; set; }
        public short qb3NombreEtage { get; set; }
        public short qb4MateriauMur { get; set; }
        public short qb5MateriauToit { get; set; }
        public short qb6StatutOccupation { get; set; }
        public short qb7Utilisation1 { get; set; }
        public short qb7Utilisation2 { get; set; }
        public short qb8NbreLogeCollectif { get; set; }
        public short qb8NbreLogeIndividuel { get; set; }
        public short statut { get; set; }
        public string dateEnvoi { get; set; }
        public bool isValidated { get; set; }
        public bool isSynchroToAppSup { get; set; }
        public bool isSynchroToCentrale { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public int dureeSaisie { get; set; }
        public bool isFieldAllFilled { get; set; }
        public bool isContreEnqueteMade { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string codeAgentRecenceur { get; set; }

        public List<LogementCJson> logementCs { get; set; }
        public List<LogementIsJson> logementIs { get; set; }
    }
}
