using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ht.Ihsil.Rgph.App.Superviseur.Models
{
    public class MenageModel
    {

        //
        public long MenageId { get; set; }
        public long LogeId { get; set; }
        public long BatimentId { get; set; }
        public string SdeId { get; set; }
        public int Qm1NoOrdre { get; set; }
        public int Qm2ModeJouissance { get; set; }
        public int Qm3ModeObtentionLoge { get; set; }
        public int Qm4_1ModeAprovEauABoire { get; set; }
        public int Qm4_2ModeAprovEauAUsageCourant { get; set; }
        public int Qm5SrcEnergieCuisson1 { get; set; }
        public int Qm5SrcEnergieCuisson2 { get; set; }
        public int Qm6TypeEclairage { get; set; }
        public int Qm7ModeEvacDechet { get; set; }
        public int Qm8EndroitBesoinPhysiologique { get; set; }
        public int Qm9NbreRadio1 { get; set; }
        public int Qm9NbreTelevision2 { get; set; }
        public int Qm9NbreRefrigerateur3 { get; set; }
        public int Qm9NbreFouElectrique4 { get; set; }
        public int Qm9NbreOrdinateur5 { get; set; }
        public int Qm9NbreMotoBicyclette6 { get; set; }
        public int Qm9NbreVoitureMachine7 { get; set; }
        public int Qm9NbreBateau8 { get; set; }
        public int Qm9NbrePanneauGeneratrice9 { get; set; }
        public int Qm9NbreMilletChevalBourique10 { get; set; }
        public int Qm9NbreBoeufVache11 { get; set; }
        public int Qm9NbreCochonCabrit12 { get; set; }
        public int Qm9NbreBeteVolaille13 { get; set; }
        public int Qm10AvoirPersDomestique { get; set; }
        public int Qm10TotalDomestiqueFille { get; set; }
        public int Qm10TotalDomestiqueGarcon { get; set; }
        public int Qm11TotalIndividuVivant { get; set; }
        public int Qn1Emigration { get; set; }
        public int Qn1NbreEmigreFille { get; set; }
        public int Qn1NbreEmigreGarcon { get; set; }
        public int Qd1Deces { get; set; }
        public int Qd1NbreDecedeFille { get; set; }
        public int Qd1NbreDecedeGarcon { get; set; }
        public int Statut { get; set; }
        public bool IsValidated { get; set; }
        public string DateDebutCollecte { get; set; }
        public string DateFinCollecte { get; set; }
        public int DureeSaisie { get; set; }
        public bool IsFieldAllFilled { get; set; }
        public bool IsContreEnqueteMade { get; set; }
        public string CodeAgentRecenceur { get; set; }
        public Nullable<byte> UpdateDeces { get; set; }
        public Nullable<byte> UpdateEmigre { get; set; }
        public List<IndividuModel> Individus { get; set; }
        public List<DecesModel> Desces { get; set; }
        public List<EmigreModel> Emigre { get; set; }
        public string MenageName
        {
            get
            {
                return "Menaj-" + Qm1NoOrdre;
            }
        }

        public MenageModel(long menageId, int noOrdre)
        {
            MenageId = menageId;
            Qm1NoOrdre = noOrdre;
        }
        public MenageModel()
        {

        }

    }
}
