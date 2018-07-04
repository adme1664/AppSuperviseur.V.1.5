using Ht.Ihsil.Rgph.App.Superviseur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Json
{
   public class MenageJson
    {
        public long menageId { get; set; }
        public long logeId { get; set; }
        public long batimentId { get; set; }
        public string sdeId { get; set; }
        public short qm1NoOrdre { get; set; }
        public short qm2ModeJouissance { get; set; }
        public short qm3ModeObtentionLoge { get; set; }
        public short qm4_1ModeAprovEauABoire { get; set; }
        public short qm4_2ModeAprovEauAUsageCourant { get; set; }
        public short qm5SrcEnergieCuisson1 { get; set; }
        public short qm5SrcEnergieCuisson2 { get; set; }
        public short qm6TypeEclairage { get; set; }
        public short qm7ModeEvacDechet { get; set; }
        public short qm8EndroitBesoinPhysiologique { get; set; }
        public int qm9NbreRadio1 { get; set; }
        public int qm9NbreTelevision2 { get; set; }
        public int qm9NbreRefrigerateur3 { get; set; }
        public int qm9NbreFouElectrique4 { get; set; }
        public int qm9NbreOrdinateur5 { get; set; }
        public int qm9NbreMotoBicyclette6 { get; set; }
        public int qm9NbreVoitureMachine7 { get; set; }
        public int qm9NbreBateau8 { get; set; }
        public int qm9NbrePanneauGeneratrice9 { get; set; }
        public int qm9NbreMilletChevalBourique10 { get; set; }
        public int qm9NbreBoeufVache11 { get; set; }
        public int qm9NbreCochonCabrit12 { get; set; }
        public int qm9NbreBeteVolaille13 { get; set; }
        public short qm10AvoirPersDomestique { get; set; }
        public short qm10TotalDomestiqueFille { get; set; }
        public short qm10TotalDomestiqueGarcon { get; set; }
        public int qm11TotalIndividuVivant { get; set; }
        public short qn1Emigration { get; set; }
        public short qn1NbreEmigre { get; set; }
        public short qd1Deces { get; set; }
        public short qd1NbreDecede { get; set; }
        public short statut { get; set; }
        public bool isValidated { get; set; }
        public string dateDebutCollecte { get; set; }
        public string dateFinCollecte { get; set; }
        public int dureeSaisie { get; set; }
        public bool isFieldAllFilled { get; set; }
        public bool isContreEnqueteMade { get; set; }
        public string codeAgentRecenceur { get; set; }
        public bool verified { get; set; }
        public RapportFinalJson rapport { get; set; }
        public List<EmigreJson> emigres { get; set; }
        public List<DecesJson> deces { get; set; }
        public List<IndividuJson> individus { get; set; }
    }
}
